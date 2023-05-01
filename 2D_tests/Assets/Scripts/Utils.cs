using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static string CHARACTER = "Ball";
    public static float EPSILON = GameObject.Find(CHARACTER).transform.localScale.x / 2f;
    public static float DIRECTION_UPDATE_TIME = 0.1f;

}
