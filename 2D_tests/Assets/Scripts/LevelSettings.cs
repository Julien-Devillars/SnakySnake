using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSettings : MonoBehaviour
{
    public int number_enemies = 1;

    public bool random_direction;
    public bool random_position;
    public List<Vector2> enemies_direction;
    public List<Vector2> enemies_position;
    public Vector2 scale;
    public List<Vector2> star_positions;
    public int score;
}
