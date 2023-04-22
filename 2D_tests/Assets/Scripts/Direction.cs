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

}

