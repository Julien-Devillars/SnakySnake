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
        Assert.AreEqual(bgs_go.Count, number_bg_expected);
    }

    public static void checkBackgroundAreEquals()
    {
        List<GameObject> bgs_go = getBackgroundsGameObject();

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();

        Assert.AreEqual(bgs_go.Count, character.mBackgrounds.Count);
        for (int i = 0; i < character.mBackgrounds.Count; ++i)
        {
            Background character_bg = character.mBackgrounds[i];
            GameObject character_bg_go = character_bg.mBackground;
            GameObject bg_go = bgs_go[i];

            Assert.AreEqual(character_bg_go, bg_go);
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
    public static void setEnemyPositionInAnchor(CharacterBehavior character, GameObject enemy, string anchor_position)
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

    public static IEnumerator move(CharacterBehavior character, string movement)
    {
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
    }
    public static IEnumerator moveUntilBorder(CharacterBehavior character, char movement)
    {
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
}
