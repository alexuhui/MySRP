using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PerObjectMaterialProperties : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");
    static MaterialPropertyBlock block;

    new Renderer renderer;

    public Color baseColor = Color.white;

    private void Awake()
    {
        RefreshBlock();
    }

    public void RefreshBlock()
    {
        if(renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }

        if (block == null)
        {
            block = new MaterialPropertyBlock();
        }

        renderer.GetPropertyBlock(block);
        block.SetColor(baseColorId, baseColor);
        renderer.SetPropertyBlock(block);
    }

    private void OnValidate()
    {
        RefreshBlock();
    }
}
