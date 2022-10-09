using UnityEngine;

public class DrectLightMeshBall : MonoBehaviour
{

	static int 
		baseColorId = Shader.PropertyToID("_BaseColor"),
		metallicId = Shader.PropertyToID("_Metallic"),
		smoothnessId = Shader.PropertyToID("_Smoothness");


	[SerializeField]
	Mesh mesh = default;
	[SerializeField]
	Material material = default;
	[SerializeField]
	[Range(1, 1023)]
	int InsCount = 1023;

	Matrix4x4[] matrices;
	Vector4[] baseColors;
	float[] metallic,smoothness;

	MaterialPropertyBlock block;

	private void Awake()
    {
		matrices = new Matrix4x4[InsCount];
		baseColors = new Vector4[InsCount];
		metallic = new float[InsCount];
		smoothness = new float[InsCount];

		for (int i = 0; i < InsCount; i++)
        {
			matrices[i] = Matrix4x4.TRS(
				Random.insideUnitSphere * 10f + new Vector3(0,0,10), Quaternion.identity, Vector3.one
			);
			baseColors[i] =
				new Vector4(Random.value, Random.value, Random.value, 1f);
			metallic[i] = Random.value < 0.25f ? 1f : 0f;
			smoothness[i] = Random.Range(0.05f, 0.95f);
		}
    }

	void Update()
	{
		if (block == null)
		{
			block = new MaterialPropertyBlock();
			block.SetVectorArray(baseColorId, baseColors);
			block.SetFloatArray(metallicId, metallic);
			block.SetFloatArray(smoothnessId, smoothness);
		}
		Graphics.DrawMeshInstanced(mesh, 0, material, matrices, InsCount, block);
	}
}
