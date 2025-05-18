using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrokenGlassFeature : ScriptableRendererFeature
{
    class BrokenGlassPass : ScriptableRenderPass
    {
        private Material material;
        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;
        private string profilerTag;

        public BrokenGlassPass(Material mat, string tag)
        {
            material = mat;
            profilerTag = tag;
            tempTexture.Init("_TempGlassEffectTex");
        }

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var stack = VolumeManager.instance.stack;
            var settings = stack.GetComponent<PostProcessingBrokenGlass>();
            if (settings == null || !settings.IsActive()) return;

            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
            desc.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempTexture.id, desc);

            // 设置全局贴图
            cmd.SetGlobalTexture("_MainTex", source);

            // 设置材质参数
            material.SetFloat("_DistortionStrength", settings.glassTint.value);
            material.SetTexture("_CrackTex", settings.glassMask.value);
            material.SetColor("_GlassColor", settings.glassColor.value);

            // Blit 实际执行
            Blit(cmd, source, tempTexture.Identifier(), material);
            Blit(cmd, tempTexture.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }


    public Material material;
    private BrokenGlassPass _pass;

    public override void Create()
    {
        if (material == null)
        {
            Shader shader = Shader.Find("PostProcessing/RadialBlur");
            if (shader == null)
            {
                Debug.LogError("未找到玻璃破碎Shader");
                return;
            }
            material = new Material(shader);
        }
        _pass = new BrokenGlassPass(material, "BrokenGlassPass");
        _pass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _pass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(_pass);
    }
}
