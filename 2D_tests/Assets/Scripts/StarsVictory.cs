using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsVictory : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        if (!Utils.HAS_WIN) return;

        foreach(Transform star_child in transform)
        {
            if (star_child.gameObject.activeSelf) return;
        }

        GameControler.status = GameControler.GameStatus.Win;

        if (GameControler.currentLevel < Levels.levels.Count - 1)
        {
            GameControler.currentLevel++;
            SceneManager.LoadScene("PlayLevel");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }
}
