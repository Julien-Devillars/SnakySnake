using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BackgroundMergeTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator test_CheckBorderIsCorrectlyMovedWith4Backgrounds()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        
        //  ________
        // |  |     |
        // |  |     |
        // |  |     |
        // |__|_____|

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">",30);
        yield return TestUtils.moveUntilBorder(character, '^', 40);
        yield return TestUtils.moveUntilReachingPoint(character, 'v', TestUtils.getBorder(2), 40, 5);

        //  ________
        // |  |     |
        // |  |     |
        // |  |_____|
        // |________|

        yield return TestUtils.moveUntilBorder(character, '>',40);

        Border border_vertical_1 = TestUtils.getBorder(4);
        Border border_horizontal_1 = TestUtils.getBorder(5);

        yield return null;
        Assert.AreEqual(border_vertical_1.mStartPoint, border_horizontal_1.mStartPoint);

        //  ________
        // |   _____|
        // |  |     |
        // |  |_____|
        // |________|

        TestUtils.setCharacterPositionInAnchor(character, "top-left");
        yield return TestUtils.move(character, "v", 30);
        yield return TestUtils.moveUntilBorder(character, '>', 40);
        
        Border border_horizontal_2 = TestUtils.getBorder(6);

        yield return null;
        Assert.AreEqual(border_vertical_1.mEndPoint, border_horizontal_2.mStartPoint);

        //  ________
        // |   ___  |
        // |  |   | |
        // |  |___| |
        // |________|

        TestUtils.setCharacterPositionInAnchor(character, "top-right");
        yield return TestUtils.move(character, "<", 30);
        yield return TestUtils.moveUntilBorder(character, 'v', 40);

        Border border_vertical_2 = TestUtils.getBorder(7);

        yield return null;
        Assert.AreEqual(border_vertical_2.mStartPoint, border_horizontal_2.mEndPoint);
        Assert.AreEqual(border_vertical_2.mEndPoint, border_horizontal_1.mEndPoint);
    }

    [UnityTest]
    public IEnumerator test_CheckBorderIsCorrectlyRemoveWhenCreatingACorner()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        //  ________
        // |__      |
        // |  |     |
        // |  |___  |
        // |______|_|

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, "^^^>vv>>>vvv", 30);

        //  ________
        // |__      |
        // |  |_    |
        // |    |_  |
        // |______|_|

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, "^^>>vvvvv", 30);

        Border border_vertical_original = TestUtils.getBorder(5);
        Border border_horizontal_original = TestUtils.getBorder(6);

        Border border_vertical_corner = TestUtils.getBorder(9);
        Border border_horizontal_corner = TestUtils.getBorder(8);

        yield return null;
        Assert.AreEqual(border_vertical_original.mEndPoint, border_horizontal_corner.mStartPoint);
        Assert.AreEqual(border_horizontal_original.mStartPoint, border_vertical_corner.mEndPoint);
    }
    [UnityTest]
    public IEnumerator test_CheckBorderIsCorrectlyRemoveWhenCreatingACornerSquareOnTheCorner()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        //  _______
        // |        |
        // |___     |
        // |   |    |
        // |___|____|

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, "^^>>>vv", 30);

        //  ________
        // |   __   |
        // |__|  |  |
        // |    _|  |
        // |___|____|

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, "^^>>^>>vv<", 30);

        Border border_horizontal_original = TestUtils.getBorder(4);
        Border border_vertical_original = TestUtils.getBorder(5);

        Border border_vertical_corner_1 = TestUtils.getBorder(6);
        Border border_horizontal_corner_2 = TestUtils.getBorder(9);

        yield return null;
        Assert.AreEqual(border_horizontal_original.mEndPoint, border_vertical_corner_1.mStartPoint);
        Assert.AreEqual(border_vertical_original.mStartPoint, border_horizontal_corner_2.mEndPoint);
    }
    [UnityTest]
    public IEnumerator test_CheckBorderIsCorrectlyRemoveWhenCreatingASquareInTheBorderContinuity()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        //  _________
        // |         |
        // |  ___    |
        // | |   |   |
        // |_|___|___|

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^^>>>vv", 30);

        //  _________
        // |  _      |
        // | | |_    |
        // | |   |   |
        // |_|___|___|

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^^^>vvv", 30);

        Border border_horizontal_original = TestUtils.getBorder(4);
        Border border_vertical_original = TestUtils.getBorder(5);

        Border border_vertical_corner_1 = TestUtils.getBorder(6);
        Border border_horizontal_corner_2 = TestUtils.getBorder(9);

        yield return null;
        Assert.AreEqual(border_horizontal_original.mEndPoint, border_vertical_corner_1.mStartPoint);
        Assert.AreEqual(border_vertical_original.mStartPoint, border_horizontal_corner_2.mEndPoint);
    }

    [UnityTest]
    public IEnumerator test_CheckBorderIsCorrectlyRemoveWhenCreatingASquareOnASquareSide()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        //  _________
        // |         |
        // |  _____  |
        // | |     | |
        // |_|_____|_|

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^^>>>>vv", 30);

        //  _________
        // |    _    |
        // |  _| |_  |
        // | |     | |
        // |_|_____|_|

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^>^^>vvv", 30);

        Border border_horizontal_original = TestUtils.getBorder(4);
        Border border_vertical_original = TestUtils.getBorder(5);

        Border border_vertical_corner_1 = TestUtils.getBorder(6);
        Border border_horizontal_corner_2 = TestUtils.getBorder(9);

        yield return null;
        Assert.AreEqual(border_horizontal_original.mEndPoint, border_vertical_corner_1.mStartPoint);
        Assert.AreEqual(border_vertical_original.mStartPoint, border_horizontal_corner_2.mEndPoint);
    }
}
