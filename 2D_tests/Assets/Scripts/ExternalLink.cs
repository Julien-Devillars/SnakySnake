using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExternalLink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameControler.isDemo)
        {
            Button button = GetComponent<Button>();
            if (button == null) return;
            button.enabled = true;
        }
    }

    public void linkToSteamPage()
    {
        Application.OpenURL("https://store.steampowered.com/app/2573150/Neon_Dash_Tales/");
    }
}
