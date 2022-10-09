using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Lighting
{
	const string bufferName = "Lighting";
	const int maxDirLightCount = 4;
	static int
		//dirLightColorId = Shader.PropertyToID("_DirectionalLightColor"),
		//dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection"),
		dirLightCountId = Shader.PropertyToID("_DirectionalLightCount"),
		dirLightColorsId = Shader.PropertyToID("_DirectionalLightColors"),
		dirLightDirectionsId = Shader.PropertyToID("_DirectionalLightDirections");

	static Vector4[]
		dirLightColors = new Vector4[maxDirLightCount],
		dirLightDirections = new Vector4[maxDirLightCount];


	CullingResults cullingResults;


	CommandBuffer buffer = new CommandBuffer
	{
		name = bufferName
	};

	public void Setup(ScriptableRenderContext context, CullingResults cullingResults)
	{
		this.cullingResults = cullingResults;

		buffer.BeginSample(bufferName);
		//SetupDirectionalLight();
		SetupLights();
		buffer.EndSample(bufferName);
		context.ExecuteCommandBuffer(buffer);
		buffer.Clear();
	}

	// 定义ref参数直接引用结构以避免反复创建
	void SetupDirectionalLight(int index, ref VisibleLight visibleLight) {
		//Light light = RenderSettings.sun;
		//buffer.SetGlobalVector(dirLightColorId, light.color.linear * light.intensity);
		//buffer.SetGlobalVector(dirLightDirectionId, -light.transform.forward);
		dirLightColors[index] = visibleLight.finalColor;
		dirLightDirections[index] = -visibleLight.localToWorldMatrix.GetColumn(2);
	}

	void SetupLights()
    {
		NativeArray<VisibleLight> visibleLights = cullingResults.visibleLights;
		int dirLightCount = 0;
		for (int i = 0; i < visibleLights.Length; i ++)
        {
			var visibleLight = visibleLights[i];
			// 只支持平行光
			if (visibleLight.lightType != LightType.Directional)
            {
				continue;
			}
			SetupDirectionalLight(dirLightCount++, ref visibleLight);
			if (dirLightCount >= maxDirLightCount)
			{
				break;
			}
		}
		buffer.SetGlobalInt(dirLightCountId, dirLightCount);
		buffer.SetGlobalVectorArray(dirLightColorsId, dirLightColors);
		buffer.SetGlobalVectorArray(dirLightDirectionsId, dirLightDirections);
	}
}
