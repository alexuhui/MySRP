#ifndef CUSTOM_COMMON_INCLUDED
	#define CUSTOM_COMMON_INCLUDED

	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
	#include "UnityInput.hlsl"

	//// 模型空间转世界空间
	//float3 TransformObjectToWorld (float3 positionOS) {
	//	return mul(unity_ObjectToWorld, float4(positionOS, 1.0)).xyz;
	//}
	
	//// 世界空间转相机空间转齐次裁剪空间
	//float4 TransformWorldToHClip (float3 positionWS) {
	//	return mul(unity_MatrixVP, float4(positionWS, 1.0));
	//}

	#define UNITY_MATRIX_M unity_ObjectToWorld
	#define UNITY_MATRIX_I_M unity_WorldToObject
	#define UNITY_MATRIX_V unity_MatrixV
	#define UNITY_MATRIX_VP unity_MatrixVP
	#define UNITY_MATRIX_P glstate_matrix_projection
	//https://docs.unity3d.com/Packages/com.unity.render-pipelines.core@14.0/changelog/CHANGELOG.html
	//Added UNITY_PREV_MATRIX_M and UNITY_PREV_MATRIX_I_M shader macros to support instanced motion vector rendering
	//Copy from the file Input.hlsl of URP
	#define UNITY_PREV_MATRIX_M   unity_MatrixPreviousM
	#define UNITY_PREV_MATRIX_I_M unity_MatrixPreviousMI

	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

#endif