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
        int world = GameControler.currentWorld + 1;
        int level = GameControler.currentLevel + 1;
        if (world == 1 && level == 1)
        {
            mTextMeshPro.text = "Move with ZQSD.";
        }
        else if(world == 1 && level == 2)
        {
            mTextMeshPro.text = "Catch all the stars to end the level.";
        }
        else if (world == 1 && level == 3)
        {
            mTextMeshPro.text = "You lose if someone hit your trail !";
        }
        else if (world == 1 && level == 4)
        {
            mTextMeshPro.text = "Your trail creates border that will you help you out !";
        }
        else if (world == 1 && level == 5)
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

        if (GameControler.status == GameControler.GameStatus.Lose 
            || GameControler.status == GameControler.GameStatus.Win
            || Utils.GAME_STOPPED)
        {
            gameObject.SetActive(false);
            GetComponent<Image>().enabled = false;
        }
    }

}
