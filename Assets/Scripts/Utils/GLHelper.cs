using UnityEngine;

public class GLHelper
{
    protected static Material lineMaterial;   

    public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
    {
        if (!lineMaterial)
        {
            lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            lineMaterial.SetInt("_ZWrite", 0);
        }

        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(color);
        GL.Vertex3(pointA.x, pointA.y, 0);
        GL.Vertex3(pointB.x, pointB.y, 0);
        GL.End();
    }
}