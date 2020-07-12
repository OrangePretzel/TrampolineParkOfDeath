Shader "TPoD/RenderPass/XYGradient"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		// X Colors
		_X0Color("X0 Color", Color) = (0, 0, 0, 1)
		_X1Color("X1 Color", Color) = (1, 0, 0, 1)

		// Y Colors
		_Y0Color("Y0 Color", Color) = (0, 0, 0, 1)
		_Y1Color("Y1 Color", Color) = (0, 1, 0, 1)
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			HLSLPROGRAM

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			#pragma vertex vert
			#pragma fragment frag

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);

			half4 _X0Color;
			half4 _X1Color;
			half4 _Y0Color;
			half4 _Y1Color;

			struct Attributes
			{
				float4 positionOS : POSITION;
				half2 uv0 : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				half2 uv0 : TEXCOORD0;
			};

			Varyings vert(in Attributes IN)
			{
				Varyings OUT = (Varyings)0;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS.xyz);
				OUT.positionCS = vertexInput.positionCS;

				OUT.uv0 = IN.uv0;

				return OUT;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv0.xy);
				half4 xColor = _X1Color * IN.uv0.x + _X0Color * (1 - IN.uv0.x);
				half4 yColor = _Y1Color * IN.uv0.y + _Y0Color * (1 - IN.uv0.y);
				return xColor * yColor * color;
			}

			ENDHLSL
		}
	}
}
