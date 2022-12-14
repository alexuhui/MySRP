#ifndef CUSTOM_UNLIT_PASS_INCLUDED
	#define CUSTOM_UNLIT_PASS_INCLUDED

	#include "../ShaderLibrary/Common.hlsl"

	//CBUFFER_START(UnityPerMaterial)
	//	half4 _BaseColor;
	//CBUFFER_END

	TEXTURE2D(_BaseMap);
	SAMPLER(sampler_BaseMap);

	UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
		//UNITY_DEFINE_INSTANCED_PROP(2D, _BaseMap)
		UNITY_DEFINE_INSTANCED_PROP(float4, _BaseMap_ST)
		UNITY_DEFINE_INSTANCED_PROP(float4, _BaseColor)
		UNITY_DEFINE_INSTANCED_PROP(float, _Cutoff)
	UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

	struct Attributes {
		float3 positionOS : POSITION;
		float2 baseUV : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct Varyings {
		float4 positionCS : SV_POSITION;
		float2 baseUV : VAR_BASE_UV;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	Varyings UnlitPassVertex (Attributes input){
		Varyings output;
		UNITY_SETUP_INSTANCE_ID(input);
		UNITY_TRANSFER_INSTANCE_ID(input, output);
		float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
		output.positionCS = TransformWorldToHClip(positionWS);
		float4 baseST = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseMap_ST);
		output.baseUV = input.baseUV * baseST.xy + baseST.zw;
		return output;
	}

	half4 UnlitPassFragment (Varyings input): SV_TARGET {
		UNITY_SETUP_INSTANCE_ID(input);
		//sampler2D baseMap = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseMap);
		half4 baseTex = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.baseUV);
		half4 baseCol = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseColor);
		half4 col = baseTex * baseCol;

		#if defined(_CLIPPING)
			half cutoff = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Cutoff);
			clip(col.a - cutoff);
		#endif
		return col;
	}

#endif