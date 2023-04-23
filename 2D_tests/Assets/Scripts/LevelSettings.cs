using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSettings
{
    public int number_enemies = 1;

    public bool random_direction;
    public bool random_position;
    public List<Vector2> enemies_direction;
    public List<Vector2> enemies_position;
}
