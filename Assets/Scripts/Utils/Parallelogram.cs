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
        //// b a c
        //var xb = this.B.x - this.A.x;
        //var yb = this.B.y - this.A.y;
        //var xc = this.C.x - this.A.x;
        //var yc = this.C.y - this.A.y;
        //var xp = point.x - this.A.x;
        //var yp = point.y - this.A.y;
        //var d = xb * yc - yb * xc;

        //if (d != 0)
        //{
        //    var oned = 1 / d;
        //    var bb = (xp * yc - xc * yp) * oned;
        //    var cc = (xb * yp - xp * yb) * oned;

        //    return (bb >= 0f) && (cc >= 0f) && (bb <= 1f) && (cc <= 1f);
        //}

        //return false;

        return PointInPolygon(point.x, point.y);
    }

    public bool PointInPolygon(float X, float Y)
    {
        var Points = new Vector2[4] { this.A, this.B, this.C, this.D };
        // Get the angle between the point and the
        // first and last vertices.
        int max_point = Points.Length - 1;
        float total_angle = GetAngle(
            Points[max_point].x, Points[max_point].y,
            X, Y,
            Points[0].x, Points[0].y);

        // Add the angles from the point
        // to each other pair of vertices.
        for (int i = 0; i < max_point; i++)
        {
            total_angle += GetAngle(
                Points[i].x, Points[i].y,
                X, Y,
                Points[i + 1].x, Points[i + 1].y);
        }

        // The total angle should be 2 * PI or -2 * PI if
        // the point is in the polygon and close to zero
        // if the point is outside the polygon.
        // The following statement was changed. See the comments.
        //return (Math.Abs(total_angle) > 0.000001);
        return (Mathf.Abs(total_angle) > 1);
    }

    public static float GetAngle(float Ax, float Ay,
    float Bx, float By, float Cx, float Cy)
    {
        // Get the dot product.
        float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

        // Get the cross product.
        float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

        // Calculate the angle.
        return (float)Mathf.Atan2(cross_product, dot_product);
    }

    private static float DotProduct(float Ax, float Ay,
    float Bx, float By, float Cx, float Cy)
    {
        // Get the vectors' coordinates.
        float BAx = Ax - Bx;
        float BAy = Ay - By;
        float BCx = Cx - Bx;
        float BCy = Cy - By;

        // Calculate the dot product.
        return (BAx * BCx + BAy * BCy);
    }

    public static float CrossProductLength(float Ax, float Ay,
    float Bx, float By, float Cx, float Cy)
    {
        // Get the vectors' coordinates.
        float BAx = Ax - Bx;
        float BAy = Ay - By;
        float BCx = Cx - Bx;
        float BCy = Cy - By;

        // Calculate the Z coordinate of the cross product.
        return (BAx * BCy - BAy * BCx);
    }



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
}