using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PerObjectMaterialProperties : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");
    static MaterialPropertyBlock block;

    new Renderer renderer;
    bool bIsAwaked = false;

    public Color baseColor = Color.white;

    private void Awake()
    {
        bIsAwaked = true;
        renderer = GetComponent<Renderer>();
        RefreshBlock();
    }

    private void RefreshBlock()
    {
        if(block == null)
        {
            block = new MaterialPropertyBlock();
        }    
        block.SetColor(baseColorId, baseColor);
        renderer.SetPropertyBlock(block);
    }

    private void OnValidate()
    {
        if(bIsAwaked)
            RefreshBlock();
    }
}
