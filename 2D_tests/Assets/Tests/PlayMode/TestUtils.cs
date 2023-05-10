using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TestUtils
{
    public static List<GameObject> getBackgroundsGameObject()
    {
        List<GameObject> bgs_go = new List<GameObject>();

        GameObject bg_parent = GameObject.Find(Utils.BACKGROUND_STR);
        for (int i = 0; i < bg_parent.transform.childCount; ++i)
        {
            GameObject child = bg_parent.transform.GetChild(i).gameObject;
            bgs_go.Add(child);
        }
        return bgs_go;
    }

    public static void checkNumberOfBackgrounds(int number_bg_expected)
    {
        List<GameObject> bgs_go = getBackgroundsGameObject();
        Assert.AreEqual(number_bg_expected, bgs_go.Count);
    }
    public static void checkBackgroundsHasEnnemy(List<bool> has_enemies)
    {
        CharacterBehavior character = getCharacter();
        Assert.AreEqual(has_enemies.Count, character.mBackgrounds.Count);

        for (int i = 0; i < has_enemies.Count; ++i)
        {
            bool has_enemy = has_enemies[i];
            Assert.AreEqual(has_enemy, character.mBackgrounds[i].hasEnemies(), "Issue on background : " + i);
        }
    }
    public static void checkBackgroundHasEnnemy(int backgroundg_idx, bool has_enemy)
    {
        CharacterBehavior character = getCharacter();
        Assert.AreEqual(has_enemy, character.mBackgrounds[backgroundg_idx].hasEnemies());
    }

    public static void checkBackgroundAreEquals()
    {
        List<GameObject> bgs_go = getBackgroundsGameObject();

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();

        Assert.AreEqual(character.mBackgrounds.Count, bgs_go.Count);
        for (int i = 0; i < character.mBackgrounds.Count; ++i)
        {
            Background character_bg = character.mBackgrounds[i];
            GameObject character_bg_go = character_bg.mBackground;
            GameObject bg_go = bgs_go[i];

            Assert.AreEqual(bg_go, character_bg_go);
        }
    }

    public static void setCharacterPositionInAnchor(CharacterBehavior character, string anchor_position)
    {
        switch (anchor_position)
        {
            case "bottom-left":
                character.transform.position = character.mMinBorderPos;
                break;
            case "top-left":
                character.transform.position = new Vector3(character.mMinBorderPos.x, character.mMaxBorderPos.y, 0);
                break;
            case "bottom-right":
                character.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMinBorderPos.y, 0);
                break;
            case "top-right":
                character.transform.position = character.mMaxBorderPos;
                break;
            case "right":
                character.transform.position = new Vector3(character.mMaxBorderPos.x, 0, 0);
                break;
            case "bottom":
                character.transform.position = new Vector3(0, character.mMinBorderPos.y, 0);
                break;
            case "left":
                character.transform.position = new Vector3(character.mMinBorderPos.x, 0, 0);
                break;
            case "top":
                character.transform.position = new Vector3(0, character.mMaxBorderPos.y, 0);
                break;
            default:
                Debug.Log("Anchor position not found");
                break;
        }
    }
    public static void setEnemyPositionInAnchor(CharacterBehavior character, EnemyBehavior enemy, string anchor_position)
    {
        float epsilon = Utils.EPSILON() * 2f;
        switch (anchor_position)
        {
            case "bottom-left":
                enemy.transform.position = new Vector3(character.mMinBorderPos.x + epsilon, character.mMinBorderPos.y + epsilon, 0);
                break;
            case "top-left":
                enemy.transform.position = new Vector3(character.mMinBorderPos.x + epsilon, character.mMaxBorderPos.y - epsilon, 0);
                break;
            case "bottom-right":
                enemy.transform.position = new Vector3(character.mMaxBorderPos.x - epsilon, character.mMinBorderPos.y + epsilon, 0);
                break;
            case "top-right":
                enemy.transform.position = new Vector3(character.mMaxBorderPos.x - epsilon, character.mMaxBorderPos.y - epsilon, 0);
                break;
            case "right":
                enemy.transform.position = new Vector3(character.mMaxBorderPos.x - epsilon, 0, 0);
                break;
            case "bottom":
                enemy.transform.position = new Vector3(0, character.mMinBorderPos.y + epsilon, 0);
                break;
            case "left":
                enemy.transform.position = new Vector3(character.mMinBorderPos.x + epsilon, 0, 0);
                break;
            case "top":
                enemy.transform.position = new Vector3(0, character.mMaxBorderPos.y - epsilon, 0);
                break;
            default:
                Debug.Log("Anchor position not found");
                break;
        }
    }

    public static IEnumerator move(CharacterBehavior character, string movement, List<float> speeds)
    {
        float initial_speed = character.mSpeed;
        Assert.AreEqual(speeds.Count, movement.Length);
        int idx = 0;
        foreach (char move in movement)
        {
            character.mSpeed = speeds[idx];
            if (move == '<')
            {
                character.updateDirection(Direction.Left);
                yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            }
            if (move == '^')
            {
                character.updateDirection(Direction.Up);
                yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            }
            if (move == '>')
            {
                character.updateDirection(Direction.Right);
                yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            }
            if (move == 'v')
            {
                character.updateDirection(Direction.Down);
                yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            }
            idx++;
        }
        character.mSpeed = initial_speed;
    }
    public static IEnumerator move(CharacterBehavior character, string movement, float speed = -1f)
    {
        float previous_speed = character.mSpeed;
        if(speed != -1)
        {
            character.mSpeed = speed;
        }
        foreach (char move in movement)
        {
            if (move == '<')
            {
                character.updateDirection(Direction.Left);
                yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            }
            if (move == '^')
            {
                character.updateDirection(Direction.Up);
                yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            }
            if (move == '>')
            {
                character.updateDirection(Direction.Right);
                yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            }
            if (move == 'v')
            {
                character.updateDirection(Direction.Down);
                yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            }
        }
        character.mSpeed = previous_speed;
    }
    public static IEnumerator moveUntilBorder(CharacterBehavior character, char movement, float speed = -1f)
    {
        float previous_speed = character.mSpeed;
        if (speed != -1)
        {
            character.mSpeed = speed;
        }
        if (movement == '<')
        {
            character.updateDirection(Direction.Left);
            yield return new WaitUntil(() => character.transform.position.x == character.mMinBorderPos.x);
        }
        if (movement == '^')
        {
            character.updateDirection(Direction.Up);
            yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);
        }
        if (movement == '>')
        {
            character.updateDirection(Direction.Right);
            yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);
        }
        if (movement == 'v')
        {
            character.updateDirection(Direction.Down);
            yield return new WaitUntil(() => character.transform.position.y == character.mMinBorderPos.y);
        }
        character.mSpeed = previous_speed;
    }
    public static IEnumerator moveUntilReachingPoint(CharacterBehavior character, char movement, Border border, float speed = -1f, float offset = 4f)
    {
        yield return moveUntilReachingPoint(character, movement, border.mStartPoint, speed, offset);
    }
    public static IEnumerator moveUntilReachingPoint(CharacterBehavior character, char movement, Trail trail, float speed = -1f, float offset = 4f)
    {
        yield return moveUntilReachingPoint(character, movement, trail.GetComponent<LineRenderer>().GetPosition(0), speed, offset);
    }
    public static IEnumerator moveUntilReachingPoint(CharacterBehavior character, char movement, Vector3 point, float speed = -1f, float offset = 4f)
    {
        float previous_speed = character.mSpeed;
        if (speed != -1)
        {
            character.mSpeed = speed;
        }
        float epsilon = Utils.EPSILON() * offset;
        if (movement == '<')
        {
            character.updateDirection(Direction.Left);
            yield return new WaitUntil(() => character.transform.position.x < point.x + epsilon);
        }
        if (movement == '^')
        {
            character.updateDirection(Direction.Up);
            yield return new WaitUntil(() => character.transform.position.y > point.y - epsilon);
        }
        if (movement == '>')
        {
            character.updateDirection(Direction.Right);
            yield return new WaitUntil(() => character.transform.position.x > point.x - epsilon);
        }
        if (movement == 'v')
        {
            character.updateDirection(Direction.Down);
            yield return new WaitUntil(() => character.transform.position.y < point.y + epsilon);
        }
        character.mSpeed = previous_speed;
    }
    public static bool bordersAreValid(List<Border> borders)
    {
        foreach (Border border in borders)
        {
            if (border.mStartPoint.x == border.mEndPoint.x && border.mStartPoint.y == border.mEndPoint.y)
            {
                return false;
            }
        }
        return true;
    }
    public static CharacterBehavior getCharacter()
    {
        return GameObject.Find(Utils.CHARACTER).GetComponent<CharacterBehavior>();
    }
    public static EnemyBehavior getEnemy(int i)
    {
        GameObject enemies_go = GameObject.Find(Utils.ENEMIES_STR);
        GameObject enemy_go = enemies_go.transform.GetChild(i).gameObject;
        return enemy_go.GetComponent<EnemyBehavior>();
    }
    public static Border getBorder(int i)
    {
        CharacterBehavior character = getCharacter();
        return character.mBorders[i];
    }
    public static Trail getTrail(int i)
    {
        GameObject trails_go = GameObject.Find(Utils.TRAILS_STR);
        GameObject trail_go = trails_go.transform.GetChild(i).gameObject;
        return trail_go.GetComponent<Trail>();
    }
}
