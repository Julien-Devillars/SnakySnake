using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
    Dummy,
    Basic,
    Circle,
    Follower,
    Flyer,
    Driller,
    Faster
}

public class EnemyInfo : ObjectInfo 
{
    public EnemyType type;
    public List<int> link;

    public EnemyInfo(EnemyType _type, Vector2 _position, Vector2 _direction, float _scale) : base(_position, _direction, _scale)
    {
        type = _type;
        link = null;
    }
    public EnemyInfo(EnemyType _type, int _position, Vector2 _direction, float _scale) : base(_position, _direction, _scale)
    {
        type = _type;
        link = null;
    }
    public EnemyInfo(EnemyType _type, Vector2 _position, Vector2 _direction, float _scale, List<int> _link) : base(_position, _direction, _scale)
    {
        type = _type;
        link = _link;
    }
    public EnemyInfo(EnemyType _type, int _position, Vector2 _direction, float _scale, List<int> _link) : base(_position, _direction, _scale)
    {
        type = _type;
        link = _link;
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
    public EnemyCircleInfo(Vector2 _position, Vector2 _direction, float _scale, float _rotation_position, float _rotation_speed, float _attack_speed, bool _inverse = false, List<int> _link = null) : base(EnemyType.Circle, _position, _direction, _scale)
    {
        mRotateSpeed = _rotation_speed;
        mAttackSpeed = _attack_speed;
        mStartRotation = _rotation_position;
        mInverse = _inverse;
        link = _link;
    }
    public EnemyCircleInfo(int _position, Vector2 _direction, float _scale, float _rotation_position, float _rotation_speed, float _attack_speed, bool _inverse = false, List<int> _link = null) : base(EnemyType.Circle, _position, _direction, _scale)
    {
        mRotateSpeed = _rotation_speed;
        mAttackSpeed = _attack_speed;
        mStartRotation = _rotation_position;
        mInverse = _inverse;
        link = _link;
    }
    public EnemyCircleInfo(int _position, Vector2 _direction, float _scale, float _rotation_position, Form _form, float _form_scale, bool _inverse = false, List<int> _link = null) : base(EnemyType.Circle, _position, _direction, _scale)
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
        link = _link;
    }
    public EnemyCircleInfo(Vector2 _position, Vector2 _direction, float _scale, float _rotation_position, Form _form, float _form_scale, bool _inverse = false, List<int> _link = null) : base(EnemyType.Circle, _position, _direction, _scale)
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
        link = _link;
    }
}

public class EnemyFollowerInfo : EnemyInfo
{
    public bool mKeepMoving;
    public EnemyFollowerInfo(Vector2 _position, Vector2 _direction, float _scale, bool keep_moving = false, List<int> _link = null) : base(EnemyType.Follower, _position, _direction, _scale)
    {
        mKeepMoving = keep_moving;
        link = _link;
    }
    public EnemyFollowerInfo(int _position, Vector2 _direction, float _scale, bool keep_moving = false, List<int> _link = null) : base(EnemyType.Follower, _position, _direction, _scale)
    {
        mKeepMoving = keep_moving;
        link = _link;
    }
}

public class EnemyDrillerInfo : EnemyInfo
{
    public bool mReverseVertical;
    public bool mReverseHorizontal;
    public EnemyDrillerInfo(Vector2 _position, Vector2 _direction, float _scale, bool reverse_on_vertical = false, bool reverse_on_horizontal = false, List<int> _link = null) 
        : base(EnemyType.Driller, _position, _direction, _scale, _link)
    {
        mReverseVertical = reverse_on_vertical;
        mReverseHorizontal = reverse_on_horizontal;
    }
    public EnemyDrillerInfo(int _position, Vector2 _direction, float _scale, bool reverse_on_vertical = false, bool reverse_on_horizontal = false, List<int> _link = null) 
        : base(EnemyType.Driller, _position, _direction, _scale, _link)
    {
        mReverseVertical = reverse_on_vertical;
        mReverseHorizontal = reverse_on_horizontal;
    }
}
public class EnemyFasterInfo : EnemyInfo
{
    public int mCounter;
    public float mStunTime;
    public float mMultiplier;
    public EnemyFasterInfo(Vector2 _position, Vector2 _direction, float _scale, int counter = 3, float stun_time = 1.5f, float multiplier = 1.5f, List<int> _link = null) : base(EnemyType.Faster, _position, _direction, _scale)
    {
        link = _link;
        mCounter = counter;
        mStunTime = stun_time;
        mMultiplier = multiplier;
    }
    public EnemyFasterInfo(int _position, Vector2 _direction, float _scale, int counter = 3, float stun_time = 1.5f, float multiplier = 1.5f, List<int> _link = null) : base(EnemyType.Faster, _position, _direction, _scale)
    {
        link = _link;
        mCounter = counter;
        mStunTime = stun_time;
        mMultiplier = multiplier;
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
    public List<AdditionalBorder> mAdditionalBorders;
    public float mBronzeTime = -1f;
    public float mSilverTime = -1f;
    public float mGoldTime = -1f;

    int mBestReach = 0;
    public Level(string name, int goal)
    {
        mLevelName = name;
        mGoalScore = goal;
        mEnemies = new List<EnemyInfo>();
        mStars = new List<StarInfo>();
        mAdditionalBorders = new List<AdditionalBorder>();
    }

    public void addEnemy(EnemyInfo enemy)
    {
        mEnemies.Add(enemy);
    }
    public void addStar(StarInfo star)
    {
        mStars.Add(star);
    }
    public void addStars(params int[] position)
    {
        foreach(int pos in position)
        {
            addStar(new StarInfo(pos, Utils.STAR_DEFAULT_SCALE));
        }
    }
    public void addStars(params Vector2[] position)
    {
        foreach (Vector2 pos in position)
        {
            addStar(new StarInfo(pos, Utils.STAR_DEFAULT_SCALE));
        }
    }
    public void addTrail(params Vector3[] points)
    {
        AdditionalBorder additional_border = new AdditionalBorder();
        additional_border.Add(points);
        mAdditionalBorders.Add(additional_border);
    }
    public void addTimers(float bronze, float silver, float gold)
    {
        mBronzeTime = bronze;
        mSilverTime = silver;
        mGoldTime = gold;
    }
    public void addTimers(float gold)
    {
        mBronzeTime = gold * 3f;
        mSilverTime = gold * 2f;
        mGoldTime = gold;
    }
    public string getGoalTime(float timer)
    {
        if (timer < 0f)
        {
            return Utils.getTimeFromFloat(mBronzeTime);
        }
        else if (timer < mGoldTime)
        {
            return Utils.getTimeFromFloat(mGoldTime);
        }
        else if (timer < mSilverTime)
        {
            return Utils.getTimeFromFloat(mGoldTime);
        }
        else if (timer < mBronzeTime)
        {
            return Utils.getTimeFromFloat(mSilverTime);
        }
        else
        {
            return Utils.getTimeFromFloat(mBronzeTime);
        }
    }
    public Material getGoalMaterial(float timer)
    {
        if (timer < 0f)
        {
            return null;
        }
        else if (timer < mGoldTime)
        {
            return Resources.Load<Material>("Materials/UI/ChronometerGoalGold");
        }
        else if (timer < mSilverTime)
        {
            return Resources.Load<Material>("Materials/UI/ChronometerGoalSilver");
        }
        else if (timer < mBronzeTime)
        {
            return Resources.Load<Material>("Materials/UI/ChronometerGoalBronze");
        }
        else
        {
            return null;
        }
    }
}

public class AdditionalBorder
{
    public List<Vector3> mFakeTrails = new List<Vector3>();
    public AdditionalBorder()
    {
        mFakeTrails = new List<Vector3>();
    }
    public void Add(params Vector3[] points) => mFakeTrails.AddRange(points);
    public void Add(Vector3 point) => mFakeTrails.Add(point);
}

public class Levels
{
    public string levels_name;
    public List<Level> levels;
    public string mWorldMusic;
    public Color mWorldColorPrincipal_1;
    public Color mWorldColorPrincipal_2;
    public Color mWorldColorSecond_1;
    public Color mWorldColorSecond_2;
    public float mWorldHue = 0f;
    public float mBronzeTime = -1f;
    public float mSilverTime = -1f;
    public float mGoldTime = -1f;

    public static Level level_1_one_star()
    {
        // Level 1
        Level level = new Level("level_1_one_star", 50);
        level.mLevelHelper = "Use the arrows or ZQSD to move.";
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE * 2f));
        level.addTimers(Utils.getSeconds(0, 6f), Utils.getSeconds(0, 4f), Utils.getSeconds(0, 2f));
        return level;
    }
    public static Level level_1_only_stars()
    {
        // Level 1
        Level level = new Level("level_1_only_stars", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector3.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "Catch all the stars to end the level.";
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(Utils.getSeconds(0, 8f), Utils.getSeconds(0, 6f), Utils.getSeconds(0, 4f));

        return level;
    }
    public static Level level_1_one_slow_enemy()
    {
        // Level 1
        Level level = new Level("level_1_only_star", 50);

        float speed = 7f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "You lose if someone hits your trail !";
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(Utils.getSeconds(0, 10f), Utils.getSeconds(0, 5f), Utils.getSeconds(0, 2.5f));

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

        level.mLevelHelper = "Press Space bar to stop moving.<br>Keep pressing to move slowly";
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));

        level.addTimers(Utils.getSeconds(0, 20f), Utils.getSeconds(0, 13f), Utils.getSeconds(0, 8f));
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

        float speed = 10f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 7, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 9, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "The created borders will help you out !";
        level.addStar(new StarInfo(new Vector2(0.25f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.5f, 0.5f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(new Vector2(0.75f, 0.5f), Utils.STAR_DEFAULT_SCALE));

        Vector2 v1 = Utils.getMidRelativePositionFromPosition(1, 2);
        Vector2 v2 = Utils.getMidRelativePositionFromPosition(4, 5);
        Vector2 v3 = Utils.getMidRelativePositionFromPosition(7, 8);
        Vector2 v4 = Utils.getMidRelativePositionFromPosition(2, 3);
        Vector2 v5 = Utils.getMidRelativePositionFromPosition(5, 6);
        Vector2 v6 = Utils.getMidRelativePositionFromPosition(8, 9);

        Vector2 v7 = Utils.getMidRelativePositionFromPosition(new Vector3(0f, 0.25f), 1);
        Vector2 v8 = Utils.getMidRelativePositionFromPosition(new Vector3(0f, 0.5f), 4);
        Vector2 v9 = Utils.getMidRelativePositionFromPosition(new Vector3(0f, 0.75f), 7);
        Vector2 v10 = Utils.getMidRelativePositionFromPosition(new Vector3(1f, 0.25f), 3);
        Vector2 v11 = Utils.getMidRelativePositionFromPosition(new Vector3(1f, 0.5f), 6);
        Vector2 v12 = Utils.getMidRelativePositionFromPosition(new Vector3(1f, 0.75f), 9);

        level.addStar(new StarInfo(v1, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v2, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v3, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v1, v2), Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v2, v3), Utils.STAR_DEFAULT_SCALE * 0.75f));

        level.addStar(new StarInfo(v4, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v5, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v6, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v4, v5), Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v5, v6), Utils.STAR_DEFAULT_SCALE * 0.75f));

        level.addStar(new StarInfo(v7, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v8, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v9, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v7, v8), Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v8, v9), Utils.STAR_DEFAULT_SCALE * 0.75f));

        level.addStar(new StarInfo(v10, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v11, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(v12, Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v10, v11), Utils.STAR_DEFAULT_SCALE * 0.75f));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(v11, v12), Utils.STAR_DEFAULT_SCALE * 0.75f));

        level.addTimers(Utils.getSeconds(0, 20f), Utils.getSeconds(0, 16f), Utils.getSeconds(0, 12f));
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

        //level.mLevelHelper = "Your trail creates borders that will help you out !";
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

        level.addTimers(Utils.getSeconds(0, 20f), Utils.getSeconds(0, 15f), Utils.getSeconds(0, 10f));
        return level;
    }
    public static Level level_1_demon_line()
    {
        // Level 1
        Level level = new Level("level_1_demon_line", 50);

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.U * 16f, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.D * 16f, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "You can skip the level and come back later.";

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(Utils.getSeconds(0, 22.5f), Utils.getSeconds(0, 17.5f), Utils.getSeconds(0, 12.5f));
        return level;
    }
    public static Level level_1_demon_square()
    {
        // Level 1
        Level level = new Level("level_1_demon_square", 50);

        float speed = 18f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.U * (speed - 5f), Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.D * (speed - 5f), Utils.ENEMY_DEFAULT_SCALE));

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
        level.addTimers(15f);
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
        level.addTimers(Utils.getSeconds(0, 22f), Utils.getSeconds(0, 18f), Utils.getSeconds(0, 13f));
        return level;
    }
    public static Level level_1_basic_link()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 7f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(1)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(Utils.getSeconds(0, 18f), Utils.getSeconds(0, 13f), Utils.getSeconds(0, 7.5f));

        level.mLevelHelper = "Some enemies can be linked, do not cross it !";
        return level;
    }
    public static Level level_1_basic_link_2()
    {
        // Level 1
        Level level = new Level("Basic", 50);

        float speed = 7f;

        level.addEnemy(new EnemyInfo(EnemyType.Basic, 1, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(2)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 8, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(0, 2)));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 3, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(Utils.getSeconds(0, 20f), Utils.getSeconds(0, 16f), Utils.getSeconds(0, 11.5f));

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
        level.addTimers(Utils.getSeconds(0, 40f), Utils.getSeconds(0, 30f), Utils.getSeconds(0, 20f));
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
        level.addTimers(7f);
        return level;
    }
    public static Level level_2_circle_show_form()
    {
        // Level 1
        Level level = new Level("level_2_circle_show_form", 50);

        float speed = 4f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(2, 5), (Vector2.right + Vector2.up) * speed, Utils.ENEMY_DEFAULT_SCALE, 60, EnemyCircleInfo.Form.Hexagone, 2f));

        level.mLevelHelper = "Try to understand their pattern.";
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(2.5f);
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
        level.addTimers(20f, 15f, 9f);
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
        level.addTimers(5f);
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_2_basic_line_with_inverse_with_link()
    {
        // Level 1
        Level level = new Level("level_2_basic_line_with_inverse", 50);

        float speed = 5f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 7), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, -90, 0.5f, true, Utils.Linker(2)));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 1), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, 90, 0.5f, true, Utils.Linker(3)));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 9), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, 90, 0.5f, true));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(6, 3), Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, -90, 0.5f, true));


        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 1), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 3), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 7), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 9), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(6f);
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
        level.addTimers(25f);
        return level;
    }
    public static Level level_2_basic_square_with_link()
    {
        // Level 1
        Level level = new Level("level_2_basic_square", 50);

        float speed = 6f;

        level.addEnemy(new EnemyCircleInfo(8, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDL, EnemyCircleInfo.Form.Square, 1f, false, Utils.Linker(1)));
        level.addEnemy(new EnemyCircleInfo(4, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rDR, EnemyCircleInfo.Form.Square, 1f, false, Utils.Linker(2)));
        level.addEnemy(new EnemyCircleInfo(2, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUR, EnemyCircleInfo.Form.Square, 1f, false, Utils.Linker(3)));
        level.addEnemy(new EnemyCircleInfo(6, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.rUL, EnemyCircleInfo.Form.Square, 1f, false, Utils.Linker(0)));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(2, 4), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(4, 8), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(8, 6), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(6, 2), Utils.STAR_DEFAULT_SCALE));
        level.addTimers(9f);
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
        level.addTimers(7f);
        return level;
    }
    public static Level level_2_three_protection_with_diff_speed()
    {
        // Level 1
        Level level = new Level("level_2_three_protection_with_diff_speed", 50);

        float speed = 3f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0f, 0.5f), 4), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 1f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.DR * speed * 2.5f, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 2.5f));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(5, 6), Utils.DR * speed * 3.5f, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 3f));

        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(15f);
        return level;
    }
    public static Level level_2_three_protection_with_diff_speed_with_link()
    {
        // Level 1
        Level level = new Level("level_2_three_protection_with_diff_speed", 50);

        float speed = 3f;

        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(new Vector2(0f, 0.5f), 4), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 1f, false, Utils.Linker(1)));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(4, 5), Utils.DR * speed * 2.5f, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 2.5f, false, Utils.Linker(2)));
        level.addEnemy(new EnemyCircleInfo(Utils.getMidRelativePositionFromPosition(5, 6), Utils.DR * speed * 3.5f, Utils.ENEMY_DEFAULT_SCALE, 300, EnemyCircleInfo.Form.Hexagone, 3f, false, Utils.Linker(0)));

        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(12f);
        return level;
    }

    public static Level level_2_line_basic_with_cross_circle()
    {
        // Level 1
        Level level = new Level("level_2_line_basic_with_cross_circle", 50);

        float speed_basic = 10f;
        float speed_circle = 5f;
        //float offset = 0.15f;

        //level.addEnemy(new EnemyInfo(EnemyType.Basic, Utils.getMidRelativePositionFromPosition(5, 8), Utils.R * speed_basic, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, Utils.getMidRelativePositionFromPosition(5, 8), Utils.L * speed_basic, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, Utils.getMidRelativePositionFromPosition(5, 2), Utils.R * speed_basic, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Basic, Utils.getMidRelativePositionFromPosition(5, 2), Utils.L * speed_basic, Utils.ENEMY_DEFAULT_SCALE));

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
        level.addTimers(13f);
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

        float speed = 4f;
        float speed_2 = 2f;

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
        level.addTimers(10f);
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
    public static Level level_3_Basic_Follower()
    {
        // Level 1
        Level level = new Level("level_3_Basic_Follower", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(7.5f);
        return level;
    }
    public static Level level_3_Basic_Follower_moving()
    {
        // Level 1
        Level level = new Level("level_3_Basic_Follower_moving", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, true));
        
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(3.5f);
        return level;
    }
    public static Level level_3_Double_Follower()
    {
        // Level 1
        Level level = new Level("level_3_Basic_Follower", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(4, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(6, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.mLevelHelper = "New enemy : <br>It will follow you !";
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(2.5f);
        return level;
    }
    public static Level level_3_Double_Follower_split_in_2()
    {
        // Level 1
        Level level = new Level("level_3_Double_Follower_split_in_2", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(4, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(6, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));

        level.addTrail(new Vector3(0.5f, 0f), new Vector3(0.5f, 1f));
        level.addTimers(7.5f);
        return level;
    }
    public static Level level_3_Double_Follower_Link()
    {
        // Level 1
        Level level = new Level("level_3_Double_Follower_Link", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(4, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, false, Utils.Linker(1)));
        level.addEnemy(new EnemyFollowerInfo(6, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 2), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 8), Utils.STAR_DEFAULT_SCALE));
        level.addTimers(5f);
        return level;
    }
    public static Level level_3_Basic_Follower_Faster()
    {
        // Level 1
        Level level = new Level("level_3_Basic_Follower", 50);
        float speed = 16f;
        level.addEnemy(new EnemyFollowerInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));

        level.addTrail(new Vector3(0.2f, 0.2f), new Vector3(0.2f, 0.4f), new Vector3(0.4f, 0.4f), new Vector3(0.4f, 0.2f), new Vector3(0.2f, 0.2f));
        level.addTrail(new Vector3(0.8f, 0.8f), new Vector3(0.8f, 0.6f), new Vector3(0.6f, 0.6f), new Vector3(0.6f, 0.8f), new Vector3(0.8f, 0.8f));
        level.addTimers(11.5f);
        return level;
    }
    public static Level level_3_Triple_Follower()
    {
        // Level 1
        Level level = new Level("level_3_Basic_Follower", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(1, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(8, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(3, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(5f);
        return level;
    }
    public static Level level_3_Quad_Follower()
    {
        // Level 1
        Level level = new Level("level_3_Quad_Follower", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(1, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, true));
        level.addEnemy(new EnemyFollowerInfo(3, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, true));
        level.addEnemy(new EnemyFollowerInfo(7, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, true));
        level.addEnemy(new EnemyFollowerInfo(9, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, true));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(12.5f);
        return level;
    }
    public static Level level_3_Quad_Follower_In_Cell_Move()
    {
        // Level 1
        Level level = new Level("level_3_Quad_Follower_In_Cell_Move", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(1, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, false));
        level.addEnemy(new EnemyFollowerInfo(3, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, true));
        level.addEnemy(new EnemyFollowerInfo(7, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, true));
        level.addEnemy(new EnemyFollowerInfo(9, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, false));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));

        level.addTrail(new Vector3(0.5f, 0.0f), new Vector3(0.5f, 1.0f));
        level.addTrail(new Vector3(0.0f, 0.5f), new Vector3(1.0f, 0.5f));
        level.addTimers(11.5f);
        return level;
    }
    public static Level level_3_Quad_Follower_Cross_Link()
    {
        // Level 1
        Level level = new Level("level_3_Quad_Follower_Cross_Link", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(1, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, true, Utils.Linker(3)));
        level.addEnemy(new EnemyFollowerInfo(3, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, true, Utils.Linker(2)));
        level.addEnemy(new EnemyFollowerInfo(7, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, true));
        level.addEnemy(new EnemyFollowerInfo(9, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, true));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(11.5f);
        return level;
    }
    public static Level level_3_Quad_Follower_Square_Link()
    {
        // Level 1
        Level level = new Level("level_3_Quad_Follower_Square_Link", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(1, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, true, Utils.Linker(1)));
        level.addEnemy(new EnemyFollowerInfo(3, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, true, Utils.Linker(3)));
        level.addEnemy(new EnemyFollowerInfo(7, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, true, Utils.Linker(0)));
        level.addEnemy(new EnemyFollowerInfo(9, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, true, Utils.Linker(2)));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addTimers(10.5f);
        return level;
    }
    public static Level level_3_Quad_Follower_split_in_2()
    {
        // Level 1
        Level level = new Level("level_3_Quad_Follower_split_in_2", 50);
        float speed = 8f;
        level.addEnemy(new EnemyFollowerInfo(1, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(3, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(7, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(9, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(1, 4), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(7, 4), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(3, 6), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(9, 6), Utils.STAR_DEFAULT_SCALE));

        level.addTimers(10f);
        level.addTrail(new Vector3(0.5f, 0f), new Vector3(0.5f, 1f));
        return level;
    }

    public static Level level_3_follower_with_circle()
    {

        Level level = new Level("level_3_follower_with_circle", 50);
        float speed = 5f;
        level.addEnemy(new EnemyFollowerInfo(1, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(3, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(7, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyFollowerInfo(9, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyCircleInfo(9, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyCircleInfo(9, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_4_basic_flyer()
    {
        Level level = new Level("level_with_dummy", 50);

        float speed = 7f;
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));

        level.addTrail(new Vector3(0.35f, 0f), new Vector3(0.35f, 1f));
        level.addTrail(new Vector3(0.65f, 0f), new Vector3(0.65f, 1f));

        level.addTimers(3f);
        level.mLevelHelper = "New enemy : It can cross the borders.";
        return level;
    }
    public static Level level_4_mid_flyer()
    {
        Level level = new Level("level_4_mid_flyer", 50);

        float speed = 7f;
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(10f);
        return level;
    }
    public static Level level_4_two_horizontale_flyer_linked()
    {
        Level level = new Level("level_4_two_horizontale_flyer_linked", 50);

        float speed = 9f;
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(2)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(4f);
        return level;
    }
    public static Level level_4_diag_flyer()
    {
        Level level = new Level("level_4_diag_flyer", 50);

        float speed = 8f;
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(5f);
        return level;
    }
    public static Level level_4_flyer_in_line()
    {
        Level level = new Level("level_4_flyer_in_line", 50);

        float speed = 8f;
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        for(int i = 0; i < 6; ++i)
        {
            level.addEnemy(new EnemyInfo(EnemyType.Flyer, new Vector2(0.2f + 0.1f * i, 0.5f), Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        }

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(4,5), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(5, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(5, 6), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(2.5f);
        return level;
    }
    public static Level level_4_cross_flyer_link_cross()
    {
        Level level = new Level("level_4_cross_flyer_link_cross", 50);

        float speed = 8f;
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(2)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(4)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(9f);
        return level;
    }
    public static Level level_4_cross_flyer_link_square()
    {
        Level level = new Level("level_4_cross_flyer_link_square", 50);

        float speed = 8f;
        float speed_2 = 5f;
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(2)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UL * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(3)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(4)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DR * speed_2, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(1)));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(9f);
        return level;
    }
    public static Level level_4_three_flyers_linked()
    {
        Level level = new Level("level_4_three_horitontale_flyers_linked", 50);

        float speed = 9f;

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(4, 8), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(0, 2)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(4, 2), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(3, 5)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(5.5f);
        return level;
    }
    public static Level level_4_cross_flyer_link_hexagone()
    {
        Level level = new Level("level_4_cross_flyer_link_square", 50);

        float speed = 8f;
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(2)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(3)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Utils.R  * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(0, 1)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(2, 3)));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));


        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(6.5f);
        return level;
    }

    public static Level level_4_five_flyers_linked()
    {
        Level level = new Level("level_4_three_horitontale_flyers_linked", 50);

        float speed = 9f;
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(5)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(1, 5), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(1)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(2)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(5, 9), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(3)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(4)));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(10)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(7, 5), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(6)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(7)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(5, 3), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(8)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(9)));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(6f);
        return level;
    }
    public static Level level_4_five_flyers_linked_2_directions()
    {
        Level level = new Level("level_4_five_flyers_linked_2_directions", 50);

        float speed = 6f;
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(4, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(5)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(1, 5), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(1)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(2)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(5, 9), Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(3)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(4)));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(10)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(7, 5), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(6)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(7)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(5, 3), Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(8)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(9)));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 1, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(15)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(1, 5), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(11)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(12)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(5, 9), Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(13)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 9, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(14)));

        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 7, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(20)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(7, 5), Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(16)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 5, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(17)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, Utils.getMidRelativePositionFromPosition(5, 3), Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(18)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 3, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, Utils.Linker(19)));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));


        level.addStar(new StarInfo(1, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(3, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(7, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(9, Utils.STAR_DEFAULT_SCALE));

        level.addTimers(30f);
        return level;
    }

    public static Level level_4_flyer_with_follower()
    {
        Level level = new Level("level_4_flyer_with_follower", 50);

        float speed = 6f;

        level.addEnemy(new EnemyFollowerInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, false, Utils.Linker(1, 2, 3, 4, 5, 6, 7, 8)));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 2, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 4, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 6, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Flyer, 8, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(1, 5, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(3, 5, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(7, 5, 0.25f), Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(Utils.getMidRelativePositionFromPosition(9, 5, 0.25f), Utils.STAR_DEFAULT_SCALE));

        level.addTimers(12.5f);
        return level;
    }

    public static Level level_5_basic_driller()
    {
        Level level = new Level("level_5_basic_driller", 50);

        float speed = 8f;

        level.addEnemy(new EnemyDrillerInfo(2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addStars(1, 2, 3, 4, 6, 7, 8, 9);

        level.mLevelHelper = "New Enemy : It will chase you throught the border.";

        level.addTimers(3.5f);
        return level;
    }
    public static Level level_5_Double_driller()
    {
        Level level = new Level("level_5_Double_driller", 50);

        float speed = 11f;

        level.addEnemy(new EnemyDrillerInfo(2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(4, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, true, true));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addStars(1, 2, 3);
        level.addStars(
            Utils.getMidRelativePositionFromPosition(new Vector2(0f, 0.25f), 1),
            Utils.getMidRelativePositionFromPosition(1, 2),
            Utils.getMidRelativePositionFromPosition(2, 3),
            Utils.getMidRelativePositionFromPosition(3, new Vector2(1f, 0.25f)));

        level.mLevelHelper = "Be careful to not get caught up.";

        level.addTimers(3f);
        return level;
    }
    public static Level level_5_Quad_driller()
    {
        Level level = new Level("level_5_basic_driller", 50);

        float speed = 8f;

        level.addEnemy(new EnemyDrillerInfo(2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(8, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(4, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addStars(1, 2, 3, 4, 6, 7, 8, 9);

        level.addTimers(3.5f);
        return level;
    }
    public static Level level_5_Double_driller_link()
    {
        Level level = new Level("level_5_basic_driller", 50);

        float speed = 8f;

        level.addEnemy(new EnemyDrillerInfo(2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, false, false, Utils.Linker(1)));
        level.addEnemy(new EnemyDrillerInfo(8, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addStars(1, 2, 3, 4, 6, 7, 8, 9);

        level.addTimers(6.5f);
        return level;
    }
    public static Level level_5_Quad_driller_link_cross()
    {
        Level level = new Level("level_5_Quad_driller_link_cross", 50);

        float speed = 8f;

        level.addEnemy(new EnemyDrillerInfo(2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, false, false, Utils.Linker(1)));
        level.addEnemy(new EnemyDrillerInfo(8, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(4, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, false, false, Utils.Linker(3)));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addStars(1, 2, 3, 4, 6, 7, 8, 9);

        level.addTimers(17f);
        return level;
    }
    public static Level level_5_Quad_driller_link_square()
    {
        Level level = new Level("level_5_Quad_driller_link_square", 50);

        float speed = 8f;

        level.addEnemy(new EnemyDrillerInfo(2, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, false, false,  Utils.Linker(2)));
        level.addEnemy(new EnemyDrillerInfo(8, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, false, false,  Utils.Linker(3)));
        level.addEnemy(new EnemyDrillerInfo(4, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, false, false,  Utils.Linker(1)));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, false, false, Utils.Linker(0)));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addStars(1, 2, 3, 4, 6, 7, 8, 9);

        level.addTimers(40f);
        return level;
    }
    public static Level level_5_driller_fast()
    {
        Level level = new Level("level_5_driller_fast", 50);

        float speed = 24f;

        level.addEnemy(new EnemyDrillerInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addStars(1, 2, 3, 4, 6, 7, 8, 9);

        level.addTimers(5f);
        return level;
    }
    public static Level level_5_quad_driller_split_in_2()
    {
        Level level = new Level("level_5_quad_driller_split_in_2", 50);

        float speed = 8f;

        level.addEnemy(new EnemyDrillerInfo(4, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(4, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStars(1, 4, 7, 3, 6, 9);

        level.addTrail(new Vector3(0.5f, 0f), new Vector3(0.5f, 1f));

        level.addTimers(4f);
        return level;
    }
    public static Level level_5_quad_driller_split_in_2_linked()
    {
        Level level = new Level("level_5_quad_driller_split_in_2_linked", 50);
        
        float speed = 8f;

        level.addEnemy(new EnemyDrillerInfo(4, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, false, false, Utils.Linker(1)));
        level.addEnemy(new EnemyDrillerInfo(4, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, false, false, Utils.Linker(3)));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStars(1, 4, 7, 3, 6, 9);
        level.addTrail(new Vector3(0.5f, 0f), new Vector3(0.5f, 1f));

        level.addTimers(13.5f);
        return level;
    }
    public static Level level_5_quad_driller_in_small_area()
    {
        Level level = new Level("level_5_double_driller_in_small_area", 50);

        float speed = 11f;

        level.addEnemy(new EnemyDrillerInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStars(5);

        level.addTrail(new Vector3(0.3f, 0.3f), new Vector3(0.3f, 0.7f), new Vector3(0.7f, 0.7f), new Vector3(0.7f, 0.3f), new Vector3(0.3f, 0.3f));

        level.addTimers(3f);
        return level;
    }
    public static Level level_5_double_driller_in_small_area_linked()
    {
        Level level = new Level("level_5_double_driller_in_small_area_linked", 50);

        float speed = 8f;

        level.addEnemy(new EnemyDrillerInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, false, false, Utils.Linker(1)));
        level.addEnemy(new EnemyDrillerInfo(5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(1, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(3, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(7, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, Utils.getMidRelativePositionFromPosition(9, 5), Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStars(5);

        level.addTrail(new Vector3(0.3f, 0.3f), new Vector3(0.3f, 0.7f), new Vector3(0.7f, 0.7f), new Vector3(0.7f, 0.3f), new Vector3(0.3f, 0.3f));

        level.addTimers(2f);
        return level;
    }
    public static Level level_5_quad_driller_with_follower_linked()
    {
        Level level = new Level("level_5_double_driller_in_small_area_linked", 50);

        float speed = 8f;

        level.addEnemy(new EnemyFollowerInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, false, Utils.Linker(1, 2, 3, 4)));

        level.addEnemy(new EnemyDrillerInfo(4, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(4, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE));
        level.addEnemy(new EnemyDrillerInfo(6, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE));

        level.addStars(2,4,6,8);
        level.addStars(
            Utils.getMidRelativePositionFromPosition(1, 5),
            Utils.getMidRelativePositionFromPosition(3, 5),
            Utils.getMidRelativePositionFromPosition(7, 5),
            Utils.getMidRelativePositionFromPosition(9, 5));

        level.addTimers(40f);
        return level;
    }

    public static Level level_with_driller()
    {
        Level level = new Level("level_with_driller", 50);
        float speed = 10f;
        level.addEnemy(new EnemyDrillerInfo(5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE * 0.5f));
        level.addEnemy(new EnemyInfo(EnemyType.Basic, 5, Vector2.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        return level;
    }
    public static Level level_with_dummy()
    {
        Level level = new Level("level_with_dummy", 50);
        level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector3.zero, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector3.zero, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Dummy, 5, Vector3.zero, Utils.ENEMY_DEFAULT_SCALE));
        //level.addEnemy(new EnemyInfo(EnemyType.Dummy, 8, Vector3.zero, Utils.ENEMY_DEFAULT_SCALE));

        level.addStar(new StarInfo(2, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(4, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(6, Utils.STAR_DEFAULT_SCALE));
        level.addStar(new StarInfo(8, Utils.STAR_DEFAULT_SCALE));
        return level;
    }

    public static Level level_6_basic_faster()
    {
        Level level = new Level("level_6_basic_faster", 50);
        level.addEnemy(new EnemyFasterInfo(Utils.getMidRelativePositionFromPosition(new Vector3(0f, 0.5f), 4),
            Utils.L * 7f, Utils.ENEMY_DEFAULT_SCALE, 5, 1.5f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(Utils.getMidRelativePositionFromPosition(new Vector3(1f, 0.5f), 6),
            Utils.R * 7f, Utils.ENEMY_DEFAULT_SCALE, 5, 1.5f, 1.25f));

        level.addEnemy(new EnemyFasterInfo(5, Utils.D * 8f, Utils.ENEMY_DEFAULT_SCALE, 3, 2f, 2f));

        level.addStars(5);

        level.mLevelHelper = "New enemy : Accelerate each hit until 0 then start again.";

        level.addTrail(new Vector3(0.25f, 0f), new Vector3(0.25f, 1f));
        level.addTrail(new Vector3(0.75f, 0f), new Vector3(0.75f, 1f));
        return level;
    }
    public static Level level_6_faster_counter_up()
    {
        Level level = new Level("level_6_faster_counter_up", 50);

        float speed = 8f;

        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 2, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 3, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 4, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.25f));

        level.addStars(4, 5, 6);
        return level;
    }

    public static Level level_6_one_faster_counter_high()
    {
        Level level = new Level("level_6_one_faster_counter_high", 50);

        float speed = 5f;

        level.addEnemy(new EnemyFasterInfo(5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, 8, 0.5f, 1.2f));

        level.addStars(5);
        level.addStars(
            Utils.getMidRelativePositionFromPosition(4, 5),
            Utils.getMidRelativePositionFromPosition(6, 5)
            );

        level.addTrail(new Vector3(0.25f, 0f), new Vector3(0.25f, 1f));
        level.addTrail(new Vector3(0.75f, 0f), new Vector3(0.75f, 1f));
        return level;
    }
    public static Level level_6_two_faster_linked()
    {
        Level level = new Level("level_6_two_faster_linked", 50);

        float speed = 5f;

        level.addEnemy(new EnemyFasterInfo(5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.5f, Utils.Linker(1)));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.5f));

        level.addStars(4, 5, 6);
        return level;
    }
    public static Level level_6_double_two_faster_linked()
    {
        Level level = new Level("level_6_double_two_faster_linked", 50);

        float speed = 5f;

        level.addEnemy(new EnemyFasterInfo(5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.5f, Utils.Linker(1)));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.5f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.5f, Utils.Linker(3)));
        level.addEnemy(new EnemyFasterInfo(5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.5f));

        level.addStars(4, 5, 6);
        return level;
    }
    public static Level level_6_double_faster_counter_up()
    {
        Level level = new Level("level_6_faster_counter_up", 50);

        float speed = 8f;

        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 2, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 3, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 4, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.25f));

        level.addEnemy(new EnemyFasterInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, 2, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, 3, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, 4, 1f, 1.25f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 1f, 1.25f));

        level.addStars(2, 4, 5, 6, 8);
        level.addStars(Utils.getMidRelativePositionFromPosition(2, 5),
            Utils.getMidRelativePositionFromPosition(4, 5),
            Utils.getMidRelativePositionFromPosition(6, 5),
            Utils.getMidRelativePositionFromPosition(8, 5));
        return level;
    }
    public static Level level_6_faster_stop_on_hit_fast()
    {
        Level level = new Level("level_6_faster_stop_on_hit_fast", 50);

        float speed = 16f;

        level.addEnemy(new EnemyFasterInfo(5, Utils.U * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 0.25f, 1f));
        level.addEnemy(new EnemyFasterInfo(5, Utils.D * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 0.25f, 1f));


        level.addStars(2, 5, 8);
        return level;
    }
    public static Level level_6_faster_on_small_area()
    {
        Level level = new Level("level_6_faster_on_small_area", 50);

        float speed = 5f;

        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 6, 0.5f, 1.25f));

        level.addTrail(new Vector3(0.3f, 0.3f), new Vector3(0.3f, 0.7f), new Vector3(0.7f, 0.7f), new Vector3(0.7f, 0.3f), new Vector3(0.3f, 0.3f));

        level.addStars(5);
        level.addStars(Utils.getMidRelativePositionFromPosition(5, 4),
            Utils.getMidRelativePositionFromPosition(5, 6));
        return level;
    }
    public static Level level_6_diagonale()
    {
        Level level = new Level("level_6_diagonale", 50);

        float speed = 8f;

        foreach(Vector3 direction in new List<Vector3>() { Utils.UR, Utils.UL, Utils.DR, Utils.DL })
        {
            level.addEnemy(new EnemyFasterInfo(5, direction * speed, Utils.ENEMY_DEFAULT_SCALE, 2, 2.25f, 1.2f));
            level.addEnemy(new EnemyFasterInfo(5, direction * speed, Utils.ENEMY_DEFAULT_SCALE, 3, 2.25f, 1.2f));
            level.addEnemy(new EnemyFasterInfo(5, direction * speed, Utils.ENEMY_DEFAULT_SCALE, 4, 2.25f, 1.2f));
            level.addEnemy(new EnemyFasterInfo(5, direction * speed, Utils.ENEMY_DEFAULT_SCALE, 5, 2.25f, 1.2f));
            level.addEnemy(new EnemyFasterInfo(5, direction * speed, Utils.ENEMY_DEFAULT_SCALE, 6, 2.25f, 1.2f));
        }

        level.addStars(1, 2, 3, 4, 5, 6, 7, 8, 9);
        return level;
    }
    public static Level level_6_diagonale_link_counter_1()
    {
        Level level = new Level("level_6_diagonale_link_counter_1", 50);

        float speed = 8f;

        level.addEnemy(new EnemyFasterInfo(5, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1f, Utils.Linker(1)));
        level.addEnemy(new EnemyFasterInfo(5, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1f, Utils.Linker(2)));
        level.addEnemy(new EnemyFasterInfo(5, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1f, Utils.Linker(3)));
        level.addEnemy(new EnemyFasterInfo(5, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1f, Utils.Linker(0)));


        level.addStars(1, 2, 3, 4, 5, 6, 7, 8, 9);
        return level;
    }

    public static Level level_6_diagonale_link_counter_1_with_center()
    {
        Level level = new Level("level_6_diagonale_link_counter_1_with_center", 50);

        float speed = 8f;

        level.addEnemy(new EnemyFasterInfo(8, Utils.UR * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1f, Utils.Linker(1)));
        level.addEnemy(new EnemyFasterInfo(8, Utils.UL * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1f, Utils.Linker(2)));
        level.addEnemy(new EnemyFasterInfo(2, Utils.DL * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1f, Utils.Linker(3)));
        level.addEnemy(new EnemyFasterInfo(2, Utils.DR * speed, Utils.ENEMY_DEFAULT_SCALE, 1, 1f, 1f, Utils.Linker(0)));

        level.addTrail(new Vector3(0.3f, 0.3f), new Vector3(0.3f, 0.7f), new Vector3(0.7f, 0.7f), new Vector3(0.7f, 0.3f), new Vector3(0.3f, 0.3f));

        level.addStars(1, 2, 3, 4, 6, 7, 8, 9);
        return level;
    }

    public static Level level_6_link_line_counter_increase()
    {
        Level level = new Level("level_6_link_line_counter_increase", 50);

        float speed = 7f;

        level.addEnemy(new EnemyFasterInfo(8, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, 2, 1.5f, 2f, Utils.Linker(1)));
        level.addEnemy(new EnemyFasterInfo(8, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 2, 1.5f, 2f));
        level.addEnemy(new EnemyFasterInfo(2, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, 2, 1.5f, 2f, Utils.Linker(3)));
        level.addEnemy(new EnemyFasterInfo(2, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 2, 1.5f, 2f));

        level.addEnemy(new EnemyFasterInfo(5, Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, 4, 1f, 1.5f, Utils.Linker(5)));
        level.addEnemy(new EnemyFasterInfo(5, Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 4, 1f, 1.5f));

        level.addEnemy(new EnemyFasterInfo(Utils.getMidRelativePositionFromPosition(5, 8),
            Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, 3, 1.25f, 1.75f, Utils.Linker(7)));
        level.addEnemy(new EnemyFasterInfo(Utils.getMidRelativePositionFromPosition(5, 8),
            Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 3, 1.25f, 1.75f));
        level.addEnemy(new EnemyFasterInfo(Utils.getMidRelativePositionFromPosition(5, 2),
            Utils.L * speed, Utils.ENEMY_DEFAULT_SCALE, 3, 1.25f, 1.75f, Utils.Linker(9)));
        level.addEnemy(new EnemyFasterInfo(Utils.getMidRelativePositionFromPosition(5, 2),
            Utils.R * speed, Utils.ENEMY_DEFAULT_SCALE, 3, 1.25f, 1.75f));

        level.addStars(1, 2, 3, 4, 5, 6, 7, 8, 9);
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
    public void addTimers(float bronze, float silver, float gold)
    {
        mBronzeTime = bronze;
        mSilverTime = silver;
        mGoldTime = gold;
    }

    public string getGoalTime(float timer)
    {
        if (timer < 0f)
        {
            return Utils.getTimeFromFloat(mBronzeTime);
        }
        else if (timer < mGoldTime)
        {
            return Utils.getTimeFromFloat(mGoldTime);
        }
        else if (timer < mSilverTime)
        {
            return Utils.getTimeFromFloat(mGoldTime);
        }
        else if (timer < mBronzeTime)
        {
            return Utils.getTimeFromFloat(mSilverTime);
        }
        else
        {
            return Utils.getTimeFromFloat(mBronzeTime);
        }
    }
    public Material getGoalMaterial(float timer)
    {
        if (timer < 0f)
        {
            return null;
        }
        else if (timer < mGoldTime)
        {
            return Resources.Load<Material>("Materials/UI/ChronometerGoalGold");
        }
        else if (timer < mSilverTime)
        {
            return Resources.Load<Material>("Materials/UI/ChronometerGoalSilver");
        }
        else if (timer < mBronzeTime)
        {
            return Resources.Load<Material>("Materials/UI/ChronometerGoalBronze");
        }
        else
        {
            return null;
        }
    }

}


public class Worlds
{
    public static List<Levels> worlds;
    private static Levels world_1()
    {
        Levels levels = new Levels(12);

        levels.levels_name = "World 1 - Countach";
        levels.mWorldMusic = "Synthwave/Countach";
        levels.mWorldColorPrincipal_1 = Color.magenta;
        levels.mWorldColorPrincipal_2 = Color.cyan;
        levels.mWorldColorSecond_1 = new Color(1f, 0.15f, 0f);
        levels.mWorldColorSecond_2 = Color.yellow;
        levels.mWorldHue = 0f;

        levels.addLevel(1, Levels.level_1_one_star());
        levels.addLevel(2, Levels.level_1_only_stars());
        levels.addLevel(3, Levels.level_1_one_slow_enemy());
        levels.addLevel(4, Levels.level_1_vertical_enemies());
        levels.addLevel(5, Levels.level_1_3());
        levels.addLevel(6, Levels.level_1_7());
        //levels.addLevel(4, Levels.level_1_2_medium_enemy_horizontal());
        //levels.addLevel(4, Levels.level_1_4());
        //levels.addLevel(7, Levels.level_1_demon_square()); // Could be removed
        levels.addLevel(7, Levels.level_1_demon_line()); // Could be removed
        levels.addLevel(8, Levels.level_1_9());
        levels.addLevel(9, Levels.level_1_12());
        levels.addLevel(10, Levels.level_1_11());
        levels.addLevel(11, Levels.level_1_basic_link());
        levels.addLevel(12, Levels.level_1_basic_link_2());

        //levels.addLevel(11, Levels.level_1_11()); // Either level 10 or 11 can be removed
        //levels.addLevel(12, Levels.level_1_13()); // COuld be remvoed too
        //levels.addLevel(2, Levels.level_1_15());

        levels.mBronzeTime = Utils.getSeconds(10, 00);
        levels.mSilverTime = Utils.getSeconds(5, 00);
        levels.mGoldTime = Utils.getSeconds(2, 30);

        return levels;
    }
    private static Levels world_2()
    {
        Levels levels = new Levels(12);

        levels.levels_name = "World 2 - Galaxy";
        levels.mWorldMusic = "Synthwave/20 - Galaxy";
        levels.mWorldColorPrincipal_1 = Color.yellow;
        levels.mWorldColorPrincipal_2 = new Color(1f, 0.15f, 0f);
        levels.mWorldColorSecond_1 = Color.magenta;
        levels.mWorldColorSecond_2 = Color.cyan;
        levels.mWorldHue = 155f;
        //levels.addLevel(Levels.level_1_1());
        //levels.addLevel(Levels.level_1_2());
        levels.addLevel(1, Levels.level_2_basic_square_circle());
        levels.addLevel(2, Levels.level_2_circle_show_form());

        levels.addLevel(3, Levels.level_2_basic_line());
        levels.addLevel(4, Levels.level_2_basic_line_with_inverse());
        levels.addLevel(5, Levels.level_2_basic_square()); // OK - Medium
        levels.addLevel(6, Levels.level_2_octogone_turning()); // OK
        levels.addLevel(7, Levels.level_2_three_protection_with_diff_speed()); // MEDIUM
        levels.addLevel(8, Levels.level_2_basic_square_with_link()); 
        levels.addLevel(9, Levels.level_2_three_protection_with_diff_speed_with_link()); 
        levels.addLevel(10, Levels.level_2_basic_line_with_inverse_with_link()); 
        levels.addLevel(11, Levels.level_2_line_basic_with_cross_circle()); // Medium - hard
        levels.addLevel(12, Levels.level_2_triple_triangle()); // HARD

        levels.mBronzeTime = Utils.getSeconds(18, 00);
        levels.mSilverTime = Utils.getSeconds(12, 00);
        levels.mGoldTime = Utils.getSeconds(6, 00);
        //levels.addLevel(8, Levels.level_3_Basic_Follower());
        //levels.addLevel(9, Levels.level_3_Double_Follower());
        //levels.addLevel(10, Levels.level_with_driller());
        //levels.addLevel(11, Levels.level_with_Link());
        //levels.addLevel(11, Levels.level_1_demon_square());
        //levels.addLevel(12, Levels.level_3_one_fast_follower());


        //levels.addLevel(8, Levels.level_2_circle_enemy_hitting_the_border()); // TO remove
        //levels.addLevel(9, Levels.level_2_four_square_enemy_turning()); // Slower - Hard ++ (remove ?)
        //levels.addLevel(10, Levels.level_2_multi_triangle()); // Slower - Hard ++ (remove ?)
        //levels.addLevel(11, Levels.level_2_line_going_to_the_center()); // Slower
        //levels.addLevel(12, Levels.level_2_lot_of_speed()); // Bof

        return levels;
    }
    private static Levels world_3()
    {
        Levels levels = new Levels(12);
        levels.levels_name = "World 3 - Dangerous";
        levels.mWorldMusic = "Synthwave/32 - Dangerous";

        levels.mWorldColorPrincipal_1 = Color.red;
        levels.mWorldColorPrincipal_2 = new Color(1f, 0f, 0.75f);
        levels.mWorldColorSecond_1 = Color.blue;
        levels.mWorldColorSecond_2 = Color.cyan;
        levels.mWorldHue = 280;

        levels.addLevel(1, Levels.level_3_Double_Follower());
        levels.addLevel(2, Levels.level_3_Basic_Follower_moving());
        levels.addLevel(3, Levels.level_3_Basic_Follower());
        levels.addLevel(4, Levels.level_3_Triple_Follower());
        levels.addLevel(5, Levels.level_3_Double_Follower_split_in_2()); 
        levels.addLevel(6, Levels.level_3_Double_Follower_Link());
        levels.addLevel(7, Levels.level_3_Quad_Follower());
        levels.addLevel(8, Levels.level_3_Quad_Follower_split_in_2());
        levels.addLevel(9, Levels.level_3_Quad_Follower_Cross_Link());
        levels.addLevel(10, Levels.level_3_Quad_Follower_In_Cell_Move());
        levels.addLevel(11, Levels.level_3_Basic_Follower_Faster());
        levels.addLevel(12, Levels.level_3_Quad_Follower_Square_Link());
        //levels.addLevel(3, Levels.level_3_one_fast_follower());

        levels.mBronzeTime = Utils.getSeconds(20, 00);
        levels.mSilverTime = Utils.getSeconds(14, 00);
        levels.mGoldTime = Utils.getSeconds(8, 00);

        return levels;
    }
    private static Levels world_4()
    {
        Levels levels = new Levels(12);
        levels.levels_name = "World 4 - Last Stop";
        levels.mWorldMusic = "Synthwave/Last Stop";

        levels.mWorldColorPrincipal_1 = new Color(1f, 0f, 0.75f);
        levels.mWorldColorPrincipal_2 = Color.blue;
        levels.mWorldColorSecond_1 = Color.red;
        levels.mWorldColorSecond_2 = Color.magenta;
        levels.mWorldHue = 310;

        levels.addLevel(1, Levels.level_4_basic_flyer());
        levels.addLevel(2, Levels.level_4_mid_flyer());
        levels.addLevel(3, Levels.level_4_two_horizontale_flyer_linked());
        levels.addLevel(4, Levels.level_4_diag_flyer());
        levels.addLevel(5, Levels.level_4_flyer_in_line());
        levels.addLevel(6, Levels.level_4_cross_flyer_link_cross());
        levels.addLevel(7, Levels.level_4_three_flyers_linked());
        levels.addLevel(8, Levels.level_4_cross_flyer_link_square());
        levels.addLevel(9, Levels.level_4_five_flyers_linked());
        levels.addLevel(10, Levels.level_4_cross_flyer_link_hexagone());
        levels.addLevel(11, Levels.level_4_five_flyers_linked_2_directions());
        levels.addLevel(12, Levels.level_4_flyer_with_follower());

        levels.mBronzeTime = Utils.getSeconds(24, 00);
        levels.mSilverTime = Utils.getSeconds(17, 00);
        levels.mGoldTime = Utils.getSeconds(10, 00);

        return levels;
    }
    private static Levels world_5()
    {
        Levels levels = new Levels(12);
        levels.levels_name = "World 5 - The Saga";
        levels.mWorldMusic = "Synthwave/68 - The Saga";

        levels.mWorldColorPrincipal_1 = Color.green; 
        levels.mWorldColorPrincipal_2 = Color.cyan;
        levels.mWorldColorSecond_1 = Color.yellow; 
        levels.mWorldColorSecond_2 = new Color(1f, 0.15f, 0f);
        levels.mWorldHue = 260f;

        //levels.addLevel(1, Levels.level_6_basic_faster());
        levels.addLevel(1, Levels.level_5_basic_driller());
        levels.addLevel(2, Levels.level_5_Double_driller());
        levels.addLevel(3, Levels.level_5_Quad_driller());
        levels.addLevel(4, Levels.level_5_Double_driller_link());
        levels.addLevel(5, Levels.level_5_Quad_driller_link_cross());
        levels.addLevel(6, Levels.level_5_driller_fast());
        levels.addLevel(7, Levels.level_5_quad_driller_split_in_2());
        levels.addLevel(8, Levels.level_5_quad_driller_with_follower_linked());
        levels.addLevel(9, Levels.level_5_quad_driller_split_in_2_linked());
        levels.addLevel(10, Levels.level_5_quad_driller_in_small_area());
        levels.addLevel(11, Levels.level_5_double_driller_in_small_area_linked());
        levels.addLevel(12, Levels.level_5_Quad_driller_link_square());

        levels.mBronzeTime = Utils.getSeconds(25, 00);
        levels.mSilverTime = Utils.getSeconds(17, 00);
        levels.mGoldTime = Utils.getSeconds(12, 00);
        return levels;
    }
    private static Levels world_6()
    {
        Levels levels = new Levels(12);
        levels.levels_name = "World 6 - Afterglow";
        levels.mWorldMusic = "Synthwave/Afterglow";

        levels.mWorldColorPrincipal_1 = Color.green;
        levels.mWorldColorPrincipal_2 = Color.yellow;
        levels.mWorldColorSecond_1 = Color.green;
        levels.mWorldColorSecond_2 = Color.magenta;
        levels.mWorldHue = 135f;

        levels.addLevel(1, Levels.level_6_basic_faster());

        levels.addLevel(2, Levels.level_6_faster_counter_up());
        levels.addLevel(3, Levels.level_6_one_faster_counter_high());
        levels.addLevel(4, Levels.level_6_two_faster_linked());
        levels.addLevel(5, Levels.level_6_faster_stop_on_hit_fast());
        levels.addLevel(6, Levels.level_6_faster_on_small_area());
        levels.addLevel(7, Levels.level_6_diagonale_link_counter_1_with_center());
        levels.addLevel(8, Levels.level_6_double_faster_counter_up());

        levels.addLevel(9, Levels.level_6_diagonale());
        levels.addLevel(10, Levels.level_6_double_two_faster_linked());
        levels.addLevel(11, Levels.level_6_diagonale_link_counter_1());
        levels.addLevel(12, Levels.level_6_link_line_counter_increase());

        levels.mBronzeTime = Utils.getSeconds(25, 00);
        levels.mSilverTime = Utils.getSeconds(17, 00);
        levels.mGoldTime = Utils.getSeconds(12, 00);
        return levels;
    }


    public static Level getLevel(int world, int level)
    {
        return worlds[world].levels[level];
    }
    public static Levels getWorld(int world)
    {
        return worlds[world];
    }
    public static Levels getCurrentWorld()
    {
        return worlds[GameControler.currentWorld];
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
        worlds.Add(world_6());
    }
}