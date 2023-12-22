Shader"Custom Shaders/water"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color("Colour", Color) = (0, 0, 0, 0.1)
        _Alpha("Alpha", Range(0.0, 1.0)) = 0.5
        _Tiling("Tiling", Range(1, 100)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex      vert
            #pragma fragment    frag
            

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
            };

            float movement;
            sampler2D _MainTex;
            float4 _Color;
            float _Alpha;
            float _Tiling;
            
            v2f vert(appdata v)
            {
                v2f output;
    
                float4 localSpace = v.vertex;
                localSpace.y += sin(movement);
                output.vertex = UnityObjectToClipPos(localSpace);
                output.uv = v.uv * _Tiling;
                return output;
            }

            fixed4 frag(v2f input) : SV_Target
            {
                
                fixed4 col = tex2D(_MainTex, input.uv);
                col.rgb *= _Color.rgb;
                col.a *= _Alpha;
                return col;
             
            }

                ENDCG
            }
        }
    }
