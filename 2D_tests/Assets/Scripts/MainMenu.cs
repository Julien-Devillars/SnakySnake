using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Click Button");
        SceneManager.LoadScene("Level_1");
        Debug.Log("After Async");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
