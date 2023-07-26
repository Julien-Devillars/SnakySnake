using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Kevin,
    Bob
}

[System.Serializable]
public class EnemySettings
{
    public EnemyType type;
    public Vector2 position;
    public Vector2 direction;
    public bool random_direction;
    public bool random_position;
    public float scale;
}

[System.Serializable]
public class LevelSettings : MonoBehaviour
{
    //public int number_enemies = 1;

    //public bool random_direction;
    //public bool random_position;
    //public List<Vector2> enemies_direction;
    //public List<Vector2> enemies_position;
    public List<Vector2> star_positions;
    public int score;


    //public List<ennemy_type> enemies_type;
    public List<EnemySettings> enemy_settings;
}
