using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static string CHARACTER = "Ball";
    public static float EPSILON = GameObject.Find(CHARACTER).transform.localScale.x / 2f;
    public static float DIRECTION_UPDATE_TIME = 0.1f;

    public static List<Vector3> getIntermediatePointFromTrail(Border border)
    {
        List<Vector3> points = new List<Vector3>();

        Vector3 start_point = border.getStartPoint();
        Vector3 last_point = border.getLastPoint();
        Vector3 direction = last_point - start_point;
        direction = direction.normalized;
        
        points.Add(start_point);

        Vector3 current_point = start_point + direction * EPSILON;
        

        while(border.onBorder(current_point))
        {
            points.Add(current_point);
            current_point = current_point + direction * EPSILON;
        }

        points.Add(last_point);

        return points;
    }

}
