using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class EnemyTests
{
    [UnityTest]
    public IEnumerator test_Check2EnemiesAreNotColliding()
    {
        SceneManager.LoadScene("TestScene_2Enemies_Facing");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        EnemyBehavior enemy_1 = TestUtils.getEnemy(1);
        EnemyBehavior enemy_2 = TestUtils.getEnemy(0);
        Vector3 previous_pos_1 = new Vector3();
        Vector3 previous_pos_2 = new Vector3();
        Vector3 first_pos_1 = enemy_1.transform.position;
        Vector3 first_pos_2 = enemy_2.transform.position;
        int cpt = 0;
        while(enemy_1.transform.position.x < first_pos_2.x || enemy_2.transform.position.x > first_pos_1.x)
        {
            Assert.AreNotEqual(10, cpt++);
            Vector3 pos_1 = enemy_1.transform.position;
            Vector3 pos_2 = enemy_2.transform.position;
            Assert.AreNotEqual(pos_1.x, previous_pos_1.x);
            Assert.AreNotEqual(pos_2.x, previous_pos_2.x);
            previous_pos_1 = pos_1;
            previous_pos_2 = pos_2;
            yield return new WaitForSeconds(1);
        }
    }

    [UnityTest]
    public IEnumerator test_LoseWhenEnemyTouchsTrail()
    {
        SceneManager.LoadScene("TestScene_1Enemy_Static");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        Utils.HAS_LOSE = false;
        
        EnemyBehavior enemy = TestUtils.getEnemy(0);
        enemy.setDirection(new Vector2(-7, 0));

        GameObject character_go = GameObject.Find(Utils.CHARACTER);
        CharacterBehavior character = character_go.GetComponent<CharacterBehavior>();
        TestUtils.setCharacterPositionInAnchor(character, "left");
        character.mSpeed = 10;
        yield return TestUtils.move(character, "^>vv");
        yield return TestUtils.moveUntilBorder(character, '>');

        Assert.AreEqual(GameControler.GameStatus.Lose, GameControler.status);
    }
}
