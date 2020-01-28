Shader "Custom/lw_drawHeat"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Coordinate("Coordinate", Vector) = (0,0,0,0)
		_Color("Draw Color", Color) = (1,0,0,0)
		_Scale("Scale", float) = .1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            struct Attributes
            {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Coordinate, _Color;
			float _Scale;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.vertex = mul(mul(unity_MatrixVP, unity_ObjectToWorld), (v.vertex)); //replaced UnityObjectToClipPos
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                // sample the texture
				half4 col = tex2D(_MainTex, i.uv);
				float draw = pow(saturate(1 - distance(i.uv,_Coordinate.xy)), 100); //speed
				half4 drawcol = _Color * (draw * _Scale);
				return saturate(col+drawcol);
            }
            ENDHLSL
        }
    }
}
