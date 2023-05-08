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

        character.transform.position = character.mMinBorderPos;

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

        character.transform.position = character.mMinBorderPos;

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

        character.transform.position = new Vector3(character.mMinBorderPos.x, character.mMaxBorderPos.y, 0);

        // Second split -> Horizontal 2
        character.updateDirection(Direction.Down);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        checkNumberOfBackgrounds(3);
        checkBackgroundAreEquals();

        character.transform.position = character.mMinBorderPos;

        // Third split -> Vertical 1
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(4);
        checkBackgroundAreEquals();

        character.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMinBorderPos.y, 0);

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

        character.transform.position = new Vector3(character.mMinBorderPos.x, character.mMaxBorderPos.y, 0);

        checkNumberOfBackgrounds(2);
        checkBackgroundAreEquals();

        character.transform.position = character.mMinBorderPos;

        // Second split -> Vertical 1
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(3);
        checkBackgroundAreEquals();

        character.transform.position = new Vector3(character.mMinBorderPos.x, character.mMaxBorderPos.y, 0);

        // Third split -> Horizontal 2
        character.updateDirection(Direction.Down);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        checkNumberOfBackgrounds(4);
        checkBackgroundAreEquals();

        character.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMinBorderPos.y, 0);

        // Fourth split -> Vertical 2
        character.updateDirection(Direction.Left);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        checkNumberOfBackgrounds(5);
        checkBackgroundAreEquals();
    }

}
