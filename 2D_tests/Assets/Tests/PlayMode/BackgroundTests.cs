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
        character.mSpeed = 30;

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");

        // Corner bottom-left
        yield return TestUtils.move(character, ">^<");

        TestUtils.checkNumberOfBackgrounds(3);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "top-left");

        // Corner top-left
        yield return TestUtils.move(character, ">v<");

        TestUtils.checkNumberOfBackgrounds(5);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "top-right");

        // Corner top-right
        yield return TestUtils.move(character, "<v>");

        TestUtils.checkNumberOfBackgrounds(7);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-right");

        // Corner top-right
        yield return TestUtils.move(character, "<^>");

        TestUtils.checkNumberOfBackgrounds(9);
        TestUtils.checkBackgroundAreEquals();
    }
    [UnityTest]
    public IEnumerator test_Background_MultiVertical()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 10;

        GameObject enemies_go = GameObject.Find(Utils.ENEMIES_STR);
        Transform enemy = enemies_go.transform.GetChild(0);
        float epsilon = Utils.EPSILON();
        enemy.position = new Vector3(character.mMaxBorderPos.x - epsilon * 2f, 0f, 0f);

        Utils.HAS_LOSE = false;
        Utils.HAS_WIN = false;

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
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
            TestUtils.checkNumberOfBackgrounds(number_expected_bg);
            TestUtils.checkBackgroundAreEquals();
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

        CharacterBehavior character = TestUtils.getCharacter();
        character.mSpeed = 10;

        GameObject enemies_go = GameObject.Find(Utils.ENEMIES_STR);
        Transform enemy = enemies_go.transform.GetChild(0);
        float epsilon = Utils.EPSILON();
        enemy.position = new Vector3(0f, character.mMaxBorderPos.y - epsilon * 2f, 0f);

        Utils.HAS_LOSE = false;
        Utils.HAS_WIN = false;

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
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
            TestUtils.checkNumberOfBackgrounds(number_expected_bg);
            TestUtils.checkBackgroundAreEquals();
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
        EnemyBehavior enemy = TestUtils.getEnemy(0);
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
    public IEnumerator test_Background_SideSquare_HitOnUpperSide()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        EnemyBehavior enemy = TestUtils.getEnemy(0);
        TestUtils.setEnemyPositionInAnchor(character, enemy, "top");
        TestUtils.setCharacterPositionInAnchor(character, "bottom");

        Utils.HAS_LOSE = false;

        yield return TestUtils.move(character, ">^^<<vv>");
        TestUtils.checkNumberOfBackgrounds(4);
        TestUtils.checkBackgroundAreEquals();
        yield return TestUtils.move(character, ">>^^<<");
        TestUtils.checkNumberOfBackgrounds(6);
        TestUtils.checkBackgroundAreEquals();
    }

    [UnityTest]
    public IEnumerator test_Background_CornerLoop()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        EnemyBehavior enemy = TestUtils.getEnemy(0);
        TestUtils.setEnemyPositionInAnchor(character, enemy, "top-right");

        Utils.HAS_LOSE = false;
        character.mSpeed = 15;

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^<");
        TestUtils.checkNumberOfBackgrounds(3);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">>^<^<");
        TestUtils.checkNumberOfBackgrounds(8);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">>>^<^<^<");
        TestUtils.checkNumberOfBackgrounds(14);
        TestUtils.checkBackgroundAreEquals();

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">>>>^<^<^<^<");
        TestUtils.checkNumberOfBackgrounds(21);
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
    public IEnumerator test_CheckConnectionBetweenBackgroundWithVerticalSnake()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        EnemyBehavior enemy = TestUtils.getEnemy(0);
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        TestUtils.setEnemyPositionInAnchor(character, enemy, "right");
        float epsilon = Utils.EPSILON();

        Border top_border = TestUtils.getBorder(0);
        Border bottom_border = TestUtils.getBorder(2);

        bool is_going_top = true;
        while (character.transform.position.x < enemy.transform.position.x - epsilon * 8f)
        {
            character.mSpeed = 10;
            yield return TestUtils.move(character, ">");

            character.mSpeed = 30;
            if (is_going_top)
            {
                yield return TestUtils.moveUntilReachingPoint(character, '^', top_border);
            }
            else
            {
                yield return TestUtils.moveUntilReachingPoint(character, 'v', bottom_border);
            }
            is_going_top = !is_going_top;
        }

        if(is_going_top) // Reversed on the last execution of the while
        {
            yield return TestUtils.moveUntilBorder(character, 'v');
        }
        else
        {
            yield return TestUtils.moveUntilBorder(character, '^');
        }

        Assert.IsTrue(TestUtils.hasBackgroundWithoutEnemy(character));
    }

    [UnityTest]
    public IEnumerator test_CheckConnectionBetweenBackgroundWithCornerSnake()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        CharacterBehavior character = TestUtils.getCharacter();
        EnemyBehavior enemy = TestUtils.getEnemy(0);
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        TestUtils.setEnemyPositionInAnchor(character, enemy, "top-right");
        float epsilon = Utils.EPSILON();

        Border top_border = TestUtils.getBorder(0);
        Border right_border = TestUtils.getBorder(1);
        Border bottom_border = TestUtils.getBorder(2);

        yield return TestUtils.move(character, ">", 10);
        yield return TestUtils.moveUntilReachingPoint(character, '^', top_border, 30);
        yield return TestUtils.move(character, ">", 10);

        yield return TestUtils.moveUntilReachingPoint(character, 'v', bottom_border, 30);
        yield return TestUtils.moveUntilReachingPoint(character, '>', right_border, 30);
        yield return TestUtils.move(character, "^", 10);

        Trail t2 = TestUtils.getTrail(2);
        yield return TestUtils.moveUntilReachingPoint(character, '<', t2, 30);
        yield return TestUtils.moveUntilReachingPoint(character, '^', top_border, 30);
        yield return TestUtils.move(character, ">", 10);

        Trail t5 = TestUtils.getTrail(5);
        yield return TestUtils.moveUntilReachingPoint(character, 'v', t5, 30);
        yield return TestUtils.moveUntilReachingPoint(character, '>', right_border, 30);
        yield return TestUtils.move(character, "^", 10);

        Trail t8 = TestUtils.getTrail(8);
        yield return TestUtils.moveUntilReachingPoint(character, '<', t8, 30);
        yield return TestUtils.moveUntilReachingPoint(character, '^', top_border, 30);
        yield return TestUtils.move(character, ">", 10);

        Trail t11 = TestUtils.getTrail(11);
        yield return TestUtils.moveUntilReachingPoint(character, 'v', t11, 30);

        yield return TestUtils.moveUntilBorder(character, '>', 30);

        Assert.IsTrue(TestUtils.hasBackgroundWithoutEnemy(character));
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

        EnemyBehavior enemy = TestUtils.getEnemy(0);
        TestUtils.setEnemyPositionInAnchor(character, enemy, "right");

        yield return TestUtils.move(character, ">", 15);
        yield return TestUtils.moveUntilBorder(character, 'v');

        Assert.IsTrue(TestUtils.hasBackgroundWithEnemy(character));
    }

}
