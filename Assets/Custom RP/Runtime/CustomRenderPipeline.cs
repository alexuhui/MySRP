using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();


    public CustomRenderPipeline()
    {
        //开启SRP Batcher
        GraphicsSettings.useScriptableRenderPipelineBatching = true;
    }


    /// <summary>
    /// 根据引擎给的上下文，遍历相机进行渲染
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cameras"></param>
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            renderer.Render(context, camera);
        }
    }
}
