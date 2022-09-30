using UnityEngine;

public class MeshBall : MonoBehaviour
{

	static int baseColorId = Shader.PropertyToID("_BaseColor");

	[SerializeField]
	Mesh mesh = default;
	[SerializeField]
	Material material = default;
	[SerializeField]
	[Range(1, 1023)]
	int InsCount = 1023;

	Matrix4x4[] matrices;
	Vector4[] baseColors;

	MaterialPropertyBlock block;

	private void Awake()
    {
		matrices = new Matrix4x4[InsCount];
		baseColors = new Vector4[InsCount];

		for (int i = 0; i < InsCount; i++)
        {
			matrices[i] = Matrix4x4.TRS(
				Random.insideUnitSphere * 10f + new Vector3(0,0,10), Quaternion.identity, Vector3.one
			);
			baseColors[i] =
				new Vector4(Random.value, Random.value, Random.value, 1f);
		}
    }

	void Update()
	{
		if (block == null)
		{
			block = new MaterialPropertyBlock();
			block.SetVectorArray(baseColorId, baseColors);
		}
		Graphics.DrawMeshInstanced(mesh, 0, material, matrices, InsCount, block);
	}
}
