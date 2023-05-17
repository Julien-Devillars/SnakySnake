using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TrailTests
{
    [UnityTest]
    public IEnumerator test_TrailShouldFindtheCorrectBorderToAttachedTo_LeftSide()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, "^>", 15);
        Vector3 pos = character.transform.position;
        yield return TestUtils.move(character, "^^<", 15);

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">", 15);
        yield return TestUtils.move(character, "<", 3);
        yield return TestUtils.move(character, "^^<", 15);

        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);
        Assert.AreEqual(pos.y, border.mEndPoint.y);
    }
    [UnityTest]
    public IEnumerator test_TrailShouldFindtheCorrectBorderToAttachedTo_RightSide()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;
    
        CharacterBehavior character = TestUtils.getCharacter();
    
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, "^>", 15);
        Vector3 pos = character.transform.position;
        yield return TestUtils.move(character, "^^<", 15);
    
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">", 15);
        yield return TestUtils.move(character, ">", 3);
        yield return TestUtils.moveUntilBorder(character, '^', 15);
    
        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);
        Assert.AreEqual(character.mMaxBorderPos.y, border.mEndPoint.y);
    }

    [UnityTest]
    public IEnumerator test_TrailShouldBeCreatedWhenLeavingAborderCorner()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
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
    public IEnumerator test_LoseWhenCharacterTouchsTrail()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;

        Utils.HAS_LOSE = false;

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "left");
        yield return TestUtils.move(character, ">^>v<", 10);

        Assert.AreEqual(GameControler.GameStatus.Lose, GameControler.status);
    }

    [UnityTest]
    public IEnumerator test_TrailPointShouldCorrectlyStartAtBorderPosition()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
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

    [UnityTest]
    public IEnumerator test_TrailIsNotGlitchingOnCloseBorderWhenMovingToIt()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">");
        Vector3 pos = character.transform.position;
        yield return TestUtils.moveUntilBorder(character, '^');

        TestUtils.setCharacterPositionInAnchor(character, "left");
        yield return TestUtils.move(character, "v>>^<<", 15);

        TestUtils.setCharacterPositionInAnchor(character, "left");
        yield return TestUtils.move(character, "v", 17);
        yield return TestUtils.moveUntilBorder(character, '>', 30);

        Assert.AreEqual(character.mBorders.Count, 9);
        Border border = TestUtils.getBorder(character.mBorders.Count - 1);
        Assert.AreEqual(border.mEndPoint.y, border.mStartPoint.y);
    }

    [UnityTest]
    public IEnumerator test_TrailIsCorrectlyCreatedAfterAutomaticallySetOnBorder()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^<");

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">v");
        yield return TestUtils.moveUntilBorder(character, '^', 4);

        Border border = character.mBorders[character.mBorders.Count - 1];
        Assert.AreEqual(border.mEndPoint.x, border.mStartPoint.x);
        Assert.AreEqual(character.mMaxBorderPos.y, border.mEndPoint.y);
    }
}
