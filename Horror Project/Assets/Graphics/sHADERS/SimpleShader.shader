Shader "Unlit/SimpleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            
            //Vert Input
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL; 
                float2 uv : TEXCOORD0;
            };


            //Vert output
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            //frag input
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            //frag output
            fixed4 frag(v2f o) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //return col;

                float3 normal = o.normal;
                float3 lightDir = normalize(float3(1, 1, 1));
                float3 lightColor = float3(0.9, 0.8, 0.76);
                float lightFallOff = max(0, dot(lightDir, normal));
                float3 diffuse = lightColor * lightFallOff;

                float3 ambientLight = float3 (0.2, 0.35, 0.4);

                return float4(ambientLight+diffuse, 0);
            }
            ENDCG
        }
    }
}
