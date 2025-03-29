Shader "PostProcessing/RadialBlur"
{
    Properties
    {
        _MainTex("MainTexture",2D) = "white"{}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
        }
        Cull Off
        ZWrite Off
        ZTest Always

        HLSLINCLUDE
            
        struct a2v
         {
             float4 positionOS:POSITION;
             float2 texcoord:TEXCOORD;
         };
         struct v2f
         {
             float4 positionCS:SV_POSITION;
             float2 texcoord:TEXCOORD;
         };
        ENDHLSL
        
        Pass
        {
            Tags
            {
                "LightMode"="UniversalForward"
            }
            
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #pragma vertex vert
            #pragma fragment frag
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float _BlurStrength;
            float _CenterAffactor;
            float2 _RadialCenter;
            int _RadialLoopCount;
            
            v2f vert(a2v IN)
            {
                v2f o;
                o.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                o.texcoord = IN.texcoord;
                return o;
            }

            half4 frag(v2f IN) : SV_Target
            {
                half4 col = 0;
                float2 offset = (_RadialCenter.xy-IN.texcoord.xy)*_BlurStrength*0.01;
                float centerDist = length(offset)*_CenterAffactor;
                float centersmooth = smoothstep(0,1,centerDist);
                for(int i=0;i<_RadialLoopCount;i++)
                {
                    col += SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,(IN.texcoord.xy + centersmooth*offset*i));
                }
                col /= _RadialLoopCount;
                return col;
            } 
            
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
