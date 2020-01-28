Shader "Custom/lw_visHeat"
{
    Properties
    {
        _StartColor ("Start Color", Color) = (0,0,1,1)
		_StartTex ("Start texture (RGB)", 2D) = "white" {}
		_MidColor ("Mid Color", Color) = (1,1,0,1)
		_MidTex ("Mid texture (RGB)", 2D) = "white" {}
		_EndColor ("End Color", Color) = (1,0,0,1)
		_EndTex ("End texture (RGB)", 2D) = "white" {}
		_Splat ("SplatMap", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma vertex vert
            #pragma fragment frag


            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float2 splat : TEXCOORD3;
                UNITY_VERTEX_INPUT_INSTANCE_ID
		    };

            struct Varyings
            {
                float4 vertex : SV_POSITION;
                float2 uv_StartTex: TEXCOORD0;
                float2 uv_MidTex: TEXCOORD1;
                float2 uv_EndTex: TEXCOORD2;
                float2 uv_Splat: TEXCOORD3;
            };

		    sampler2D _Splat;

            sampler2D _StartTex;
            half4 _StartColor;
            sampler2D _MidTex;
            half4 _MidColor;
            sampler2D _EndTex;
            half4 _EndColor;

            Varyings vert (Attributes a){
                Varyings o;
                o.vertex = mul(mul(unity_MatrixVP, unity_ObjectToWorld), (a.vertex)); //replaced UnityObjectToClipPos
                o.uv_StartTex = a.texcoord;
                o.uv_MidTex = a.texcoord1;
                o.uv_EndTex = a.texcoord2;
                o.uv_Splat = a.splat;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half amount = tex2Dlod(_Splat,float4(i.uv_Splat,0,0)).r; //only detects red color on the splatmap.
                half4 c = (step(amount,0.5))*lerp(tex2D (_StartTex, i.uv_StartTex) * _StartColor, tex2D (_MidTex, i.uv_MidTex) * _MidColor, amount*2) 
					 + (1-step(amount,0.5))*lerp(tex2D (_MidTex, i.uv_MidTex) * _MidColor, tex2D (_EndTex, i.uv_EndTex) * _EndColor, float(amount*2)-1);
		
                return c;
            }
            ENDHLSL
        }
    }
}
