using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject mMainMenu;
    public GameObject mOptionsMenu;
    public GameObject mCreditsMenu;
    public void Start()
    {
        GameControler.GameVolume = 0.5f;
        Back();
    }
    public void Update()
    {
        GetComponent<AudioSource>().volume = GameControler.GameVolume;
    }

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
    public void Options()
    {
        mMainMenu.SetActive(false);
        mOptionsMenu.SetActive(true);
    }
    public void Back()
    {
        mMainMenu.SetActive(true);
        mOptionsMenu.SetActive(false);
        mCreditsMenu.SetActive(false);
    }
    public void Credits()
    {
        mMainMenu.SetActive(false);
        mCreditsMenu.SetActive(true);
    }
}
