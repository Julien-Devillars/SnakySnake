using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    public Vector2 position;
    public Vector2 direction;
    public float scale;

    public ObjectInfo(Vector2 _position, Vector2 _direction, float _scale = 1f)
    {
        position = _position;
        direction = _direction;
        scale = _scale;
    }
    public ObjectInfo(int _position, Vector2 _direction, float _scale = 1f)
    {
        position = Utils.getRelativePositionFromPosition(_position);
        direction = _direction;
        scale = _scale;
    }

}

public enum EnemyType
{
    Basic,
    Circle,
    Follower,
    Flyer,
    Driller
}

public class EnemyInfo : ObjectInfo 
{
    public EnemyType type;

    public EnemyInfo(EnemyType _type, Vector2 _position, Vector2 _direction, float _scale) : base(_position, _direction, _scale)
    {
        type = _type;
    }
    public EnemyInfo(EnemyType _type, int _position, Vector2 _direction, float _scale) : base(_position, _direction, _scale)
    {
        type = _type;
    }
}
public class EnemyCircleInfo : EnemyInfo
{
    public float mRotateSpeed;
    public float mAttackSpeed;
    public float mStartRotation;
    public EnemyCircleInfo(Vector2 _position, Vector2 _direction, float _scale, float _rotation_speed, float _rotation_position, float _attack_speed) : base(EnemyType.Circle, _position, _direction, _scale)
    {
        mRotateSpeed = _rotation_speed;
        mAttackSpeed = _attack_speed;
        mStartRotation = _rotation_position;
    }
    public EnemyCircleInfo(int _position, Vector2 _direction, float _scale, float _rotation_speed, float _rotation_position, float _attack_speed) : base(EnemyType.Circle, _position, _direction, _scale)
    {
        mRotateSpeed = _rotation_speed;
        mAttackSpeed = _attack_speed;
        mStartRotation = _rotation_position;
    }
}

public class StarInfo : ObjectInfo
{
    public StarInfo(Vector2 _position, float _scale = 1f) : base(_position, new Vector2(0, 0), _scale)
    { }
    public StarInfo(int _position, float _scale = 1f) : base(_position, new Vector2(0, 0), _scale)
    { 
    }
}

public class Level
{
    public string mLevelName;
    public int mGoalScore;
    public List<EnemyInfo> mEnemies;
    public List<StarInfo> mStars;

    int mBestReach = 0;
    public Level(string name, int goal)
    {
        mLevelName = name;
        mGoalScore = goal;
        mEnemies = new List<EnemyInfo>();
        mStars = new List<StarInfo>();
    }

    public void addEnemy(EnemyInfo enemy)
    {
        mEnemies.Add(enemy);
    }
    public void addStar(StarInfo star)
    {
        mStars.Add(star);
    }
}

public class Levels
{
    public string levels_name;
    public List<Level> levels;

    public static Level level_1_1()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 4f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 4f));
        return level;
    }
    public static Level level_1_2()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Vector2.up * 7f, Utils.ENEMY_DEFAULT_SCALE * 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Vector2.right * 7f, Utils.ENEMY_DEFAULT_SCALE * 2f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_1_3()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.75f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.25f), Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.5f), Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.5f), Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_4()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.25f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_5()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_1_6()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_7()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector2.right + Vector2.up) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector2.left + Vector2.up) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector2.right + Vector2.down) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector2.left + Vector2.down) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_8()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector2.right + Vector2.up) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector2.left + Vector2.up) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector2.right + Vector2.down) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector2.left + Vector2.down) * 3f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_9()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.2f), (Vector2.left) * 1f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.3f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.4f), (Vector2.left) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.6f), (Vector2.left) * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.7f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.8f), (Vector2.left) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.2f), (Vector2.right) * 1f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.3f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.4f), (Vector2.right) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.6f), (Vector2.right) * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.7f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.8f), (Vector2.right) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_10()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_11()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0f,1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));


        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_12()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_13()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_14()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE / 2f));
        return level;
    }
    public static Level level_1_15()
    {
        // Level 1
        Level level = new Level("Basic", 50);


        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.2f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.3f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.4f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (Vector2.left) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.6f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.7f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.8f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.2f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.3f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.4f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (Vector2.right) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.6f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.7f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.8f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.2f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.3f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.4f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (Vector2.up) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.6f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.7f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.8f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.2f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.3f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.4f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (Vector2.down) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.6f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.7f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.8f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }

    //
    //
    //public static Level level_1()
    //{
    //    // Level 1
    //    Level level = new Level("Basic", 50);
    //
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 4f));
    //
    //    level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 4f));
    //    return level;
    //}
    //public static Level level_2()
    //{
    //    Level level = new Level("Plus", 45);
    //
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.25f), new Vector2(0f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.75f), new Vector2(0f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.25f), new Vector2(0f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.75f), new Vector2(0f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //
    //    StarInfo star_1 = new StarInfo(new Vector2(0.5f, 0.25f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_3 = new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE);
    //    level.addStar(star_1);
    //    level.addStar(star_2);
    //    level.addStar(star_3);
    //    return level;
    //}
    //public static Level level_3()
    //{
    //    // Level 1
    //    Level level = new Level("Cross", 45);
    //
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.25f), new Vector2(1f, -1f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.75f), new Vector2(1f, 1f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.25f), new Vector2(-1f, -1f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.75f), new Vector2(-1f, -1f), Utils.ENEMY_DEFAULT_SCALE));
    //
    //    StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    level.addStar(star_1);
    //    level.addStar(star_2);
    //    level.addStar(star_3);
    //    return level;
    //}
    //public static Level level_4()
    //{
    //    // Level 1
    //    Level level = new Level("Circle", 50);
    //
    //    level.addEnemy(new EnemyCircleInfo(new Vector2(0.25f, 0.25f), new Vector2(5f, 0f), Utils.ENEMY_DEFAULT_SCALE, 150, 0, 1));
    //    level.addEnemy(new EnemyCircleInfo(new Vector2(0.5f, 0.25f), new Vector2(-4f, 0f), Utils.ENEMY_DEFAULT_SCALE, 40, 180, 5));
    //    level.addEnemy(new EnemyCircleInfo(new Vector2(0.75f, 0.25f), new Vector2(3f, 3f), Utils.ENEMY_DEFAULT_SCALE, 90, 225, 10));
    //
    //    StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    level.addStar(star_1);
    //    level.addStar(star_2);
    //    level.addStar(star_3);
    //    return level;
    //}
    //public static Level level_5()
    //{
    //    // Level 1
    //    Level level = new Level("Follower", 80);
    //
    //    level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.25f, 0.25f), new Vector2(10f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.25f, 0.25f), new Vector2(3f, 3f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.25f, 0.25f), new Vector2(-4f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //
    //    StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    level.addStar(star_1);
    //    level.addStar(star_2);
    //    level.addStar(star_3);
    //    return level;
    //}
    //public static Level level_6()
    //{
    //    // Level 1
    //    Level level = new Level("Flyer", 80);
    //
    //    level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.25f, 0.25f), new Vector2(10f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.25f, 0.25f), new Vector2(30f, 30f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.25f), new Vector2(20f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //
    //    StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE);
    //    level.addStar(star_1);
    //    level.addStar(star_2);
    //    level.addStar(star_3);
    //    return level;
    //}
    //public static Level level_7()
    //{
    //    // Level 1
    //    Level level = new Level("Driller", 80);
    //
    //    level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), new Vector2(10f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), new Vector2(-10f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), new Vector2(0f, 10f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), new Vector2(0f, -10f), Utils.ENEMY_DEFAULT_SCALE));
    //    level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), Utils.ENEMY_DEFAULT_SCALE));
    //
    //    level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
    //    //level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
    //    level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
    //    level.addStar(new StarInfo(new Vector2(0.5f, 0.25f), Utils.STAR_DEFAULT_SCALE));
    //    level.addStar(new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE));
    //    return level;
    //}

    public Levels()
    {
        levels = new List<Level>();
    }

    public void addLevel(Level level)
    {
        levels.Add(level);
    }

}


public class Worlds
{
    public static List<Levels> worlds;

    private static Levels world_1()
    {
        Levels levels = new Levels();

        levels.levels_name = "World 1";
        levels.addLevel(Levels.level_1_1());
        levels.addLevel(Levels.level_1_2());
        levels.addLevel(Levels.level_1_3());
        levels.addLevel(Levels.level_1_4());
        levels.addLevel(Levels.level_1_5());
        levels.addLevel(Levels.level_1_6());
        levels.addLevel(Levels.level_1_7());
        levels.addLevel(Levels.level_1_8());
        levels.addLevel(Levels.level_1_9());
        levels.addLevel(Levels.level_1_10());
        levels.addLevel(Levels.level_1_11());
        levels.addLevel(Levels.level_1_12());
        levels.addLevel(Levels.level_1_13());
        levels.addLevel(Levels.level_1_14());
        levels.addLevel(Levels.level_1_15());

        return levels;
    }
    /*
    private static Levels world_2()
    {
        Levels levels = new Levels();

        levels.levels_name = "World 2";
        levels.addLevel(Levels.level_7());
        levels.addLevel(Levels.level_6());
        levels.addLevel(Levels.level_5());
        levels.addLevel(Levels.level_4());
        levels.addLevel(Levels.level_3());
        levels.addLevel(Levels.level_2());
        levels.addLevel(Levels.level_1());

        return levels;
    }*/

    public static Level getLevel(int world, int level)
    {
        return worlds[world].levels[level];
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void createWorlds()
    {
        worlds = new List<Levels>();

        worlds.Add(world_1());
        //worlds.Add(world_2());
    }
}