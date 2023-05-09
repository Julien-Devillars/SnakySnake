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

        GameObject enemies_go = GameObject.Find(Utils.ENEMIES_STR);
        GameObject enemy_1 = enemies_go.transform.GetChild(1).gameObject;
        GameObject enemy_2 = enemies_go.transform.GetChild(0).gameObject;
        Vector3 previous_pos_1 = new Vector3();
        Vector3 previous_pos_2 = new Vector3();
        Vector3 first_pos_1 = enemy_1.transform.position;
        Vector3 first_pos_2 = enemy_2.transform.position;
        int cpt = 0;
        while(enemy_1.transform.position.x < first_pos_2.x || enemy_2.transform.position.x > first_pos_1.x)
        {
            Assert.AreNotEqual(cpt++, 10);
            Vector3 pos_1 = enemy_1.transform.position;
            Vector3 pos_2 = enemy_2.transform.position;
            Assert.AreNotEqual(previous_pos_1.x, pos_1.x);
            Assert.AreNotEqual(previous_pos_2.x, pos_2.x);
            previous_pos_1 = pos_1;
            previous_pos_2 = pos_2;
            yield return new WaitForSeconds(1);
        }
    }
}
