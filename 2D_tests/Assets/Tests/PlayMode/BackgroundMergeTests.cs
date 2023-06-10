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
    public IEnumerator test_CheckBorderIsCorrectlyRemoveWhenCreatingASquareInTheBorderContinuity_StartPointChanged()
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

        Border border_horizontal_original = TestUtils.getBorder(5);
        Border border_vertical_corner_2 = TestUtils.getBorder(9);

        yield return null;
        Assert.AreEqual(border_horizontal_original.mStartPoint, border_vertical_corner_2.mEndPoint);
    }
    [UnityTest]
    public IEnumerator test_CheckBorderIsCorrectlyRemoveWhenCreatingASquareInTheBorderContinuity_EndPointChanged()
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
        // |    _    |
        // |  _| |   |
        // | |   |   |
        // |_|___|___|

        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">>>>^^^<vvv", 30);

        Border border_horizontal_original = TestUtils.getBorder(5);
        Border border_vertical_corner_2 = TestUtils.getBorder(9);

        yield return null;
        Assert.AreEqual(border_horizontal_original.mEndPoint, border_vertical_corner_2.mEndPoint);
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
        yield return TestUtils.move(character, ">>^^^>vvv", 30);

        Border border_vertical_original_1 = TestUtils.getBorder(4);
        Border border_vertical_original_2 = TestUtils.getBorder(5);

        Border border_vertical_corner_1 = TestUtils.getBorder(6);
        Border border_vertical_corner_2 = TestUtils.getBorder(8);

        Border new_border_after_split_1 = TestUtils.getBorder(9);
        Border new_border_after_split_2 = TestUtils.getBorder(10);

        yield return null;

        Assert.AreEqual(border_vertical_original_1.mEndPoint, new_border_after_split_1.mStartPoint);
        Assert.AreEqual(border_vertical_corner_1.mStartPoint, new_border_after_split_1.mEndPoint);

        Assert.AreEqual(border_vertical_original_2.mStartPoint, new_border_after_split_2.mEndPoint);
        Assert.AreEqual(border_vertical_corner_2.mEndPoint, new_border_after_split_2.mStartPoint);
    }
    [UnityTest]
    public IEnumerator test_CheckBorderIsNotMissingAfterCrossing()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        //  _________
        // |  ____   |
        // | |    |  |
        // | |  __|  |
        // |_|_|_____|

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">^^^>>>>vv<<v", 30);

        //  _________
        // |  ____  |
        // | |    | |
        // | |   _| |
        // |_|__|___|


        yield return TestUtils.move(character, ">^^<vv", 30);

        yield return null;

        Border border_vertical_original = TestUtils.getBorder(6);
        Border border_horizontal_original = TestUtils.getBorder(7);
        Border border_vertical_split = TestUtils.getBorder(8);


        Assert.AreEqual(border_vertical_original.mEndPoint, border_horizontal_original.mStartPoint);
        Assert.AreEqual(border_horizontal_original.mEndPoint, border_vertical_split.mEndPoint);
    }
    [UnityTest]
    public IEnumerator test_CheckBorderIsNotMissingAfterCrossingFromAbove()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        //  _________
        // |         |
        // |   __    |
        // |__|  |   |
        // |_____|___|

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "bottom-left");
        yield return TestUtils.move(character, ">>>>^^<<v<<", 30);

        //  _________
        // |  ___     |
        // | |  _|_   |
        // |_|_|  |   |
        // |______|___|


        yield return TestUtils.move(character, "v>^^^>>", 30);
        yield return TestUtils.move(character, ">", 15);
        yield return TestUtils.move(character, "vvv", 30);

        yield return null;

        yield return new WaitForSeconds(5);
        Border border_vertical_original = TestUtils.getBorder(4);
        Border border_horitontal_original = TestUtils.getBorder(5);
        Border border_vertical_split = TestUtils.getBorder(10);

        Assert.AreEqual(border_horitontal_original.mStartPoint, border_vertical_original.mEndPoint);
        Assert.AreEqual(border_horitontal_original.mEndPoint, border_vertical_split.mEndPoint);
    }
    [UnityTest]
    public IEnumerator test_CheckBorderOnSideIsNotDEletingThe2Borders()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        //  _________
        // |__       |
        // |  |      |
        // |__|      |
        // |_________|

        CharacterBehavior character = TestUtils.getCharacter();
        TestUtils.setCharacterPositionInAnchor(character, "left");
        yield return TestUtils.move(character, "^^>vvvv<", 30);

        //  _________
        // |         |
        // |  _____  |
        // | |     | |
        // |_|_____|_|

        TestUtils.setCharacterPositionInAnchor(character, "left");
        yield return TestUtils.move(character, "^>>vv<<", 30);

        //yield return new WaitForSeconds(5);
        Border border_horitontal_original_side_1 = TestUtils.getBorder(6);
        Border border_horitontal_original_side_2 = TestUtils.getBorder(8);
        Border border_vertical_vertical_1 = TestUtils.getBorder(9);
        Border border_vertical_vertical_2 = TestUtils.getBorder(10);
        
        //Assert.AreEqual(border_horitontal_original.mStartPoint, border_vertical_original.mEndPoint);
        //Assert.AreEqual(border_horitontal_original.mEndPoint, border_vertical_split.mEndPoint);
    }

    [UnityTest]
    public IEnumerator test_showTest()
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
        TestUtils.setCharacterPositionInAnchor(character, "bottom");
        yield return TestUtils.move(character, "<<^^>>>>vv", 30);
    }
}
