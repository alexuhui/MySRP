using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrectLight : MonoBehaviour
{
    public Material material;

    private void Awake()
    {
        Spawn();
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        Clear();
        int size = 5;
        for(int row = 0; row < size; row ++ )
        {
            for(int col = 0; col < size; col ++)
            {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                var render = obj.GetComponent<MeshRenderer>();
                render.sharedMaterial = material;
                obj.name = material.name;
                obj.transform.SetParent(transform, false);
                obj.transform.localPosition = new Vector3(col, -row, 0);

                var property = obj.AddComponent<PerObjectMaterialProperties>();
                float r = Random.Range(0f, 1f);
                float g = Random.Range(0f, 1f);
                float b = Random.Range(0f, 1f);
                float a = Random.Range(0.5f, 1f);
                //property.baseColor = new Color(r, g, b, a);
                property.baseColor = Color.gray;
                property.cutoff = Random.Range(0.2f, 0.7f);
                property.metallic = 0.25f * col;
                property.smoothness = Mathf.Min(0.95f, 0.25f * row);
                property.RefreshBlock();
            }
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
