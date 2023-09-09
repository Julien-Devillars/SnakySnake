using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public bool mInverse;

    public enum Form
    {
        Line,
        Triangle,
        Square,
        Pentagone,
        Hexagone,
        Octogone,
        iLine,
        iTriangle,
        iSquare,
        iPentagone,
        iHexagone,
        iOctogone
    }
    public EnemyCircleInfo(Vector2 _position, Vector2 _direction, float _scale, float _rotation_position, float _rotation_speed, float _attack_speed, bool _inverse = false) : base(EnemyType.Circle, _position, _direction, _scale)
    {
        mRotateSpeed = _rotation_speed;
        mAttackSpeed = _attack_speed;
        mStartRotation = _rotation_position;
        mInverse = _inverse;
    }
    public EnemyCircleInfo(int _position, Vector2 _direction, float _scale, float _rotation_position, float _rotation_speed, float _attack_speed, bool _inverse = false) : base(EnemyType.Circle, _position, _direction, _scale)
    {
        mRotateSpeed = _rotation_speed;
        mAttackSpeed = _attack_speed;
        mStartRotation = _rotation_position;
        mInverse = _inverse;
    }
    public EnemyCircleInfo(int _position, Vector2 _direction, float _scale, float _rotation_position, Form _form, float _form_scale, bool _inverse = false) : base(EnemyType.Circle, _position, _direction, _scale)
    {
        switch(_form)
        {
            case Form.Line:
                mRotateSpeed = 180;
                break;
            case Form.Triangle:
                mRotateSpeed = 120;
                break;
            case Form.Square:
                mRotateSpeed = 90;
                break;
            case Form.Pentagone:
                mRotateSpeed = 72;
                break;
            case Form.Hexagone:
                mRotateSpeed = 60;
                break;
            case Form.Octogone:
                mRotateSpeed = 45;
                break;
            case Form.iLine:
                mRotateSpeed = -180;
                break;
            case Form.iTriangle:
                mRotateSpeed = -120;
                break;
            case Form.iSquare:
                mRotateSpeed = -90;
                break;
            case Form.iPentagone:
                mRotateSpeed = -72;
                break;
            case Form.iHexagone:
                mRotateSpeed = -60;
                break;
            case Form.iOctogone:
                mRotateSpeed = -45;
                break;
            default:
                break;
        }

        mAttackSpeed = 1f / _form_scale;
        mRotateSpeed *= _form_scale;
        mStartRotation = _rotation_position;
        mInverse = _inverse;
    }
    public EnemyCircleInfo(Vector2 _position, Vector2 _direction, float _scale, float _rotation_position, Form _form, float _form_scale, bool _inverse = false) : base(EnemyType.Circle, _position, _direction, _scale)
    {
        switch (_form)
        {
            case Form.Line:
                mRotateSpeed = 180;
                break;
            case Form.Triangle:
                mRotateSpeed = 120;
                break;
            case Form.Square:
                mRotateSpeed = 90;
                break;
            case Form.Pentagone:
                mRotateSpeed = 72;
                break;
            case Form.Hexagone:
                mRotateSpeed = 60;
                break;
            case Form.Octogone:
                mRotateSpeed = 45;
                break;
            case Form.iLine:
                mRotateSpeed = -180;
                break;
            case Form.iTriangle:
                mRotateSpeed = -120;
                break;
            case Form.iSquare:
                mRotateSpeed = -90;
                break;
            case Form.iPentagone:
                mRotateSpeed = -72;
                break;
            case Form.iHexagone:
                mRotateSpeed = -60;
                break;
            case Form.iOctogone:
                mRotateSpeed = -45;
                break;
            default:
                break;
        }

        mAttackSpeed = 1f / _form_scale;
        mRotateSpeed *= _form_scale;
        mStartRotation = _rotation_position;
        mInverse = _inverse;
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
    public string mLevelHelper = "";
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


    public static Level level_1_one_star()
    {
        // Level 1
        Level level = new Level("level_1_one_star", 50);
        level.mLevelHelper = "Move with ZQSD.";
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_1_only_stars()
    {
        // Level 1
        Level level = new Level("level_1_only_stars", 50);

        level.mLevelHelper = "Catch all the stars to end the level.";
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE / 2f));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_one_slow_enemy()
    {
        // Level 1
        Level level = new Level("level_1_only_star", 50);

        float speed = 7f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "You lose if someone hit your trail !";
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));

        return level;
    }

    public static Level level_1_2_medium_enemy_horizontal()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 8f;
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        return level;
    }

    public static Level level_1_3()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Utils.D * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Utils.R * 11f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Utils.L * 11f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Utils.U * 8f, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "You can stop by going to the opposite direction on border.";
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_4()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Utils.DR * 10f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Utils.DL * 10f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Utils.UR * 10f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Utils.UL * 10f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        return level;
    }

    public static Level level_1_vertical_enemies()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 14f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "Your trail creates borders that will help you out !";
        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));

        Vector2 v1 = Utils.getMidRelativePositionFromPosition(1, 2);
        Vector2 v2 = Utils.getMidRelativePositionFromPosition(4, 5);
        Vector2 v3 = Utils.getMidRelativePositionFromPosition(7, 8);
        Vector2 v4 = Utils.getMidRelativePositionFromPosition(2, 3);
        Vector2 v5 = Utils.getMidRelativePositionFromPosition(5, 6);
        Vector2 v6 = Utils.getMidRelativePositionFromPosition(8, 9);
        level.addStar(new StarInfo(v1, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v2, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v3, Utils.STAR_DEFAULT_SCALE * 0.75f));

        level.addStar(new StarInfo(v4, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v5, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v6, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v1, v2), Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v2, v3), Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v4, v5), Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v5, v6), Utils.STAR_DEFAULT_SCALE * 0.75f));

        return level;
    }
    public static Level level_1_vertical_enemies_harer()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 10f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "Your trail creates borders that will help you out !";
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

    public static Level level_1_7()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.UR * 13f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.UL * 13f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.DR * 13f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.DL * 13f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector3(0.33f, 0.33f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector3(0.33f, 0.66f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector3(0.66f, 0.33f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector3(0.66f, 0.66f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_demon_line()
    {
        // Level 1
        Level level = new Level("level_1_demon_line", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.U * 16f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.D * 16f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_demon_square()
    {
        // Level 1
        Level level = new Level("level_1_demon_square", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.L * 20f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.R * 20f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.U * 16f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.D * 16f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_9()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.2f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.2f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.3f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.3f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.4f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.4f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (Vector2.left) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.5f), (Vector2.right) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.6f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.6f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.7f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.7f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.8f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.5f, 0.8f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));


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

        float speed = 5f;

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

        float speed = 5f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(0f,1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));


        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, (new Vector2(-1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
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

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.4f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.5f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.6f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.7f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.8f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.25f, 0.9f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.1f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.2f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.3f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.4f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.6f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.1f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.2f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.3f), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.7f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.8f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.75f, 0.9f), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));


        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_1_13()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        //float speed = 3f;
        //
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 4, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 6, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        //level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        //level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        //level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        //level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));

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
    public static Level level_1_14()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.1f, 0.1f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.2f, 0.2f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.3f, 0.3f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.4f, 0.4f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.6f, 0.6f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.7f, 0.7f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.8f, 0.8f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.9f, 0.9f), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.1f, 0.1f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.2f, 0.2f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.3f, 0.3f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.4f, 0.4f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.5f, 0.5f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.6f, 0.6f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.7f, 0.7f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.8f, 0.8f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector2(0.9f, 0.9f), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(1, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 9), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));


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
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.6f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.7f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.8f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.2f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.3f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, new Vector3(0.4f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
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

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(1, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(3, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(7, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(9, 5), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_basic_square_circle()
    {
        // Level 1
        Level level = new Level("level_2_basic_square_circle", 50);

        float speed = 10f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 5), Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Square, 1f));

        level.mLevelHelper = "New enemy : <br>Changes directions in cycles !";
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_circle_show_form()
    {
        // Level 1
        Level level = new Level("level_2_circle_show_form", 50);

        float speed = 4f;

        level.addEnemy(new EnemyCircleInfo(7, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(9, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Triangle, 0.5f));
        level.addEnemy(new EnemyCircleInfo(1, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Square, 0.5f));
        level.addEnemy(new EnemyCircleInfo(3, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Pentagone, 0.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(2, 5), (Vector2.right + Vector2.up) * speed, Utils.ENEMY_DEFAULT_SCALE, 60, EnemyCircleInfo.Form.Hexagone, 2f));

        level.mLevelHelper = "Try to understand their pattern.";
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_basic_line()
    {
        // Level 1
        Level level = new Level("level_2_basic_line", 50);

        float speed = 8f;

        level.addEnemy(new EnemyCircleInfo(6, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Line, 1f));
        level.addEnemy(new EnemyCircleInfo(4, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Line, 1f));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 6), Utils.STAR_DEFAULT_SCALE));
        return level;
    }

    public static Level level_2_basic_line_with_inverse()
    {
        // Level 1
        Level level = new Level("level_2_basic_line_with_inverse", 50);

        float speed = 5f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 7), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, -90, 0.5f, true));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 1), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, 90, 0.5f, true));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 9), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, 90, 0.5f, true));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 3), Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, -90, 0.5f, true));


        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 1), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 3), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 7), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 9), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }

    public static Level level_2_basic_square()
    {
        // Level 1
        Level level = new Level("level_2_basic_square", 50);

        float speed = 6f;

        level.addEnemy(new EnemyCircleInfo(8, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, EnemyCircleInfo.Form.Square, 1f));
        level.addEnemy(new EnemyCircleInfo(4, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, EnemyCircleInfo.Form.Square, 1f));
        level.addEnemy(new EnemyCircleInfo(2, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.Square, 1f));
        level.addEnemy(new EnemyCircleInfo(6, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, EnemyCircleInfo.Form.Square, 1f));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(2,4), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(4,8), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(8,6), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(6,2), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_triple_triangle()
    {
        // Level 1
        Level level = new Level("level_2_triple_triangle", 50);

        float speed = 4f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 5, 0.9f), Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 5, 0.45f), Utils.R * speed * 2.3f, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(1, Utils.R * speed * 3.5f, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Triangle, 1f));

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(9, 5, 0.9f), Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(9, 5, 0.45f), Utils.L * speed * 2.3f, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(9, Utils.L * speed * 3.5f, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Triangle, 1f));

        level.mLevelHelper = "Find a passage by breaking enemies' pattern.";
        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(1, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 9), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_three_protection_with_diff_speed()
    {
        // Level 1
        Level level = new Level("level_2_three_protection_with_diff_speed", 50);

        float speed = 3f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0f, 0.5f), 4), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.DR * speed * 2.5f, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 2.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(5, 6), Utils.DR * speed * 4f, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 5f));

        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        return level;
    }

    public static Level level_2_line_basic_with_cross_circle()
    {
        // Level 1
        Level level = new Level("level_2_line_basic_with_cross_circle", 50);

        float speed_basic = 10f;
        float speed_circle = 5f;
        //float offset = 0.15f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, Utils.getMidRelativePositionFromPosition(5, 8), Utils.R * speed_basic, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, Utils.getMidRelativePositionFromPosition(5, 8), Utils.L * speed_basic, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, Utils.getMidRelativePositionFromPosition(5, 2), Utils.R * speed_basic, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, Utils.getMidRelativePositionFromPosition(5, 2), Utils.L * speed_basic, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyCircleInfo(1, Utils.UR * speed_circle, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(3, Utils.UL * speed_circle, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(7, Utils.DR * speed_circle, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(9, Utils.DL * speed_circle, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(2, Utils.U * speed_circle, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(8, Utils.D * speed_circle, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Line, 0.5f));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 4), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 8), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 6), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 2), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_circle_enemy_hitting_the_border()
    {
        // Level 1
        Level level = new Level("level_2_circle_enemy_hitting_the_border", 50);

        float speed = 6f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0f, 0.25f), 1), Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, 100, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0f, 0.5f), 4), Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, 100, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0f, 0.75f), 7), Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, 100, 1f));

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0.25f, 1f), 7), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, 100, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0.5f, 1f), 8), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, 100, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0.75f, 1f), 9), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, 100, 1f));

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(1f, 0.75f), 9), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, 100, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(1f, 0.5f), 6), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, 100, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(1f, 0.25f), 3), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, 100, 1f));

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0.75f, 0f), 3), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, 100, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0.5f, 0f), 2), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, 100, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0.25f, 0f), 1), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, 100, 1f));


        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 1), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 3), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 7), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 9), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_four_square_enemy_turning()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 7f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 5), Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Square,  1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(3, 5), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Square,  1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(9, 5), Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Square,  1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(7, 5), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Square,  1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 5), Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.iSquare, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(3, 5), Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.iSquare, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(9, 5), Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.iSquare, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(7, 5), Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.iSquare, 1f));

        level.addEnemy(new EnemyCircleInfo(2, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.Square, 1f));
        level.addEnemy(new EnemyCircleInfo(6, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, EnemyCircleInfo.Form.Square, 1f));
        level.addEnemy(new EnemyCircleInfo(8, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, EnemyCircleInfo.Form.Square, 1f));
        level.addEnemy(new EnemyCircleInfo(4, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, EnemyCircleInfo.Form.Square, 1f));
        level.addEnemy(new EnemyCircleInfo(2, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, EnemyCircleInfo.Form.iSquare, 1f));
        level.addEnemy(new EnemyCircleInfo(6, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, EnemyCircleInfo.Form.iSquare, 1f));
        level.addEnemy(new EnemyCircleInfo(8, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, EnemyCircleInfo.Form.iSquare, 1f));
        level.addEnemy(new EnemyCircleInfo(4, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.iSquare, 1f));

        level.addEnemy(new EnemyCircleInfo(4, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Line, 1f));
        level.addEnemy(new EnemyCircleInfo(4, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Line, 1f));
        level.addEnemy(new EnemyCircleInfo(6, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Line, 1f));
        level.addEnemy(new EnemyCircleInfo(6, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Line, 1f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(2, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(6, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(8, 5), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_multi_triangle()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5.5f;
        float speed_2 = 3f;

        level.addEnemy(new EnemyCircleInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Triangle, 1f));

        level.addEnemy(new EnemyCircleInfo(5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.iTriangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.iTriangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.iTriangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.iTriangle, 1f));

        level.addEnemy(new EnemyCircleInfo(5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, EnemyCircleInfo.Form.Triangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.Triangle, 1f));

        level.addEnemy(new EnemyCircleInfo(5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, EnemyCircleInfo.Form.iTriangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, EnemyCircleInfo.Form.iTriangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.iTriangle, 1f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, EnemyCircleInfo.Form.iTriangle, 1f));

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.R * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Triangle, 0.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.U * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Triangle, 0.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.L * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Triangle, 0.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.D * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Triangle, 0.5f));
        
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 5), Utils.R * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Triangle, 0.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 5), Utils.U * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Triangle, 0.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 5), Utils.L * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Triangle, 0.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 5), Utils.D * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Triangle, 0.5f));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(2, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(6, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(8, 5), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_line_going_to_the_center()
    {
        // Level 1
        Level level = new Level("level_2_line_going_to_the_center", 50);

        float speed = 5f;
        int nb = 3;
        for (int i = 1; i <= nb; ++i)
        {
            float angle = 1f / nb;
            float angle_rotation = 90 / nb;
            level.addEnemy(new EnemyCircleInfo(4, new Vector2(1f, angle * i) * speed, Utils.ENEMY_DEFAULT_SCALE, angle_rotation * i, EnemyCircleInfo.Form.Line, 1f));
            level.addEnemy(new EnemyCircleInfo(4, new Vector2(1f, -angle * i) * speed, Utils.ENEMY_DEFAULT_SCALE, -angle_rotation * i, EnemyCircleInfo.Form.iLine, 1f));

            level.addEnemy(new EnemyCircleInfo(2, new Vector2(angle * i, 1f) * speed, Utils.ENEMY_DEFAULT_SCALE, 90 - angle_rotation * i, EnemyCircleInfo.Form.Line, 1f));
            level.addEnemy(new EnemyCircleInfo(2, new Vector2(-angle * i, 1f) * speed, Utils.ENEMY_DEFAULT_SCALE, 90 + angle_rotation * i, EnemyCircleInfo.Form.iLine, 1f));

            level.addEnemy(new EnemyCircleInfo(6, new Vector2(-1f, angle * i) * speed, Utils.ENEMY_DEFAULT_SCALE, 180 - angle_rotation * i, EnemyCircleInfo.Form.Line, 1f));
            level.addEnemy(new EnemyCircleInfo(6, new Vector2(-1f, -angle * i) * speed, Utils.ENEMY_DEFAULT_SCALE, 180 + angle_rotation * i, EnemyCircleInfo.Form.iLine, 1f));

            level.addEnemy(new EnemyCircleInfo(8, new Vector2(angle * i, -1f) * speed, Utils.ENEMY_DEFAULT_SCALE, 270 + angle_rotation * i, EnemyCircleInfo.Form.Line, 1f));
            level.addEnemy(new EnemyCircleInfo(8, new Vector2(-angle * i, -1f) * speed, Utils.ENEMY_DEFAULT_SCALE, 270 - angle_rotation * i, EnemyCircleInfo.Form.iLine, 1f));
        }

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 2, 0.4f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 4, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 6, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 8, 0.4f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_octogone_turning()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        int nb = 4;

        for(int i = 0; i < nb; ++i)
        {
            float speed = 2f + 1f * i;
            level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 2), Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Octogone, 1f));
            level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(9, 8), Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Octogone, 1f));
            level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(2, 3), Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.iOctogone, 1f));
            level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(7, 8), Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.iOctogone, 1f));
        }

        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 2), Utils.R * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Octogone, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 2), Utils.R * speed_3, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Octogone, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(1, 2), Utils.R * speed_4, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Octogone, 1f));
        //
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 3), Utils.U * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Octogone, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 3), Utils.U * speed_3, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Octogone, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 3), Utils.U * speed_4, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Octogone, 1f));
        //
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(9, 8), Utils.L * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Octogone, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(9, 8), Utils.L * speed_3, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Octogone, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(9, 8), Utils.L * speed_4, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Octogone, 1f));
        //
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 7), Utils.D * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Octogone, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 7), Utils.D * speed_3, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Octogone, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 7), Utils.D * speed_4, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Octogone, 1f));


        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 4, 0.66f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 6, 0.66f), Utils.STAR_DEFAULT_SCALE));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 4, 0.33f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 6, 0.33f), Utils.STAR_DEFAULT_SCALE));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 2), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 8), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_lot_of_speed()
    {
        // Level 1
        Level level = new Level("level_2_all_form", 50);

        float speed = 22f;

        //level.addEnemy(new EnemyCircleInfo(5, Utils.R * speed_line, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Line, 1f));
        //level.addEnemy(new EnemyCircleInfo(5, Utils.L * speed_line, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Line, 1f));
        //level.addEnemy(new EnemyCircleInfo(5, Utils.D * speed_line, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Line, 1f));
        //level.addEnemy(new EnemyCircleInfo(5, Utils.U * speed_line, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Line, 1f));
        //
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(5, 2), new Vector2(0.5f, 1f) * speed_triangle, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.Triangle, 1f));
        ////level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(5, 2), new Vector2(-0.5f, 1f) * speed_triangle, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, EnemyCircleInfo.Form.iTriangle, 1f));
        ////level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(5, 8), new Vector2(0.5f, -1f) * speed_triangle, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, EnemyCircleInfo.Form.iTriangle, 1f));
        //level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(5, 8), new Vector2(-0.5f, -1f) * speed_triangle, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, EnemyCircleInfo.Form.Triangle, 1f));
        //
        //level.addEnemy(new EnemyCircleInfo(1, Utils.R * speed_square, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Square, 1f));
        //level.addEnemy(new EnemyCircleInfo(3, Utils.U * speed_square, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Square, 1f));
        //level.addEnemy(new EnemyCircleInfo(9, Utils.L * speed_square, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Square, 1f));
        //level.addEnemy(new EnemyCircleInfo(7, Utils.D * speed_square, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Square, 1f));

        level.addEnemy(new EnemyCircleInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rR, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rL, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rU, EnemyCircleInfo.Form.Line, 0.5f));
        level.addEnemy(new EnemyCircleInfo(5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rD, EnemyCircleInfo.Form.Line, 0.5f));

        //level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        //level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        //level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        //level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(2, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(6, 5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(8, 5), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_12()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        //level.addEnemy(new EnemyCircleInfo(1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        //level.addEnemy(new EnemyCircleInfo(2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        //level.addEnemy(new EnemyCircleInfo(2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        //level.addEnemy(new EnemyCircleInfo(3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        //level.addEnemy(new EnemyCircleInfo(4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        //level.addEnemy(new EnemyCircleInfo(5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        //level.addEnemy(new EnemyCircleInfo(5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        //level.addEnemy(new EnemyCircleInfo(6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        //level.addEnemy(new EnemyCircleInfo(7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        //level.addEnemy(new EnemyCircleInfo(8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        //level.addEnemy(new EnemyCircleInfo(8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        //level.addEnemy(new EnemyCircleInfo(9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_13()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyCircleInfo(1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_14()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        level.addEnemy(new EnemyCircleInfo(1, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(1, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(2, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(2, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(3, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(3, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(4, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(4, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(5, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(5, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(5, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(5, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(6, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(6, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(7, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(7, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(8, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(8, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(9, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(9, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));


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
    public static Level level_2_15()
    {
        // Level 1
        Level level = new Level("Basic", 50);


        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.2f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.3f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.4f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.5f), (Vector2.left) * 8f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.6f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.7f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.8f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.2f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.3f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.4f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.5f), (Vector2.right) * 8f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.6f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.7f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.8f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(new Vector3(0.2f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.3f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.4f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.5f), (Vector2.up) * 8f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.6f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.7f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.8f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

        level.addEnemy(new EnemyCircleInfo(new Vector3(0.2f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.3f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.4f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.5f, 0.5f), (Vector2.down) * 8f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.6f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.7f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));
        level.addEnemy(new EnemyCircleInfo(new Vector3(0.8f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE, Random.RandomRange(-360, 360f), Random.RandomRange(0, 360), Random.RandomRange(0.2f, 5f)));

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
    public static Level level_3_1()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 4f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 4f));
        return level;
    }
    public static Level level_3_2()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.5f), Vector2.up * 7f, Utils.ENEMY_DEFAULT_SCALE * 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.5f), Vector2.right * 7f, Utils.ENEMY_DEFAULT_SCALE * 2f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_3_3()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.75f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.25f), Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.25f, 0.5f), Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.75f, 0.5f), Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_4()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.5f), Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.5f), Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector2(0.5f, 0.5f), Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.25f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_5()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_3_6()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 7, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 1, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 9, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 3, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_7()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector2.right + Vector2.up) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector2.left + Vector2.up) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector2.right + Vector2.down) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector2.left + Vector2.down) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_8()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector2.right + Vector2.up) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector2.left + Vector2.up) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector2.right + Vector2.down) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector2.left + Vector2.down) * 3f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_9()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.2f), (Vector2.left) * 1f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.3f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.4f), (Vector2.left) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.6f), (Vector2.left) * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.7f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.8f), (Vector2.left) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.2f), (Vector2.right) * 1f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.3f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.4f), (Vector2.right) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.6f), (Vector2.right) * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.7f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.8f), (Vector2.right) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_10()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_11()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));


        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_12()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Follower, 9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_13()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_3_14()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 1, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 1, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 2, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 3, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 3, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 4, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 4, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 5, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 6, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 6, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 7, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 7, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 8, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, 9, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, 9, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


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
    public static Level level_3_15()
    {
        // Level 1
        Level level = new Level("Basic", 50);


        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.2f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.3f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.4f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (Vector2.left) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.6f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.7f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.8f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.2f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.3f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.4f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (Vector2.right) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.6f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.7f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.8f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.2f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.3f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.4f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (Vector2.up) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.6f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.7f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.8f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.2f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.3f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.4f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.5f, 0.5f), (Vector2.down) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.6f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.7f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Follower, new Vector3(0.8f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));

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
    public static Level level_4_1()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 4f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 4f));
        return level;
    }
    public static Level level_4_2()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.5f), Vector2.up * 7f, Utils.ENEMY_DEFAULT_SCALE * 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.5f), Vector2.right * 7f, Utils.ENEMY_DEFAULT_SCALE * 2f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_4_3()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.75f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.25f), Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.25f, 0.5f), Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.75f, 0.5f), Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_4()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.5f), Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.5f), Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.5f, 0.5f), Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.25f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_5()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_4_6()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_7()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector2.right + Vector2.up) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector2.left + Vector2.up) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector2.right + Vector2.down) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector2.left + Vector2.down) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_8()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector2.right + Vector2.up) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector2.left + Vector2.up) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector2.right + Vector2.down) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector2.left + Vector2.down) * 3f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_9()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.2f), (Vector2.left) * 1f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.3f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.4f), (Vector2.left) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.6f), (Vector2.left) * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.7f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.8f), (Vector2.left) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.2f), (Vector2.right) * 1f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.3f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.4f), (Vector2.right) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.6f), (Vector2.right) * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.7f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.8f), (Vector2.right) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_10()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_11()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));


        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_12()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_13()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_14()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


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
    public static Level level_4_15()
    {
        // Level 1
        Level level = new Level("Basic", 50);


        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.2f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.3f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.4f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (Vector2.left) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.6f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.7f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.8f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.2f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.3f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.4f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (Vector2.right) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.6f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.7f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.8f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.2f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.3f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.4f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (Vector2.up) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.6f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.7f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.8f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.2f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.3f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.4f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.5f, 0.5f), (Vector2.down) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.6f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.7f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector3(0.8f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));

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
    public static Level level_5_1()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 4f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 4f));
        return level;
    }
    public static Level level_5_2()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), Vector2.up * 7f, Utils.ENEMY_DEFAULT_SCALE * 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), Vector2.right * 7f, Utils.ENEMY_DEFAULT_SCALE * 2f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_5_3()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.75f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.25f), Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.25f, 0.5f), Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.75f, 0.5f), Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE * 1.5f));

        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_4()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector2(0.5f, 0.5f), Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.25f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_5()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.up * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.down * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.right * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.left * 5f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.75f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE * 2f));
        return level;
    }
    public static Level level_5_6()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 7, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 1, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 9, Vector2.up * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 3, Vector2.down * 6f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_7()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector2.right + Vector2.up) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector2.left + Vector2.up) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector2.right + Vector2.down) * 7f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector2.left + Vector2.down) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_8()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector2.right + Vector2.up) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector2.left + Vector2.up) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector2.right + Vector2.down) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector2.left + Vector2.down) * 3f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_9()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.2f), (Vector2.left) * 1f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.3f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.4f), (Vector2.left) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.6f), (Vector2.left) * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.7f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.8f), (Vector2.left) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.2f), (Vector2.right) * 1f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.3f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.4f), (Vector2.right) * 3f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.6f), (Vector2.right) * 5f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.7f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.8f), (Vector2.right) * 7f, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_10()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_11()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));


        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.25f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.5f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(0.75f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, -1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(1f, -0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.25f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.5f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-0.75f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 1f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.75f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.5f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0.25f)) * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (new Vector2(-1f, 0f)) * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_12()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        //level.addEnemy(new EnemyInfo(EnemyType.Driller, 9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_13()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 3f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 1, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 1, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 3, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 3, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 4, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 4, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 4, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 6, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 6, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 6, Vector2.down * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 7, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 7, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, Vector2.right * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 9, Vector2.up * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 9, Vector2.left * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_5_14()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 5f;
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 1, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 1, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 2, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 3, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 3, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 4, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 4, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 5, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 6, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 6, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 7, (Vector3.left + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 7, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, (Vector3.right + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 8, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, 9, (Vector3.right + Vector3.down) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, 9, (Vector3.left + Vector3.up) * speed, Utils.ENEMY_DEFAULT_SCALE / 2f));


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
    public static Level level_5_15()
    {
        // Level 1
        Level level = new Level("Basic", 50);


        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero + Vector2.up * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.2f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.3f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.4f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (Vector2.left) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.6f), (Vector2.left) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.7f), (Vector2.left) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.8f), (Vector2.left) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.2f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.3f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.4f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (Vector2.right) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.6f), (Vector2.right) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.7f), (Vector2.right) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.8f), (Vector2.right) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.2f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.3f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.4f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (Vector2.up) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.6f, 0.5f), (Vector2.up) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.7f, 0.5f), (Vector2.up) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.8f, 0.5f), (Vector2.up) * 2f, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.2f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.3f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.4f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.5f, 0.5f), (Vector2.down) * 8f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.6f, 0.5f), (Vector2.down) * 6f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.7f, 0.5f), (Vector2.down) * 4f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Driller, new Vector3(0.8f, 0.5f), (Vector2.down) * 2f, Utils.ENEMY_DEFAULT_SCALE));

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


    public Levels(int nb_level)
    {
        levels = new List<Level>();
        for (int i = 0; i < nb_level; ++i)
        {
            levels.Add(new Level("tmp", 0));
        }
    }
    public Levels()
    {
        levels = new List<Level>();
    }

    public void addLevel(Level level)
    {
        levels.Add(level);
    }
    public void addLevel(int idx, Level level)
    {
        levels[idx - 1] = level;
    }

}


public class Worlds
{
    public static List<Levels> worlds;

    private static Levels world_1()
    {
        Levels levels = new Levels(12);

        levels.levels_name = "A simple Start";
        levels.addLevel(1, Levels.level_1_one_star());
        levels.addLevel(2, Levels.level_1_only_stars());
        levels.addLevel(3, Levels.level_1_one_slow_enemy());
        levels.addLevel(4, Levels.level_1_vertical_enemies());
        levels.addLevel(6, Levels.level_1_7());
        levels.addLevel(5, Levels.level_1_3());
        //levels.addLevel(4, Levels.level_1_2_medium_enemy_horizontal());
        //levels.addLevel(4, Levels.level_1_4());
        //levels.addLevel(7, Levels.level_1_demon_square()); // Could be removed
        levels.addLevel(7, Levels.level_1_demon_line()); // Could be removed
        levels.addLevel(8, Levels.level_1_9());
        levels.addLevel(9, Levels.level_1_12());
        levels.addLevel(10, Levels.level_1_11());
        levels.addLevel(11, Levels.level_2_basic_square_circle());
        levels.addLevel(12, Levels.level_2_circle_show_form());

        //levels.addLevel(11, Levels.level_1_11()); // Either level 10 or 11 can be removed
        //levels.addLevel(12, Levels.level_1_13()); // COuld be remvoed too
        //levels.addLevel(2, Levels.level_1_15());

        return levels;
    }
    private static Levels world_2()
    {
        Levels levels = new Levels(12);

        levels.levels_name = "Different way";
        //levels.addLevel(Levels.level_1_1());
        //levels.addLevel(Levels.level_1_2());
        levels.addLevel(1, Levels.level_2_basic_line());
        levels.addLevel(2, Levels.level_2_basic_line_with_inverse());
        levels.addLevel(3, Levels.level_2_octogone_turning()); // OK
        levels.addLevel(4, Levels.level_2_basic_square()); // OK - Medium
        levels.addLevel(5, Levels.level_2_three_protection_with_diff_speed()); // MEDIUM
        levels.addLevel(6, Levels.level_2_line_basic_with_cross_circle()); // Medium - hard
        levels.addLevel(7, Levels.level_2_triple_triangle()); // HARD


        //levels.addLevel(8, Levels.level_2_circle_enemy_hitting_the_border()); // TO remove
        //levels.addLevel(9, Levels.level_2_four_square_enemy_turning()); // Slower - Hard ++ (remove ?)
        //levels.addLevel(10, Levels.level_2_multi_triangle()); // Slower - Hard ++ (remove ?)
        //levels.addLevel(11, Levels.level_2_line_going_to_the_center()); // Slower
        //levels.addLevel(12, Levels.level_2_lot_of_speed()); // Bof

        return levels;
    }
    private static Levels world_3()
    {
        Levels levels = new Levels();

        levels.levels_name = "World 3";
        levels.addLevel(Levels.level_3_3());
        levels.addLevel(Levels.level_3_4());
        levels.addLevel(Levels.level_3_6());
        levels.addLevel(Levels.level_3_7());
        levels.addLevel(Levels.level_3_8());
        levels.addLevel(Levels.level_3_9());
        levels.addLevel(Levels.level_3_10());
        levels.addLevel(Levels.level_3_11());
        levels.addLevel(Levels.level_3_12());
        levels.addLevel(Levels.level_3_13());
        levels.addLevel(Levels.level_3_14());
        levels.addLevel(Levels.level_3_15());

        return levels;
    }
    private static Levels world_4()
    {
        Levels levels = new Levels();

        levels.levels_name = "World 4";
        //levels.addLevel(Levels.level_1_1());
        //levels.addLevel(Levels.level_1_2());
        levels.addLevel(Levels.level_4_3());
        levels.addLevel(Levels.level_4_4());
        levels.addLevel(Levels.level_4_6());
        levels.addLevel(Levels.level_4_7());
        levels.addLevel(Levels.level_4_8());
        levels.addLevel(Levels.level_4_9());
        levels.addLevel(Levels.level_4_10());
        levels.addLevel(Levels.level_4_11());
        levels.addLevel(Levels.level_4_12());
        levels.addLevel(Levels.level_4_13());
        levels.addLevel(Levels.level_4_14());
        levels.addLevel(Levels.level_4_15());

        return levels;
    }
    private static Levels world_5()
    {
        Levels levels = new Levels();

        levels.levels_name = "World 5";
        //levels.addLevel(Levels.level_1_1());
        //levels.addLevel(Levels.level_1_2());
        levels.addLevel(Levels.level_5_3());
        levels.addLevel(Levels.level_5_4());
        levels.addLevel(Levels.level_5_6());
        levels.addLevel(Levels.level_5_7());
        levels.addLevel(Levels.level_5_8());
        levels.addLevel(Levels.level_5_9());
        levels.addLevel(Levels.level_5_10());
        levels.addLevel(Levels.level_5_11());
        levels.addLevel(Levels.level_5_12());
        levels.addLevel(Levels.level_5_13());
        levels.addLevel(Levels.level_5_14());
        levels.addLevel(Levels.level_5_15());

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
        worlds.Add(world_3());
        worlds.Add(world_4());
        worlds.Add(world_5());
    }
}