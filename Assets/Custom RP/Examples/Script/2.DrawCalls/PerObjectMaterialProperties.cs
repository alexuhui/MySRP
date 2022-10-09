using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    static int 
        baseColorId = Shader.PropertyToID("_BaseColor"),
        cutoffId = Shader.PropertyToID("_Cutoff"),
        metallicId = Shader.PropertyToID("_Metallic"),
        smoothnessId = Shader.PropertyToID("_Smoothness");

    static MaterialPropertyBlock block;

    new Renderer renderer;

    public Color baseColor = Color.white;
    [Range(0,1)]
    public float cutoff = 0.5f;
    [Range(0, 1)]
    public float metallic = 0.0f;
    [Range(0, 1)]
    public float smoothness = 0.5f;

    private void Awake()
    {
        RefreshBlock();
    }

    private void OnValidate()
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
        block.SetFloat(metallicId, metallic);
        block.SetFloat(smoothnessId, smoothness);
        renderer.SetPropertyBlock(block);
    }  
}
