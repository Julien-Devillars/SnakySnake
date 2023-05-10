using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class DirectionTests
{

    [UnityTest]
    public IEnumerator test_CheckTimerForDirection()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();

        Assert.IsTrue(character.updateDirection(Direction.Right));

        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsTrue(character.updateDirection(Direction.Up));

        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME / 2f);
        Assert.IsFalse(character.updateDirection(Direction.Left));
    }

    [UnityTest]
    public IEnumerator test_CheckDirectionOnBorder()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();

        // Check Bottom right place
        character.transform.position = new Vector3(character.mMinBorderPos.x, character.mMinBorderPos.y, 0);
        Vector3 old_pos = character.transform.position;

        // Right
        Assert.IsTrue(character.updateDirection(Direction.Right));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Right));
        Assert.IsTrue(character.updateDirection(Direction.Left));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsTrue(old_pos == character.transform.position);
        // Up
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Up));
        Assert.IsTrue(character.updateDirection(Direction.Down));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsTrue(old_pos == character.transform.position);

        character.transform.position = new Vector3(character.mMinBorderPos.x, character.mMaxBorderPos.y, 0);
        old_pos = character.transform.position;

        // Right
        Assert.IsTrue(character.updateDirection(Direction.Right));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Right));
        Assert.IsTrue(character.updateDirection(Direction.Left));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsTrue(old_pos == character.transform.position);
        // Down
        Assert.IsTrue(character.updateDirection(Direction.Down));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsTrue(old_pos == character.transform.position);

        character.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMaxBorderPos.y, 0);
        old_pos = character.transform.position;

        // Left
        Assert.IsTrue(character.updateDirection(Direction.Left));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Left));
        Assert.IsTrue(character.updateDirection(Direction.Right));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsTrue(old_pos == character.transform.position);
        // Down
        Assert.IsTrue(character.updateDirection(Direction.Down));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Down));
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsTrue(old_pos == character.transform.position);

        character.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMinBorderPos.y, 0);
        old_pos = character.transform.position;

        // Left
        Assert.IsTrue(character.updateDirection(Direction.Left));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Left));
        Assert.IsTrue(character.updateDirection(Direction.Right));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsTrue(old_pos == character.transform.position);
        // Up
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Up));
        Assert.IsTrue(character.updateDirection(Direction.Down));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsTrue(old_pos == character.transform.position);
    }

    [UnityTest]
    public IEnumerator test_CheckDirectionInCanvas()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();

        // Up
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Up));
        Assert.IsFalse(character.updateDirection(Direction.Down));
        // Left
        Assert.IsTrue(character.updateDirection(Direction.Left));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Left));
        Assert.IsFalse(character.updateDirection(Direction.Right));
        // Up
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Up));
        Assert.IsFalse(character.updateDirection(Direction.Down));
        // Right
        Assert.IsTrue(character.updateDirection(Direction.Right));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);
        Assert.IsFalse(character.updateDirection(Direction.Right));
        Assert.IsFalse(character.updateDirection(Direction.Left));
        // Down
        Assert.IsTrue(character.updateDirection(Direction.Down));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Down));
        Assert.IsFalse(character.updateDirection(Direction.Up));
    }

    [UnityTest]
    public IEnumerator test_CheckDirectionInBackground()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        Vector3 old_pos = new Vector3();

        // Check Bottom right place
        character.transform.position = character.mMinBorderPos;
        old_pos = character.transform.position;

        // Right
        Assert.IsTrue(character.updateDirection(Direction.Right));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 4f);

        // Up until reaching the top
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);
        
        // Left
        Assert.IsTrue(character.updateDirection(Direction.Left));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 2f);

        // Down
        Assert.IsTrue(character.updateDirection(Direction.Down));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME * 3f);

        // Up
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Up));
        Assert.IsTrue(character.updateDirection(Direction.Down));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Left
        Assert.IsTrue(character.updateDirection(Direction.Left));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Left));
        Assert.IsTrue(character.updateDirection(Direction.Right));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Down
        Assert.IsTrue(character.updateDirection(Direction.Down));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Down));
        Assert.IsTrue(character.updateDirection(Direction.Up));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Right
        Assert.IsTrue(character.updateDirection(Direction.Right));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        Assert.IsFalse(character.updateDirection(Direction.Right));
        Assert.IsTrue(character.updateDirection(Direction.Left));
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
    }
}
