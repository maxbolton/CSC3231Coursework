Shader"Custom Shaders/obeliskShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _EmissionTex ("Emission Texture", 2D) = "black" {}
        _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1)
        _IsFloating("Floating", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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
            sampler2D _EmissionTex;
            fixed4 _EmissionColor;
            float _IsFloating;
            
            v2f vert(appdata v)
            {
                v2f output;

                float4 localSpace = v.vertex;
                    
                if (_IsFloating > 0.5)
                {
                    localSpace.y += sin(movement);
                }
                
                output.vertex = UnityObjectToClipPos(localSpace);
                output.uv = v.uv;
                return output;
            }

            fixed4 frag(v2f input) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, input.uv);
                fixed4 emissionCol = tex2D(_EmissionTex, input.uv) * _EmissionColor;

                col.rgb += emissionCol.rgb;
                return col;
            }
            ENDCG
        }
    }
}
