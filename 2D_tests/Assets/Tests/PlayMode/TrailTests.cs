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

    [UnityTest]
    public IEnumerator test_TrailShouldBeCreatedWhenLeavingAborderCorner()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        Assert.AreEqual(character.mBorders.Count, 4);

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^<>>v");
        Assert.AreEqual(character.mBorders.Count, 8);

        TestUtils.setCharacterPositionInAnchor(character, "top-left");
        yield return TestUtils.move(character, "v>^vv<");
        Assert.AreEqual(character.mBorders.Count, 12);

        TestUtils.setCharacterPositionInAnchor(character, "top-right");
        yield return TestUtils.move(character, "<v><<^");
        Assert.AreEqual(character.mBorders.Count, 16);

        TestUtils.setCharacterPositionInAnchor(character, "bottom-right");
        yield return TestUtils.move(character, "^<v^^>");
        Assert.AreEqual(character.mBorders.Count, 20);
    }

    [UnityTest]
    public IEnumerator test_TrailShouldBeCreatedWhenLeavingAborderSide()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        Assert.AreEqual(character.mBorders.Count, 4);

        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, ">^<<v");
        Assert.AreEqual(character.mBorders.Count, 7);
        Assert.IsTrue(TestUtils.bordersAreValid(character.mBorders));

        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, "^>>v");
        Assert.AreEqual(character.mBorders.Count, 9);
        Assert.IsTrue(TestUtils.bordersAreValid(character.mBorders));
        //Assert.AreEqual(character.mBorders.Count, 10);
        //TestUtils.setCharacterPositionInAnchor(character, "bottom");
        //yield return TestUtils.move(character, "^<<v");
        //Assert.AreEqual(character.mBorders.Count, 11);
        //TestUtils.setCharacterPositionInAnchor(character, "bottom");
        //yield return TestUtils.move(character, "^^>v");
        //Assert.AreEqual(character.mBorders.Count, 14);
        //TestUtils.setCharacterPositionInAnchor(character, "bottom");
        //yield return TestUtils.move(character, "^^<v");
        //Assert.AreEqual(character.mBorders.Count, 16);
    }

    [UnityTest]
    public IEnumerator test_TrailShouldCorrectlyBeCreatedWithSlowSpeed()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        Assert.AreEqual(character.mBorders.Count, 4);
        character.mSpeed = 5;

        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, ">^<<v");
        Assert.AreEqual(character.mBorders.Count, 7);
        Assert.IsTrue(TestUtils.bordersAreValid(character.mBorders));

        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, "^>>v");
        Assert.IsTrue(TestUtils.bordersAreValid(character.mBorders));
    }
}
