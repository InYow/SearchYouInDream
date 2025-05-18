Shader "PostProcessing/GlassBreak"
{
    Properties
    {
         _MainTex("MainTexture",2D) = "white"{}
        _CrackTex ("Crack Texture", 2D) = "white" {}
        _GlassColor("Glass Color",COLOR) = (1,1,1,1)
        _DistortionStrength ("Distortion Strength", Float) = 0.05
    }
    SubShader
    {
       Tags
        {
            "RenderPipeline" = "UniversalPipeline"
        }
        Pass
        {
            ZTest Always
            Cull Off
            ZWrite Off
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_CrackTex);
            SAMPLER(sampler_CrackTex);
            float _DistortionStrength;
            float4 _GlassColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 crack = SAMPLE_TEXTURE2D(_CrackTex, sampler_CrackTex,i.uv).rg; // 使用红蓝通道偏移
                float2 offset = (crack - 0.5) * _DistortionStrength;

                float2 distortedUV = i.uv + offset;
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex,distortedUV);

                // 可选：叠加裂缝颜色
                float crackAmount = SAMPLE_TEXTURE2D(_CrackTex, sampler_CrackTex,i.uv).b;
                col.rgb = lerp(col.rgb, _GlassColor.rgb, crackAmount);

                return col;
            }
            ENDHLSL
        }
    }
}
