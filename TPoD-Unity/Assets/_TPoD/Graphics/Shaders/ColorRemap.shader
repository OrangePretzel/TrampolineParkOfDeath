Shader "TPoD/RenderPass/ColorRemap"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ColorRamp("Color Ramp", 2D) = "white" {}
		_RemapFactor("Remap Factor", float) = 0.5
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			HLSLPROGRAM

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
			// #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

			#pragma vertex vert
			#pragma fragment frag

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);

			TEXTURE2D(_ColorRamp);
			SAMPLER(sampler_ColorRamp);

			float _RemapFactor;

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
				half luminosity = 0.3 * color.r + 0.59 * color.g + 0.11 * color.b;

				half4 remapColor = SAMPLE_TEXTURE2D(_ColorRamp, sampler_ColorRamp, half2(luminosity, 0));

				half4 outColor = remapColor * _RemapFactor + color * (1 - _RemapFactor);
				return half4(outColor.xyz, 1);
			}

			ENDHLSL
		}
	}
}
