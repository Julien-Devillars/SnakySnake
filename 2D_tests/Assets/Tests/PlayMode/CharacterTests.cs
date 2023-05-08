using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CharacterTests
{

    [UnityTest]
    public IEnumerator test_CheckTimerForDirection()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        string character_name = Utils.CHARACTER;

        GameObject character_go = GameObject.Find(character_name);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        //character.timer_move = direction_update_time;

        bool moved = character.updateDirection(Direction.Right);
        Assert.IsTrue(moved);
        
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        moved = character.updateDirection(Direction.Up);
        Assert.IsTrue(moved);

        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME / 2f);
        moved = character.updateDirection(Direction.Left);
        Assert.IsFalse(moved);
    }

    [UnityTest]
    public IEnumerator test_CheckDirectionOnBorder()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;

        string character_name = "Ball";

        GameObject character_go = GameObject.Find(character_name);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();

        Vector3 old_pos = new Vector3();

        // Check Bottom right place
        character_go.transform.position = new Vector3(character.mMinBorderPos.x, character.mMinBorderPos.y, 0);
        old_pos = character_go.transform.position;

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

        character_go.transform.position = new Vector3(character.mMinBorderPos.x, character.mMaxBorderPos.y, 0);
        old_pos = character_go.transform.position;

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

        character_go.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMaxBorderPos.y, 0);
        old_pos = character_go.transform.position;

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

        character_go.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMinBorderPos.y, 0);
        old_pos = character_go.transform.position;

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
}
