using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TrailTests
{
    [UnityTest]
    public IEnumerator test_TrailShouldFindtheCorrectBorderToAttachedTo()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();

        TestUtils.setCharacterPositionInAnchor(character, "left");
        character.mSpeed = 10;
        yield return TestUtils.move(character, "vv>^^^^^<");

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">");
        character.mSpeed = 2;
        yield return TestUtils.move(character, "<");
        character.mSpeed = 7;
        yield return TestUtils.moveUntilBorder(character, '^');

        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mStartPoint.x, border.mEndPoint.x);
        Assert.AreEqual(border.mEndPoint.y, character.mMaxBorderPos.y);
    }
}
