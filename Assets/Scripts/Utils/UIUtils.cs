using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtils
{
    public static Parallelogram GetScreenParallelogramFromRect(RectTransform rt, Camera camera)
    {
        // getting the world corners
        var corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        // getting the screen corners
        for (var i = 0; i < corners.Length; i++)
        {
            corners[i] = camera.WorldToScreenPoint(corners[i]);
        }

        return new Parallelogram(corners[0], corners[3], corners[2], corners[1]);
    }
}
