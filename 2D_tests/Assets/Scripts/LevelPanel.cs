using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour
{
    public TextMeshProUGUI mWorldName;
    public GameObject mLevels;

    // Start is called before the first frame update
    void Start()
    {
        GameControler.currentWorld = 0;
        GameControler.currentLevel = 0;
        display();
    }


    public void display()
    {
        //Level current_level = Levels.levels[mDisplayIndex];
        //mLevelName.text = current_level.mLevelName;

        mWorldName.text = "The World Name";
        int cpt = 0;
        foreach(Transform level in mLevels.transform)
        {
            cpt++;
            level.GetChild(0).GetComponent<TextMeshProUGUI>().text = cpt.ToString();
            int level_idx = cpt - 1;
            level.GetComponent<Button>().onClick.AddListener(delegate { Play(level_idx); });
        }

    }

    public void onLeft()
    {
        if(GameControler.currentWorld <= 0)
        {
            GameControler.currentWorld = Worlds.worlds.Count - 1;
        }
        else
        {
            GameControler.currentWorld--;
        }
        display();
    }
    public void onRight()
    {
        if (GameControler.currentWorld >= Worlds.worlds.Count - 1)
        {
            GameControler.currentWorld = 0;
        }
        else
        {
            GameControler.currentWorld++;
        }
        display();
    }

    public void Play(int level)
    {
        GameControler.currentLevel = level;
        GameControler.type = GameControler.GameType.Play;
        if(Worlds.worlds.Count == 0)
        {
            Worlds.createWorlds();
        }
        EnemiesGeneratorPlayMode.test_level = false;
        SceneManager.LoadScene("PlayLevel");
    }
}
