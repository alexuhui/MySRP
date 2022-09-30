using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjSpawn : MonoBehaviour
{
    [Range(1, 10000)]
    public int Count = 76;
    public Material[] Materials;

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
            render.sharedMaterial = Materials[Random.Range(0, Materials.Length)];
            obj.transform.SetParent(transform, false);
            int col = i % maxRow - half;
            int row = i / maxRow;
            obj.transform.localPosition = new Vector3(col, row*0.2f, row);

            var property = obj.AddComponent<PerObjectMaterialProperties>();
            byte r = (byte)Random.Range(0,256);
            byte g = (byte)Random.Range(0,256);
            byte b = (byte)Random.Range(0,256);
            //property.baseColor = new Color32(r, g, b, 255);
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
