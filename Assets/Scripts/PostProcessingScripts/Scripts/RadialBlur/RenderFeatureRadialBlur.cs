using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessingScripts.Scripts
{
    public class RenderFeatureRadialBlur : ScriptableRendererFeature
    {class RadialBlurPass : ScriptableRenderPass
    {
        private Material material;
        private RenderTargetIdentifier source;
        private RenderTargetIdentifier tempTexture;
        private int tempTextureID = Shader.PropertyToID("_TempTex");

        public RadialBlurPass(Material mat)
        {
            material = mat;
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        }

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null) return;

            CommandBuffer cmd = CommandBufferPool.Get("Radial Blur Effect");
            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;

            cmd.GetTemporaryRT(tempTextureID, opaqueDesc);
            tempTexture = new RenderTargetIdentifier(tempTextureID);

            VolumeStack stack = VolumeManager.instance.stack;
            PostProcessingRadialBlur blurEffect = stack.GetComponent<PostProcessingRadialBlur>();

            if (!blurEffect.IsActive()) return;

            material.SetInt("_RadialLoopCount", blurEffect.loop.value);
            material.SetFloat("_BlurStrength", blurEffect.blur.value);
            material.SetFloat("_CenterAffactor", blurEffect.radialSmoothness.value);
            material.SetVector("_RadialCenter", blurEffect.radialCenter.value);

            cmd.Blit(source, tempTexture, material);
            cmd.Blit(tempTexture, source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
            cmd.ReleaseTemporaryRT(tempTextureID);
        }
    }

    RadialBlurPass radialBlurPass;
    public Material material;

    public override void Create()
    {
        if (material == null)
        {
            Shader shader = Shader.Find("PostProcessing/RadialBlur");
            if (shader == null)
            {
                Debug.LogError("未找到径向模糊Shader");
                return;
            }
            material = new Material(shader);
        }
        radialBlurPass = new RadialBlurPass(material);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (material == null) return;
        radialBlurPass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(radialBlurPass);
    }
    }
}
