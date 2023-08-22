using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    public Vector2 position;
    public Vector2 direction;
    public Vector2 scale;

    public ObjectInfo(Vector2 _position, Vector2 _direction, Vector2 _scale)
    {
        position = _position;
        direction = _direction;
        scale = _scale;
    }

}

public enum EnemyType
{
    Basic
}

public class EnemyInfo : ObjectInfo 
{
    public EnemyType type;

    public EnemyInfo(EnemyType _type, Vector2 _position, Vector2 _direction, Vector2 _scale) : base(_position, _direction, _scale)
    {
        type = _type;
    }
}
public class StarInfo : ObjectInfo
{
    public StarInfo(Vector2 _position, Vector2 _scale) : base(_position, new Vector2(0, 0), _scale)
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
    public static List<Level> levels;

    private static Level level_1()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(0f, 1f), new Vector2(1f, 1f)));

        StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.25f), new Vector2(1f, 1f));
        StarInfo star_2 = new StarInfo(new Vector2(0.75f, 0.25f), new Vector2(1f, 1f));
        StarInfo star_3 = new StarInfo(new Vector2(0.5f, 0.75f), new Vector2(1f, 1f));
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }
    private static Level level_2()
    {
        // Level 1
        Level level = new Level("Plus", 45);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(1f, 0f), new Vector2(1f, 1f)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(-1f, 0f), new Vector2(1f, 1f)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(0f, 1f), new Vector2(1f, 1f)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), new Vector2(0f, -1f), new Vector2(1f, 1f)));

        StarInfo star_1 = new StarInfo(new Vector2(0.5f, 0.25f), new Vector2(1f, 1f));
        StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), new Vector2(1f, 1f));
        StarInfo star_3 = new StarInfo(new Vector2(0.5f, 0.75f), new Vector2(1f, 1f));
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }
    private static Level level_3()
    {
        // Level 1
        Level level = new Level("Cross", 45);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.25f), new Vector2(1f, -1f), new Vector2(1f, 1f)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.75f), new Vector2(1f, 1f), new Vector2(1f, 1f)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.25f), new Vector2(-1f, -1f), new Vector2(1f, 1f)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.75f), new Vector2(-1f, -1f), new Vector2(1f, 1f)));

        StarInfo star_1 = new StarInfo(new Vector2(0.25f, 0.5f), new Vector2(1f, 1f));
        StarInfo star_2 = new StarInfo(new Vector2(0.5f, 0.5f), new Vector2(1f, 1f));
        StarInfo star_3 = new StarInfo(new Vector2(0.75f, 0.5f), new Vector2(1f, 1f));
        level.addStar(star_1);
        level.addStar(star_2);
        level.addStar(star_3);
        return level;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void createLevels()
    {
        levels = new List<Level>();

        levels.Add(level_1());
        levels.Add(level_2());
        levels.Add(level_3());
    }
}

