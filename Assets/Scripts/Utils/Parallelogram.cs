using UnityEngine;

public struct Parallelogram
{
    public Vector2 A;
    public Vector2 B;
    public Vector2 C;
    public Vector2 D;

    public Parallelogram(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        this.A = A;
        this.B = B;
        this.C = C;
        this.D = D;
    }

    public bool Contains(Vector2 point)
    {
        return PointInPolygon(point.x, point.y);
    }

    private bool PointInPolygon(float X, float Y)
    {
        var points = new Vector2[4] { this.A, this.B, this.C, this.D };

        int max_point = points.Length - 1;
        float total_angle = GetAngle(
            points[max_point].x, points[max_point].y,
            X, Y,
            points[0].x, points[0].y);

        for (int i = 0; i < max_point; i++)
        {
            total_angle += GetAngle(
                points[i].x, points[i].y,
                X, Y,
                points[i + 1].x, points[i + 1].y);
        }

        return (Mathf.Abs(total_angle) > 1);
    }

    private float GetAngle(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
    {
        float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);
        float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

        return (float)Mathf.Atan2(cross_product, dot_product);
    }

    private float DotProduct(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
    {
        float BAx = Ax - Bx;
        float BAy = Ay - By;
        float BCx = Cx - Bx;
        float BCy = Cy - By;

        return (BAx * BCx + BAy * BCy);
    }

    private float CrossProductLength(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
    {
        float BAx = Ax - Bx;
        float BAy = Ay - By;
        float BCx = Cx - Bx;
        float BCy = Cy - By;

        return (BAx * BCy - BAy * BCx);
    }


#if UNITY_EDITOR
    public static void DrawParallelogram(Parallelogram parallelogram)
    {
        var pointA = parallelogram.A;
        pointA.y = Screen.height - parallelogram.A.y;

        var pointB = parallelogram.B;
        pointB.y = Screen.height - parallelogram.B.y;

        var pointC = parallelogram.C;
        pointC.y = Screen.height - parallelogram.C.y;

        var pointD = parallelogram.D;
        pointD.y = Screen.height - parallelogram.D.y;

        GLHelper.DrawLine(pointA, pointB, Color.blue);
        GLHelper.DrawLine(pointB, pointC, Color.blue);
        GLHelper.DrawLine(pointC, pointD, Color.blue);
        GLHelper.DrawLine(pointD, pointA, Color.blue);
    }
#endif
}