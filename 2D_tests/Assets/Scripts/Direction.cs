using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Direction
{
    public enum direction
    {
        Left,
        Up,
        Right,
        Down,
        None
    }
    public static Dictionary<direction, Vector3> directions = new Dictionary<direction, Vector3>() 
    {
        { direction.Left, Vector3.left } ,
        { direction.Up, Vector3.up },
        { direction.Right, Vector3.right },
        { direction.Down, Vector3.down },
        { direction.None, Vector3.zero }
    };

    public static direction Left   = direction.Left;
    public static direction Up     = direction.Up;
    public static direction Right  = direction.Right;
    public static direction Down   = direction.Down;
    public static direction None   = direction.None;

    public static direction getDirectionFrom2Points(Vector3 start_point, Vector3 end_point)
    {
        if(start_point.y == end_point.y && start_point.x > end_point.x)
        {
            return Left;
        }
        if(start_point.x == end_point.x && start_point.y < end_point.y)
        {
            return Up;
        }
        if(start_point.y == end_point.y && start_point.x < end_point.x)
        {
            return Right;
        }
        if(start_point.x == end_point.x && start_point.y > end_point.y)
        {
            return Down;
        }
        return None;
    }

    public static bool isVertical(direction d)
    {
        return d == Up || d == Down;
    }
    public static bool isHorizontal(direction d)
    {
        return d == Left || d == Right;
    }

}

