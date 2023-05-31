using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    void Play()
    {
        SceneManager.LoadSceneAsync("Level_1");
    }
    void Quit()
    {
        Application.Quit();
    }
}
