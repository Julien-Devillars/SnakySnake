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

        CharacterBehavior character = TestUtils.getCharacter();

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
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);
        Assert.AreEqual(character.mMaxBorderPos.y, border.mEndPoint.y);
    }

    [UnityTest]
    public IEnumerator test_TrailShouldBeCreatedWhenLeavingAborderCorner()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        Assert.AreEqual(4, character.mBorders.Count);

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^<>>v");
        Assert.AreEqual(8, character.mBorders.Count);

        TestUtils.setCharacterPositionInAnchor(character, "top-left");
        yield return TestUtils.move(character, "v>^vv<");
        Assert.AreEqual(12, character.mBorders.Count);

        TestUtils.setCharacterPositionInAnchor(character, "top-right");
        yield return TestUtils.move(character, "<v><<^");
        Assert.AreEqual(16, character.mBorders.Count);

        TestUtils.setCharacterPositionInAnchor(character, "bottom-right");
        yield return TestUtils.move(character, "^<v^^>");
        Assert.AreEqual(20, character.mBorders.Count);
    }

    [UnityTest]
    public IEnumerator test_TrailShouldBeCreatedWhenLeavingAborderSide()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        Assert.AreEqual(4, character.mBorders.Count);

        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, ">^<<v");
        Assert.AreEqual(7, character.mBorders.Count);
        Assert.IsTrue(TestUtils.bordersAreValid(character.mBorders));

        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, "^>>v");
        Assert.AreEqual(10, character.mBorders.Count);
        Assert.IsTrue(TestUtils.bordersAreValid(character.mBorders));
    }

    [UnityTest]
    public IEnumerator test_TrailShouldCorrectlyBeCreatedWithSlowSpeed()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        Assert.AreEqual(4, character.mBorders.Count);
        character.mSpeed = 5;

        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, ">^<<v");
        Assert.AreEqual(7, character.mBorders.Count);
        Assert.IsTrue(TestUtils.bordersAreValid(character.mBorders));

        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, "^>>v");
        Assert.IsTrue(TestUtils.bordersAreValid(character.mBorders));
    }

    [UnityTest]
    public IEnumerator test_LoseWhenCharacterTouchsTrail()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        Utils.HAS_LOSE = false;

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "left");
        character.mSpeed = 10;
        yield return TestUtils.move(character, ">^>v<");

        Assert.AreEqual(GameControler.GameStatus.Lose, GameControler.status);
    }

    [UnityTest]
    public IEnumerator test_TrailPointShouldCorrectlyStartAtBorderPosition()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
    
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;


        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        EnemyBehavior enemy = TestUtils.getEnemy(0);
        TestUtils.setEnemyPositionInAnchor(character, enemy, "bottom");

        yield return TestUtils.move(character, ">");
        Vector3 start_point = character.transform.position;
        yield return TestUtils.moveUntilBorder(character, '^', 30);
        Border b1 = TestUtils.getBorder(character.mBorders.Count - 1);
        
        Vector3 end_point = character.transform.position;

        Assert.AreEqual(start_point, b1.mStartPoint);
        Assert.AreEqual(character.mMinBorderPos.y, b1.mStartPoint.y);

        Assert.AreEqual(end_point, b1.mEndPoint);
        Assert.AreEqual(character.mMaxBorderPos.y, b1.mEndPoint.y);

        TestUtils.setCharacterPositionInAnchor(character, "left");
        yield return TestUtils.moveUntilBorder(character, '>', 30);

        Border b2 = TestUtils.getBorder(character.mBorders.Count - 1);
        Assert.AreEqual(b2.mStartPoint.x, b1.mStartPoint.x, "Start point of the border is not at the right position (Should be on the border)");
        Assert.AreEqual(b2.mEndPoint.x, character.mMaxBorderPos.x);

    }
}
