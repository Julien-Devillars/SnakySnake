using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelHelper : MonoBehaviour
{
    public TextMeshProUGUI mTextMeshPro;
    
    void Start()
    {
        string helper_text = Worlds.getLevel(GameControler.currentWorld, GameControler.currentLevel).mLevelHelper;
        if(helper_text == "")
        {
            gameObject.SetActive(false);
            GetComponent<Image>().enabled = false;
        }
        else
        {
            mTextMeshPro.text = helper_text;
        }
    }
    void Update()
    {

        if (GameControler.status == GameControler.GameStatus.Lose 
            || GameControler.status == GameControler.GameStatus.Win
            || Utils.GAME_STOPPED)
        {
            gameObject.SetActive(false);
            GetComponent<Image>().enabled = false;
        }
    }

}
