using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsVictory : MonoBehaviour
{

    CharacterBehavior character;
    private void Start()
    {
        character = GameObject.Find(Utils.CHARACTER).GetComponent<CharacterBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!Utils.HAS_WIN) return;
        if (character.mTrails.Count > 0) return; // Cannot win if any trail currently

        foreach(Transform star_child in transform)
        {
            if (star_child.gameObject.activeSelf) return;
        }

        GameControler.status = GameControler.GameStatus.Win;

        if (GameControler.currentLevel < Worlds.worlds[GameControler.currentWorld].levels.Count - 1)
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
