using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer 
{
    ScriptableRenderContext context;
    Camera camera;

    const string bufferName = "Render Camera";
    CommandBuffer buffer = new CommandBuffer() {name = bufferName};

    CullingResults cullingResults;

    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;

        PrepareBuffer();
        PrepareForSceneWindow();
        if (!Cull()) return;

        Setup();

        DrawVisibleGeometry();
        DrawUnsupportedShaders();
        DrawGizmos();

        Submit();
    }

    bool Cull()
    {
        if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            cullingResults = context.Cull(ref p);
            return true;
        }
        return false;
    }

    void Setup()
    {
        context.SetupCameraProperties(camera);

        CameraClearFlags flags = camera.clearFlags;
        buffer.ClearRenderTarget(
            flags <= CameraClearFlags.Depth, 
            flags != CameraClearFlags.Nothing,
            flags == CameraClearFlags.Color ? camera.backgroundColor.linear : Color.clear
        );
        //buffer.ClearRenderTarget(true, true, Color.clear);
        buffer.BeginSample(SampleName);
        //buffer.ClearRenderTarget(true, true, Color.clear);
        ExecuteBuffer();
        //context.SetupCameraProperties(camera);
    }
    

    void DrawVisibleGeometry()
    {
        // 首先渲染不透明物体
        SortingSettings sortingSettings = new SortingSettings(camera);
        sortingSettings.criteria = SortingCriteria.CommonOpaque;

        // 这么写是不对的，看了源码才发现带参数的构造函数里面很多初始化代码
        // 最初这么写的想法是：当有多个unlitShaderTagId时直接通过for循环SetShaderPassName
        // 看起来工整一些
        //DrawingSettings drawingSettings = new DrawingSettings();
        //drawingSettings.sortingSettings = sortingSettings;
        //drawingSettings.SetShaderPassName(0, unlitShaderTagId);
        var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings);

        FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);

        // 然后渲染天空盒
        context.DrawSkybox(camera);

        // 最后渲染透明物体
        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
    }

    void Submit()
    {
        buffer.EndSample(SampleName);
        ExecuteBuffer();
        context.Submit();
    }

    void ExecuteBuffer()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }
}
