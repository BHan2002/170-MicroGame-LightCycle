Shader "Custom/InstancedUnlit" {
    Properties {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
    }

    SubShader {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }

        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _BaseColor)
                UNITY_DEFINE_INSTANCED_PROP(float2, _UVOffset)
            UNITY_INSTANCING_BUFFER_END(Props)

            Varyings vert(Attributes IN) {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv + UNITY_ACCESS_INSTANCED_PROP(Props, _UVOffset);
                OUT.color = UNITY_ACCESS_INSTANCED_PROP(Props, _BaseColor);

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target {
                float2 g = abs(frac(IN.uv * 8) - 0.5);
                float GridLine = smoothstep(0.45, 0.5, max(g.x, g.y));

                half3 col = IN.color.rgb;
                col *= 1.0 - GridLine * 0.3;

                return half4(col, IN.color.a);
            }

            ENDHLSL
        }
    }
}