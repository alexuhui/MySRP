using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBatching : MonoBehaviour
{
    [SerializeField]
    PrimitiveType PrimitiveType = PrimitiveType.Cube;
    [SerializeField]
    int Count = 1023;


    private void Awake()
    {
        Shader shader = Shader.Find("Custom RP/Unlit");
        Material mat = new Material(shader);
        mat.SetColor("_BaseColor", Color.yellow);
        for (int i = 0; i < Count; i++)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType);
            go.name = PrimitiveType.ToString() + i;
            go.GetComponent<MeshRenderer>().sharedMaterial = mat;
            go.transform.SetParent(transform);
            go.transform.position = Random.insideUnitSphere * 10f + new Vector3(0, 0, 10);
        }
    }
}
