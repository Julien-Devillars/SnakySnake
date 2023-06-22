using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BackgroundTests
{

    [UnityTest]
    public IEnumerator test_CheckBackgroundAtStartUp()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        TestUtils.checkNumberOfBackgrounds(1);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn2_Horizontal()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 30;
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);

        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        TestUtils.checkNumberOfBackgrounds(2);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn2_Vertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 30;

        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);

        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        TestUtils.checkNumberOfBackgrounds(2);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn4_VerticalHorizontal()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 30;

        // First split -> Vertical
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");

        // Second split -> Horizontal
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        TestUtils.checkNumberOfBackgrounds(3);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn4_HorizontalVertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 30;

        // First split -> Horizontal
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");

        // Second split -> Vertical
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        TestUtils.checkNumberOfBackgrounds(3);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn8_Horizontal_Horizontal_Vertical_Vertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 30;

        // First split -> Horizontal 1
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        TestUtils.checkNumberOfBackgrounds(2);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "top-left");

        // Second split -> Horizontal 2
        character.updateDirection(Direction.Down);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        TestUtils.checkNumberOfBackgrounds(3);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");

        // Third split -> Vertical 1
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        TestUtils.checkNumberOfBackgrounds(4);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-right");

        // Fourth split -> Vertical 2
        character.updateDirection(Direction.Left);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        TestUtils.checkNumberOfBackgrounds(5);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_BackgroundSplitIn8_Horizontal_Vertical_Horizontal_Vertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 30;

        // First split -> Horizontal 1
        character.updateDirection(Direction.Up);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        TestUtils.setCharacterPositionInAnchor(character, "top-left");

        TestUtils.checkNumberOfBackgrounds(2);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");

        // Second split -> Vertical 1
        character.updateDirection(Direction.Right);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        TestUtils.checkNumberOfBackgrounds(3);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "top-left");

        // Third split -> Horizontal 2
        character.updateDirection(Direction.Down);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Right);
        yield return new WaitUntil(() => character.transform.position.x == character.mMaxBorderPos.x);

        TestUtils.checkNumberOfBackgrounds(4);
        TestUtils.checkBackgroundAreEquals();

        character.transform.position = new Vector3(character.mMaxBorderPos.x, character.mMinBorderPos.y, 0);
        TestUtils.setCharacterPositionInAnchor(character, "bottom-right");

        // Fourth split -> Vertical 2
        character.updateDirection(Direction.Left);
        yield return new WaitForSeconds(Utils.DIRECTION_UPDATE_TIME);
        // Up until reaching the top
        character.updateDirection(Direction.Up);
        yield return new WaitUntil(() => character.transform.position.y == character.mMaxBorderPos.y);

        TestUtils.checkNumberOfBackgrounds(5);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_Background_1Turn_Corners()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");

        // Corner bottom-left
        yield return TestUtils.move(character, ">^<", 30);
        yield return new WaitForSeconds(0.1f);
        TestUtils.checkNumberOfBackgrounds(3);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "top-left");

        // Corner top-left
        yield return TestUtils.move(character, ">v<");
        yield return new WaitForSeconds(0.1f);
        TestUtils.checkNumberOfBackgrounds(5);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "top-right");

        // Corner top-right
        yield return TestUtils.move(character, "<v>");
        yield return new WaitForSeconds(0.1f);
        TestUtils.checkNumberOfBackgrounds(7);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-right");

        // Corner top-right
        yield return TestUtils.move(character, "<^>");
        yield return new WaitForSeconds(0.1f);
        TestUtils.checkNumberOfBackgrounds(9);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_Background_2Turns_Sides()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom");

        yield return TestUtils.move(character, ">^<<v");
        TestUtils.checkNumberOfBackgrounds(4);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "left");

        yield return TestUtils.move(character, "v>^^<");
        TestUtils.checkNumberOfBackgrounds(7);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "top");

        yield return TestUtils.move(character, "<v>>^");
        TestUtils.checkNumberOfBackgrounds(10);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "right");

        yield return TestUtils.move(character, "^<vv>");
        TestUtils.checkNumberOfBackgrounds(13);
        TestUtils.checkBackgroundAreEquals();
    }
    [UnityTest]
    public IEnumerator test_Background_SideSquareInsideSideSquare()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        Enemy enemy = TestUtils.getEnemy(0);
        TestUtils.setEnemyPositionInAnchor(character, enemy, "top");
        TestUtils.setCharacterPositionInAnchor(character, "bottom");

        Utils.HAS_LOSE = false;

        yield return TestUtils.move(character, ">^^<<vv>");
        TestUtils.checkNumberOfBackgrounds(4);
        TestUtils.checkBackgroundAreEquals();
        yield return TestUtils.move(character, "^>>^^<<<<vv>>");
        TestUtils.checkNumberOfBackgrounds(11);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_CheckConnectionBetweenBackgroundIsCorrectlyWorkingWithSmallBackground()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 15;
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">");
        Border top_border = TestUtils.getBorder(0);
        Border right_border = TestUtils.getBorder(1);
        Border bottom_border = TestUtils.getBorder(2);
        yield return TestUtils.moveUntilReachingPoint(character, '^', top_border, 15, 3);
        yield return TestUtils.moveUntilReachingPoint(character, '>', right_border, 15, 3);
        yield return TestUtils.moveUntilReachingPoint(character, 'v', bottom_border, 15, 3);
        Trail trail_0 = TestUtils.getTrail(0);
        yield return TestUtils.moveUntilReachingPoint(character, '<', trail_0, 15, 3);
        yield return TestUtils.moveUntilBorder(character, 'v');

        TestUtils.checkNumberOfBackgrounds(6);
        TestUtils.checkBackgroundsHasEnnemy(new List<bool>{ false, false, false, true, true, false});
    }

    [UnityTest]
    public IEnumerator test_EnemyPositionOnBackgroundSplit()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "left");

        Enemy enemy = TestUtils.getEnemy(0);
        TestUtils.setEnemyPositionInAnchor(character, enemy, "right");

        yield return TestUtils.move(character, ">", 15);
        yield return TestUtils.moveUntilBorder(character, 'v');

        Assert.IsTrue(TestUtils.hasBackgroundWithEnemy(character));
    }

    [UnityTest]
    public IEnumerator test_CrossingBorderShouldCreateBackground()
    {
        SceneManager.LoadScene("TestScene_2Enemies_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom");

        Enemy enemy_1 = TestUtils.getEnemy(0);
        TestUtils.setEnemyPositionInAnchor(character, enemy_1, "right");
        Enemy enemy_2 = TestUtils.getEnemy(1);
        TestUtils.setEnemyPositionInAnchor(character, enemy_2, "left");

        yield return TestUtils.moveUntilBorder(character, '^', 35);
        yield return TestUtils.move(character, ">v<<v");

        Assert.AreEqual(4, character.mBackgrounds.Count);
        Assert.AreEqual(7, character.mBorders.Count);

        yield return TestUtils.move(character, ">>>v");

        Assert.AreEqual(7, character.mBackgrounds.Count);
        Assert.AreEqual(10, character.mBorders.Count);
    }

}
