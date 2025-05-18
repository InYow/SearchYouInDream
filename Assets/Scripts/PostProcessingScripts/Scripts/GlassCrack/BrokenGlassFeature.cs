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
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
            desc.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempTexture.id, desc);

            // 设置当前屏幕图像为 _MainTex
            cmd.SetGlobalTexture("_MainTex", source);

            // 执行两次 Blit：先处理，再写回
            Blit(cmd, source, tempTexture.Identifier(), material);
            Blit(cmd, tempTexture.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

    }

    [System.Serializable]
    public class BrokenGlassSettings
    {
        public Material material;
    }

    public BrokenGlassSettings settings = new BrokenGlassSettings();
    private BrokenGlassPass _pass;

    public override void Create()
    {
        _pass = new BrokenGlassPass(settings.material, "BrokenGlassPass");
        _pass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _pass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(_pass);
    }
}