using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class Utils
{
    public static string CHARACTER = "Ball";
    public static string BACKGROUND_STR = "Backgrounds";
    public static string BACKGROUND_MESH_STR = "BackgroundMesh";
    public static string BACKGROUND_VIEW_STR = "BackgroundView";
    public static string BORDER_STR = "Borders";
    public static string ENEMIES_STR = "Enemies";
    public static string TRAILS_STR = "Trails";
    public static string LEVEL_STR = "LevelController";
    public static string STARS_STR = "Stars";
    public static string STAR_STR = "Star";
    public static float EPSILON() => GameObject.Find(CHARACTER).transform.localScale.x / 2f;
    public static float OFFSET => 0.01f;
    public static float DIRECTION_UPDATE_TIME = 0.02f;
    public static float ADD_LINE_UPDATE_TIME = 0.02f;
    public static float OUTLINE_BORDER_THICKNESS = 0.1f;
    public static float ENNEMY_COLLIDER_UPDATE_TIME = Time.deltaTime * 2f;

    public static bool HAS_LOSE = true;
    public static bool HAS_WIN = true;
    public static bool GAME_STOPPED = false;

    public static bool SHADER_ON = true;

    public static float ENEMY_DEFAULT_SCALE = 4f;
    public static float STAR_DEFAULT_SCALE = 2f;

    public static Vector3 U = Vector3.up;
    public static Vector3 UR = Vector3.up + Vector3.right;
    public static Vector3 R = Vector3.right;
    public static Vector3 DR = Vector3.down + Vector3.right;
    public static Vector3 D = Vector3.down;
    public static Vector3 DL = Vector3.down + Vector3.left;
    public static Vector3 L = Vector3.left;
    public static Vector3 UL = Vector3.up + Vector3.left;

    public static float rR = 0;
    public static float rUR = 45;
    public static float rU = 90;
    public static float rUL = 135;
    public static float rL = 180;
    public static float rDL = 225;
    public static float rD = 270;
    public static float rDR = 315;
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

    public static bool PointIsInBackgroundWithoutEnnemies(Vector3 point)
    {
        foreach (Background bg in getBackgrounds())
        {
            if (bg.contains(point) && !bg.hasEnemies())
            {
                return false;
            }
        }
        return true;
    }
    public static bool OnePointsAreInBackgroundsWithoutEnemies(List<Vector3> points)
    {
        foreach (Vector3 point in points)
        {
            bool found = PointIsInBackgroundWithoutEnnemies(point);
            if (found)
            {
                return true;
            }
        }

        return false;
    }
    public static bool allPointsAreInBackgroundsWithoutEnemies(List<Vector3> points)
    {
        foreach (Vector3 point in points)
        {
            bool found = PointIsInBackgroundWithoutEnnemies(point);
            if (found)
            {
                return false;
            }
        }

        return true;
    }


    public static Vector2 getRelativePositionFromPosition(int pos)
    {
        switch (pos)
        {
            case 1:
                return new Vector2(0.25f, 0.25f);
            case 2:
                return new Vector2(0.5f, 0.25f);
            case 3:
                return new Vector2(0.75f, 0.25f);
            case 4:
                return new Vector2(0.25f, 0.5f);
            case 5:
                return new Vector2(0.5f, 0.5f);
            case 6:
                return new Vector2(0.75f, 0.5f);
            case 7:
                return new Vector2(0.25f, 0.75f);
            case 8:
                return new Vector2(0.5f, 0.75f);
            case 9:
                return new Vector2(0.75f, 0.75f);
            case 0:
            default:
                return Vector2.zero;
        }
    }
    public static Vector2 getMidRelativePositionFromPosition(Vector2 v1, Vector2 v2, float interpolate = 0.5f)
    {

        return v1 * (1 - interpolate) + v2 * interpolate;
    }

    public static Vector2 getMidRelativePositionFromPosition(int pos_1, int pos_2, float interpolate = 0.5f)
    {
        Vector2 v1 = getRelativePositionFromPosition(pos_1);
        Vector2 v2 = getRelativePositionFromPosition(pos_2);

        return getMidRelativePositionFromPosition(v1, v2, interpolate);
    }
    public static Vector2 getMidRelativePositionFromPosition(Vector2 v1, int pos_2, float interpolate = 0.5f)
    {
        Vector2 v2 = getRelativePositionFromPosition(pos_2);

        return getMidRelativePositionFromPosition(v1, v2, interpolate);
    }
    public static Vector3 getMidRelativePositionFromPosition(int pos_1, Vector2 v2, float interpolate = 0.5f)
    {
        Vector2 v1 = getRelativePositionFromPosition(pos_1);

        return getMidRelativePositionFromPosition(v1, v2, interpolate);
    }
    //This returns the angle in radians
    public static float AngleInRad(Vector2 vec1, Vector2 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    //This returns the angle in degrees
    public static float AngleInDeg(Vector2 vec1, Vector2 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }
    public static float getAngleFromPosition(int pos_origin, int pos_1, int pos_2)
    {

        float scale = 10f;
        Vector2 v0 = getRelativePositionFromPosition(pos_origin) * scale;
        Vector2 v1 = getRelativePositionFromPosition(pos_1) * scale;
        Vector2 v2 = getRelativePositionFromPosition(pos_2) * scale;
        return Vector2.Angle(v1 - v0, v2 - v0);
        //return AngleInDeg(v1 - v0, v2 - v0);
    }
    public static List<int> Linker(params int[] links) => links.ToList<int>();

    public static string getTimeFromFloat(float time)
    {
        if(time < 0)
        {
            return "00:00:000";
        }
        TimeSpan ts = TimeSpan.FromSeconds(time);
        string time_str = string.Format("{0:00}:{1:00}:{2:000}", (int)ts.TotalMinutes, ts.Seconds, ts.Milliseconds);
        return time_str;
    }

    public static bool checkNoBorderBetwennBackground(Background bg_1, Background bg_2, List<Border> borders)
    {
        Vector3 center_1 = bg_1.getCenterPoint();
        Vector3 center_2 = bg_2.getCenterPoint();
        Vector3 mix_point_1 = new Vector3(center_1.x, center_2.y);
        Vector3 mix_point_2 = new Vector3(center_2.x, center_1.y);
        bool c1_is_connected = true;
        bool c2_is_connected = true;
        foreach (Border border in borders)
        {
            bool mix1_c1_intersect = !Utils.intersect(border.mStartPoint, border.mEndPoint, mix_point_1, center_1);
            bool mix1_c2_intersect = !Utils.intersect(border.mStartPoint, border.mEndPoint, mix_point_1, center_2);
            bool mix2_c1_intersect = !Utils.intersect(border.mStartPoint, border.mEndPoint, mix_point_2, center_1);
            bool mix2_c2_intersect = !Utils.intersect(border.mStartPoint, border.mEndPoint, mix_point_2, center_2);

            bool mix1_is_ok = mix1_c1_intersect && mix1_c2_intersect;
            bool mix2_is_ok = mix2_c1_intersect && mix2_c2_intersect;

            c1_is_connected &= mix1_is_ok;
            c2_is_connected &= mix2_is_ok;
        }

        return c1_is_connected || c2_is_connected;
    }
}
