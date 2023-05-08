using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BackgroundTests
{

    public List<GameObject> getBackgroundsGameObject()
    {
        List<GameObject> bgs_go = new List<GameObject>();

        GameObject bg_parent = GameObject.Find(Utils.BACKGROUND_STR);
        for(int i = 0;  i < bg_parent.transform.childCount; ++i)
        {
            GameObject child = bg_parent.transform.GetChild(i).gameObject;
            bgs_go.Add(child);

        }
        return bgs_go;
    }

    public void checkNumberOfBackgrounds(int number_bg_expected)
    {
        List<GameObject> bgs_go = getBackgroundsGameObject();
        Assert.AreEqual(bgs_go.Count, number_bg_expected);
    }

    public void checkBackgroundAreEquals()
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

    public void setCharacterPositionInAnchor(CharacterBehavior character, string anchor_position)
    {
        switch(anchor_position)
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

    public IEnumerator move(CharacterBehavior character, string movement)
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

    [UnityTest]
    public IEnumerator test_CheckBackgroundAtStartUp()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        checkNumberOfBackgrounds(1);
        checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn2_Horizontal()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 30;
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);

        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        checkNumberOfBackgrounds(2);
        checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn2_Vertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 30;

        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);

        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(2);
        checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn4_VerticalHorizontal()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 30;

        // First split -> Vertical
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        setCharacterPositionInAnchor(character, "bottom-left");

        // Second split -> Horizontal
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        checkNumberOfBackgrounds(3);
        checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn4_HorizontalVertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 30;

        // First split -> Horizontal
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        setCharacterPositionInAnchor(character, "bottom-left");

        // Second split -> Vertical
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(3);
        checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn8_Horizontal_Horizontal_Vertical_Vertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 30;

        // First split -> Horizontal 1
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        checkNumberOfBackgrounds(2);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "top-left");

        // Second split -> Horizontal 2
        character.updateDirection(Direction.Down);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        checkNumberOfBackgrounds(3);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "bottom-left");

        // Third split -> Vertical 1
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(4);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "bottom-right");

        // Fourth split -> Vertical 2
        character.updateDirection(Direction.Left);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(5);
        checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn8_Horizontal_Vertical_Horizontal_Vertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 30;

        // First split -> Horizontal 1
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        setCharacterPositionInAnchor(character, "top-left");

        checkNumberOfBackgrounds(2);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "bottom-left");

        // Second split -> Vertical 1
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(3);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "top-left");

        // Third split -> Horizontal 2
        character.updateDirection(Direction.Down);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        checkNumberOfBackgrounds(4);
        checkBackgroundAreEquals();

        character.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMinBorderPos.y, 0);
        setCharacterPositionInAnchor(character, "bottom-right");

        // Fourth split -> Vertical 2
        character.updateDirection(Direction.Left);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(5);
        checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_Background_1Turn_Corners()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 30;

        setCharacterPositionInAnchor(character, "bottom-left");

        // Corner bottom-left
        yield return move(character, ">^<");

        checkNumberOfBackgrounds(3);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "top-left");

        // Corner top-left
        yield return move(character, ">v<");

        checkNumberOfBackgrounds(5);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "top-right");

        // Corner top-right
        yield return move(character, "<v>");

        checkNumberOfBackgrounds(7);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "bottom-right");

        // Corner top-right
        yield return move(character, "<^>");

        checkNumberOfBackgrounds(9);
        checkBackgroundAreEquals();
    }
    [UnityTest]
    public IEnumerator test_Background_MultiVertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 10;

        GameObject enemies_go = GameObject.Find(Utils.ENEMIES_STR);
        Transform enemy = enemies_go.transform.GetChild(0);
        float epsilon = Utils.EPSILON();
        enemy.position = new Vector3(character.mMaxBorderPos.x - epsilon * 2f, 0f, 0f);

        Utils.HAS_ENEMY_COLLISION = false;
        Utils.HAS_SCORE_ACTIVATED = false;

        setCharacterPositionInAnchor(character, "bottom-left");
        int number_expected_bg = 2;
        while(character.transform.position.x < enemy.position.x - epsilon * 4f)
        {
            character.mSpeed = 10;
            // Corner bottom-left
            character.updateDirection(Direction.Right);
            yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            character.mSpeed = 70;
            if(character.transform.position.y == character.mMinBorderPos.y)
            {
                character.updateDirection(Direction.Up);
                yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);
            }
            else
            {
                character.updateDirection(Direction.Down);
                yield return new WaitUntil(() => character.transform.position.y == character.mMinBorderPos.y);
            }
            checkNumberOfBackgrounds(number_expected_bg);
            checkBackgroundAreEquals();
            number_expected_bg++;
        }
    }

    [UnityTest]
    public IEnumerator test_Background_MultiHorizontal()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        character.mSpeed = 10;

        GameObject enemies_go = GameObject.Find(Utils.ENEMIES_STR);
        Transform enemy = enemies_go.transform.GetChild(0);
        float epsilon = Utils.EPSILON();
        enemy.position = new Vector3(0f, character.mMaxBorderPos.y - epsilon * 2f, 0f);

        Utils.HAS_ENEMY_COLLISION = false;
        Utils.HAS_SCORE_ACTIVATED = false;

        setCharacterPositionInAnchor(character, "bottom-left");
        int number_expected_bg = 2;
        while (character.transform.position.y < enemy.position.y - epsilon * 4f)
        {
            character.mSpeed = 10;
            // Corner bottom-left
            character.updateDirection(Direction.Up);
            yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
            character.mSpeed = 90;
            if (character.transform.position.x == character.mMinBorderPos.x)
            {
                character.updateDirection(Direction.Right);
                yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);
            }
            else
            {
                character.updateDirection(Direction.Left);
                yield return new WaitUntil(() => character.transform.position.x == character.mMinBorderPos.x);
            }
            checkNumberOfBackgrounds(number_expected_bg);
            checkBackgroundAreEquals();
            number_expected_bg++;
        }
    }


    [UnityTest]
    public IEnumerator test_Background_2Turns_Sides()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        setCharacterPositionInAnchor(character, "bottom");

        yield return move(character, ">^<<v");
        checkNumberOfBackgrounds(4);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "left");

        yield return move(character, "v>^^<");
        checkNumberOfBackgrounds(7);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "top");

        yield return move(character, "<v>>^");
        checkNumberOfBackgrounds(10);
        checkBackgroundAreEquals();

        setCharacterPositionInAnchor(character, "right");

        yield return move(character, "^<vv>");
        checkNumberOfBackgrounds(13);
        checkBackgroundAreEquals();
    }

}
