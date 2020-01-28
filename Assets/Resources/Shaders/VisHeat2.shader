Shader "Custom/VisHeat2" {
	Properties {
		_StartColor ("Start Color", Color) = (0,0,1,1)
		_StartTex ("Start texture (RGB)", 2D) = "white" {}
		_MidColor ("Mid Color", Color) = (1,1,0,1)
		_MidTex ("Mid texture (RGB)", 2D) = "white" {}
		_EndColor ("End Color", Color) = (1,0,0,1)
		_EndTex ("End texture (RGB)", 2D) = "white" {}
		_Splat ("SplatMap", 2D) = "black" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows


		#pragma target 4.6

		sampler2D _Splat;

		sampler2D _StartTex;
		fixed4 _StartColor;
		sampler2D _MidTex;
		fixed4 _MidColor;
		sampler2D _EndTex;
		fixed4 _EndColor;

		struct Input {
			float2 uv_StartTex;
			float2 uv_MidTex;
			float2 uv_EndTex;
			float2 uv_Splat;
		};
		fixed4 _Color;


		void surf (Input IN, inout SurfaceOutputStandard o) {

			half amount = tex2Dlod(_Splat,float4(IN.uv_Splat,0,0)).r; //only detects red color on the splatmap.

			fixed4 c = (step(amount,0.5))*lerp(tex2D (_StartTex, IN.uv_StartTex) * _StartColor, tex2D (_MidTex, IN.uv_MidTex) * _MidColor, amount*2) 
					 + (1-step(amount,0.5))*lerp(tex2D (_MidTex, IN.uv_MidTex) * _MidColor, tex2D (_EndTex, IN.uv_EndTex) * _EndColor, float(amount*2)-1);
			
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
