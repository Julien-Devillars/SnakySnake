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
}
public class StarInfo : ObjectInfo
{
    public StarInfo(Vector2 _position, float _scale = 1f) : base(_position, new Vector2(0, 0), _scale)
    {}
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
    public List<Level> levels;

    public static Level level_1()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(0f, 1f), Utils.ENEMY_DEFAULT_SCALE));

        StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.25f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_2 = new StarInfo(new Vector2(0.75f, 0.25f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_3 = new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE);
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }
    public static Level level_2()
    {
        // Level 1
        Level level = new Level("Plus", 45);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(1f, 0f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(-1f, 0f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(0f, 1f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(0f, -1f), Utils.ENEMY_DEFAULT_SCALE));

        StarInfo star_1 = new StarInfo(new Vector2(0.5f, 0.25f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_3 = new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE);
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }
    public static Level level_3()
    {
        // Level 1
        Level level = new Level("Cross", 45);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.25f), new Vector2(1f, -1f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.75f), new Vector2(1f, 1f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.25f), new Vector2(-1f, -1f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.75f), new Vector2(-1f, -1f), Utils.ENEMY_DEFAULT_SCALE));

        StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }
    public static Level level_4()
    {
        // Level 1
        Level level = new Level("Circle", 50);

        level.addEnemy(new EnemyCircleInfo(new Vector2(0.25f, 0.25f), new Vector2(5f, 0f), Utils.ENEMY_DEFAULT_SCALE, 150, 0, 1));
        level.addEnemy(new EnemyCircleInfo(new Vector2(0.5f, 0.25f), new Vector2(-4f, 0f), Utils.ENEMY_DEFAULT_SCALE, 40, 180, 5));
        level.addEnemy(new EnemyCircleInfo(new Vector2(0.75f, 0.25f), new Vector2(3f, 3f), Utils.ENEMY_DEFAULT_SCALE, 90, 225, 10));

        StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }
    public static Level level_5()
    {
        // Level 1
        Level level = new Level("Follower", 80);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.25f, 0.25f), new Vector2(10f, 0f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.25f, 0.25f), new Vector2(3f, 3f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.25f, 0.25f), new Vector2(-4f, 0f), Utils.ENEMY_DEFAULT_SCALE));

        StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }
    public static Level level_6()
    {
        // Level 1
        Level level = new Level("Flyer", 80);

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.25f, 0.25f), new Vector2(10f, 0f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.25f, 0.25f), new Vector2(30f, 30f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.25f), new Vector2(20f, 0f), Utils.ENEMY_DEFAULT_SCALE));

        StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE);
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }
    public static Level level_7()
    {
        // Level 1
        Level level = new Level("Driller", 80);

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), new Vector2(10f, 0f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), new Vector2(-10f, 0f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), new Vector2(0f, 10f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), new Vector2(0f, -10f), Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        //level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }

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

        levels.addLevel(Levels.level_1());
        levels.addLevel(Levels.level_2());
        levels.addLevel(Levels.level_3());
        levels.addLevel(Levels.level_4());
        levels.addLevel(Levels.level_5());
        levels.addLevel(Levels.level_6());
        levels.addLevel(Levels.level_7());

        return levels;
    }

    private static Levels world_2()
    {
        Levels levels = new Levels();

        levels.addLevel(Levels.level_7());
        levels.addLevel(Levels.level_6());
        levels.addLevel(Levels.level_5());
        levels.addLevel(Levels.level_4());
        levels.addLevel(Levels.level_3());
        levels.addLevel(Levels.level_2());
        levels.addLevel(Levels.level_1());

        return levels;
    }

    public static Level getLevel(int world, int level)
    {
        return worlds[world].levels[level];
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void createWorlds()
    {
        worlds = new List<Levels>();

        worlds.Add(world_1());
        worlds.Add(world_2());
    }
}