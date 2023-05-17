using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BorderTests
{
    [UnityTest]
    public IEnumerator test_CharacterShoulMovedToACreatedBorderIfLeavingABorder()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 10;
        yield return TestUtils.move(character, ">");
        Vector3 fixed_pos = character.transform.position;
        character.mSpeed = 30;
        yield return TestUtils.moveUntilBorder(character, '^');

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");

        character.mSpeed = 10;
        // On the right
        yield return TestUtils.move(character, ">");
        character.mSpeed = 3;
        // A bit on the right
        yield return TestUtils.move(character, ">");
        character.mSpeed = 30;
        // Top
        yield return TestUtils.moveUntilBorder(character, '^');

        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);

        Assert.AreEqual(border.mEndPoint.x, fixed_pos.x);
        Assert.AreEqual(border.mStartPoint.x, fixed_pos.x);
    }
    [UnityTest]
    public IEnumerator test_CharacterShoulMovedToAnOriginalBorderIfLeavingABorder()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 3;
        yield return TestUtils.move(character, ">");
        character.mSpeed = 30;
        yield return TestUtils.moveUntilBorder(character, '^');

        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);

        Assert.AreEqual(border.mEndPoint.x, character.mMinBorderPos.x);
        Assert.AreEqual(border.mStartPoint.x, character.mMinBorderPos.x);
    }
    [UnityTest]
    public IEnumerator test_CharacterShoulNotMovedToTheBorderIfHaveATrail()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();

        TestUtils.setCharacterPositionInAnchor(character, "left");
        character.mSpeed = 10;
        yield return TestUtils.move(character, "vv>^^^^^<");

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">");
        character.mSpeed = 2;
        yield return TestUtils.move(character, ">");
        character.mSpeed = 7;
        yield return TestUtils.moveUntilBorder(character, '^');

        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);
        Assert.AreEqual(character.mMaxBorderPos.y, border.mEndPoint.y);
    }

    [UnityTest]
    public IEnumerator test_CharacterShoulNotMovedToTheBorderOnBackground()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        character.mSpeed = 10;
        yield return TestUtils.move(character, ">");
        yield return TestUtils.moveUntilBorder(character, '^');

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">");
        character.mSpeed = 2;
        yield return TestUtils.move(character, "<");
        character.mSpeed = 7;
        yield return TestUtils.moveUntilBorder(character, '^');

        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);
        Assert.AreEqual(character.mMaxBorderPos.y, border.mEndPoint.y);
    }

    [UnityTest]
    public IEnumerator test_CharacterShouldNotBeSetOnSameDirectionLineWhenDeletingTrail()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();

        TestUtils.setCharacterPositionInAnchor(character, "left");
        yield return TestUtils.move(character, ">", 15);
        yield return TestUtils.moveUntilBorder(character, '^', 35);

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">", 15);
        yield return TestUtils.move(character, ">", 3);
        yield return TestUtils.moveUntilBorder(character, '^', 3);

        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);
    }
}
