using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");
    static int cutoffId = Shader.PropertyToID("_Cutoff");
    static MaterialPropertyBlock block;

    new Renderer renderer;

    public Color baseColor = Color.white;
    [Range(0,1)]
    public float cutoff = 0.5f;

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
        block.SetFloat(cutoffId, cutoff);
        renderer.SetPropertyBlock(block);
    }

    private void OnValidate()
    {
        RefreshBlock();
    }
}
