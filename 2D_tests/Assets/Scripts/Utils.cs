using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static string CHARACTER = "Ball";
    public static string BACKGROUND_STR = "Backgrounds";
    public static string BACKGROUND_MESH_STR = "BackgroundMesh";
    public static string BORDER_STR = "Borders";
    public static string ENEMIES_STR = "Enemies";
    public static string TRAILS_STR = "Trails";
    public static string LEVEL_STR = "LevelController";
    public static string STAR_STR = "Star";
    public static float EPSILON() => GameObject.Find(CHARACTER).transform.localScale.x / 2f;
    public static float OFFSET => 0.01f;
    public static float DIRECTION_UPDATE_TIME = 0.1f;
    public static float ADD_LINE_UPDATE_TIME = 0.05f;
    public static float OUTLINE_BORDER_THICKNESS = 0.1f;
    public static float ENNEMY_COLLIDER_UPDATE_TIME = Time.deltaTime * 2f;

    public static bool HAS_LOSE = true;
    public static bool HAS_WIN = true;
    public static bool GAME_STOPPED = false;

    public static bool SHADER_ON = true;

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

    public static bool fuzzyCompare(float val_1, float val_2)
    {
        return val_1 > val_2 - Utils.EPSILON() * 2f && val_1 < val_2 + Utils.EPSILON() * 2f;
    }
    public static void fixGivenPointsIfNeeded(Vector3 position_start, Vector3 position_end)
    {
        if (Utils.fuzzyCompare(position_start.x, position_end.x) && position_start.x != position_end.x)
        {
            Debug.Log("Fix Line Point x");
            position_end.x = position_start.x;
        }
        if (Utils.fuzzyCompare(position_start.y, position_end.y) && position_start.y != position_end.y)
        {
            Debug.Log("Fix Line Point y");
            position_end.y = position_start.y;
        }
    }
    public static List<Border> getBorders()
    {
        GameObject borders_go = GameObject.Find(Utils.BORDER_STR);
        List<Border> borders = new List<Border>();

        for(int i = 0; i < borders_go.transform.childCount; ++i)
        {
            Border border = borders_go.transform.GetChild(i).GetComponent<Border>();
            borders.Add(border);
        }

        return borders;
    }

    public static List<Background> getBackgrounds()
    {
        GameObject backgrounds_go = GameObject.Find(Utils.BACKGROUND_STR);
        List<Background> backgrounds = new List<Background>();

        for (int i = 0; i < backgrounds_go.transform.childCount; ++i)
        {
            Background background = backgrounds_go.transform.GetChild(i).GetComponent<Background>();
            backgrounds.Add(background);
        }

        return backgrounds;
    }

    public static Dictionary<string, Vector3> getOffsetPoints(Vector3 point)
    {
        return new Dictionary<string, Vector3>()
        {
            { "left", new Vector3(point.x - OFFSET, point.y, 0) },
            { "up", new Vector3(point.x, point.y + OFFSET, 0) },
            { "right", new Vector3(point.x + OFFSET, point.y, 0) },
            { "down", new Vector3(point.x, point.y - OFFSET, 0) },
            { "up-left", new Vector3(point.x - OFFSET, point.y + OFFSET, 0) },
            { "up-right", new Vector3(point.x + OFFSET, point.y + OFFSET, 0) },
            { "down-left", new Vector3(point.x - OFFSET, point.y - OFFSET, 0) },
            { "down-right", new Vector3(point.x + OFFSET, point.y - OFFSET, 0) }
        };
    }

    public static bool borderIsBetweenBackgrounds(Border border, Background bg_1, Background bg_2)
    {
        if (!backgroundAreConnected(bg_1, bg_2)) return false;

        Vector3 border_start_point = border.mStartPoint;
        Vector3 border_end_point = border.mEndPoint;

        Dictionary<string, Vector3> start_point_offsets = getOffsetPoints(border_start_point);
        Dictionary<string, Vector3> end_point_offsets = getOffsetPoints(border_end_point);

        if (bg_1.isHorizontalConnected(bg_2))
        {
            return (bg_1.containsEquals(start_point_offsets["up"]) && bg_2.containsEquals(start_point_offsets["down"]) || bg_1.containsEquals(start_point_offsets["down"]) && bg_2.containsEquals(start_point_offsets["up"]))
                || (bg_1.containsEquals(end_point_offsets["up"]) && bg_2.containsEquals(end_point_offsets["down"]) || bg_1.containsEquals(end_point_offsets["down"]) && bg_2.containsEquals(end_point_offsets["up"]));
        }

        if (bg_1.isVerticalConnected(bg_2))
        {
            return (bg_1.containsEquals(start_point_offsets["left"]) && bg_2.containsEquals(start_point_offsets["right"]) || bg_1.containsEquals(start_point_offsets["right"]) && bg_2.containsEquals(start_point_offsets["left"]))
                || (bg_1.containsEquals(end_point_offsets["left"]) && bg_2.containsEquals(end_point_offsets["right"]) || bg_1.containsEquals(end_point_offsets["right"]) && bg_2.containsEquals(end_point_offsets["left"]));
        }

        return true;
    }
    public void updateBackgroundMesh()
    {
        GameObject bg_mesh_go = GameObject.Find(BACKGROUND_MESH_STR);
        BackgroundMesh bg_mesh = bg_mesh_go.GetComponent<BackgroundMesh>();
        bg_mesh.mForceUpdate = true;
    }

    public float fade(float t)
    {
        float a = Mathf.Round(t);
        return Mathf.Pow((4 * t), 3) * (1 - a) + (1 - 4 * Mathf.Pow((1 - t), 3)) * a; // 4t^(3)(1-a)+(1-4(1-t)^(3))a
    }

}
