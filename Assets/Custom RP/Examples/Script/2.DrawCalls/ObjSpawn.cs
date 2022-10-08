using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjSpawn : MonoBehaviour
{
    [Range(1, 10000)]
    public int Count = 76;
    public Material[] Materials;
    public int MaxMatIndex;

    private void Awake()
    {
        Spawn();
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        Clear();
        int maxRow = Mathf.CeilToInt(Mathf.Sqrt(Count));
        int half = maxRow / 2;
        for(int i = 0; i <= Count; i ++ )
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var render = obj.GetComponent<MeshRenderer>();
            var mat = Materials[Random.Range(0, Mathf.Min(MaxMatIndex, Materials.Length))];
            render.sharedMaterial = mat;
            obj.name = mat.name;
            obj.transform.SetParent(transform, false);
            int col = i % maxRow - half;
            int row = i / maxRow;
            obj.transform.localPosition = new Vector3(col, row*0.2f, row);

            var property = obj.AddComponent<PerObjectMaterialProperties>();
            float r = Random.Range(0f,1f);
            float g = Random.Range(0f,1f);
            float b = Random.Range(0f,1f);
            float a = Random.Range(0.5f,1f);
            property.baseColor = new Color(r, g, b, a);
            property.cutoff = Random.Range(0.2f, 0.7f);
            property.RefreshBlock();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        for(int i = transform.childCount-1; i >= 0; i --)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
