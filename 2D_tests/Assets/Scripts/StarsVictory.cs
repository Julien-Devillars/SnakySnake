using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsVictory : MonoBehaviour
{

    CharacterBehavior character;
    bool mWin = false;
    private void Start()
    {
        character = GameObject.Find(Utils.CHARACTER).GetComponent<CharacterBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!Utils.HAS_WIN) return;
        if (character.mTrailPoints.Count > 0) return; // Cannot win if any trail currently
        if (character.mCurrentDirection != Direction.direction.None && character.mCurrentDirection != Direction.direction.Stop) return; // Cannot win if any movement

        foreach (Transform star_child in transform)
        {
            if (star_child.gameObject.GetComponent<SpriteRenderer>().enabled) return;
        }
        

        GameControler.status = GameControler.GameStatus.Win;
        if(!mWin)
        {
            mWin = true;
            ES3.Save<bool>($"PlayMode_World{GameControler.currentWorld}_Level{GameControler.currentLevel}_status", true);
            Destroy(gameObject);
        }
    }
}
