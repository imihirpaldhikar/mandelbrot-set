Shader "Explorer/Mandelbrot"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Area ("Area", vector) = (0, 0, 4, 4)
        _Angle ("Angle", range(-3.1415, 3.1415)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _Area;
            float _Angle;

            float2 rot(float2 pointer, float2 pivot, float a) {
                float sine = sin(a);
                float cosine = cos(a);

                pointer -= pivot;
                pointer = float2(pointer.x*cosine - pointer.y*sine, pointer.x*sine + pointer.y*cosine);

                pointer += pivot;

                return pointer;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 c = _Area.xy + (i.uv - 0.5)*_Area.zw;

                c = rot(c, _Area.xy, _Angle);

                float2 z;
                float iter;
                for(iter = 0; iter < 6000; iter++) {
                    z = float2(z.x*z.x - z.y*z.y, 2*z.x*z.y) + c;
                    if(length(z) > 2) break;
                }
                float b =  sqrt(iter / 6000);

                float4 color = sin(float4(0.8, 0.48, 0.60, 1) * b*100) * 0.5 + 0.5;

                return color;
            }
            ENDCG
        }
    }
}
