using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static string CHARACTER = "Ball";
    public static string BACKGROUND_STR = "Backgrounds";
    public static string ENEMIES_STR = "Enemies";
    public static float EPSILON() => GameObject.Find(CHARACTER).transform.localScale.x / 2f;
    public static float DIRECTION_UPDATE_TIME = 0.1f;
    public static float ADD_LINE_UPDATE_TIME = 0.05f;

    public static bool HAS_SCORE_ACTIVATED = true;
    public static bool HAS_ENEMY_COLLISION = true;

    public static List<Vector3> getIntermediatePointFromTrail(Border border)
    {
        List<Vector3> points = new List<Vector3>();

        Vector3 start_point = border.getStartPoint();
        Vector3 last_point = border.getLastPoint();
        Vector3 direction = last_point - start_point;
        direction = direction.normalized;
        
        points.Add(start_point);

        Vector3 current_point = start_point + direction * EPSILON();
        

        while(border.onBorder(current_point))
        {
            points.Add(current_point);
            current_point = current_point + direction * EPSILON();
        }

        points.Add(last_point);

        return points;
    }

    public static bool ccw(Vector3 A, Vector3 B, Vector3 C)
    {
        return (C.y - A.y) * (B.x - A.x) > (B.y - A.y) * (C.x - A.x);
    }

    //Return true if line segments AB and CD intersect
    public static bool intersect(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        return ccw(A, C, D) != ccw(B, C, D) && ccw(A, B, C) != ccw(A, B, D);
    }

    public static bool backgroundAreConnected(Background bg_1, Background bg_2)
    {
        return bg_1.mMinBorderPos.x == bg_2.mMaxBorderPos.x
                && (bg_1.mMinBorderPos.y < bg_2.mMaxBorderPos.y || bg_1.mMaxBorderPos.y > bg_2.mMinBorderPos.y)
            || bg_1.mMinBorderPos.y == bg_2.mMaxBorderPos.y
                && (bg_1.mMinBorderPos.x < bg_2.mMaxBorderPos.x || bg_1.mMaxBorderPos.x > bg_2.mMinBorderPos.x);
    }

    public static void propagateBackgrounds(List<Background> backgrounds)
    {
        List<Background> fifo = new List<Background>();
        foreach (Background bg in backgrounds)
        {
            fifo.Add(bg);
        }
        for(int i = 0; i < fifo.Count; ++i)
        {
            Background bg = fifo[i];
            List<Background> connections = new List<Background>();
            connections.AddRange(bg.mConnectedBackground);

            foreach (Background connection_source in connections)
            {
                foreach (Background connection_target in connections)
                {
                    bool added = connection_source.addConnection(connection_target);
                    if (added)
                    {
                        fifo.Add(connection_source);
                    }
                }
            }
        }
        
    }

}
