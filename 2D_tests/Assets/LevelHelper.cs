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
        if(GameControler.currentWorld == 0 && GameControler.currentLevel == 0)
        {
            mTextMeshPro.text = "Move with ZQSD and catch the stars to cleared the level.";
        }
        else if (GameControler.currentWorld == 0 && GameControler.currentLevel == 1)
        {
            mTextMeshPro.text = "You lose when the enemy touch your line.";
        }
        else if (GameControler.currentWorld == 0 && GameControler.currentLevel == 2)
        {
            mTextMeshPro.text = "You can stop by going to the opposite direction on border.";
        }
        else
        {
            gameObject.SetActive(false);
            GetComponent<Image>().enabled = false;
        }
    }
    void Update()
    {

        if (GameControler.status == GameControler.GameStatus.Lose || GameControler.status == GameControler.GameStatus.Win)
        {
            gameObject.SetActive(false);
            GetComponent<Image>().enabled = false;
        }
    }

}
