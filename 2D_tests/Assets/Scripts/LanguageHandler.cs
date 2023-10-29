using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageHandler : MonoBehaviour
{
    public TextMeshProUGUI mLanguageText;
    List<SystemLanguage> mLanguages = new List<SystemLanguage>()
    {
        SystemLanguage.English,
        SystemLanguage.French,
        SystemLanguage.Spanish,
        SystemLanguage.German,
        SystemLanguage.Italian,
        SystemLanguage.Russian,
    };
    public int mLanguageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        SystemLanguage user_language = ES3.Load<SystemLanguage>("Language", Application.systemLanguage);    
        if(!mLanguages.Contains(user_language))
        {
            ES3.Save<SystemLanguage>("Language", SystemLanguage.English);
        }
        else
        {
            mLanguageIndex = mLanguages.IndexOf(user_language);
            mLanguageText.text = Translation.lang[user_language];
            ES3.Save<SystemLanguage>("Language", user_language);
        }
    }

    public void updateLanguage()
    {
        if(mLanguageIndex  + 1 < mLanguages.Count)
        {
            mLanguageIndex++;
        }
        else
        {
            mLanguageIndex = 0;
        }
        SystemLanguage language = mLanguages[mLanguageIndex];
        mLanguageText.text = Translation.lang[language];
        ES3.Save<SystemLanguage>("Language", language);
        Debug.Log($"Send {language}");
        StartCoroutine(TranslationText.UpdateText(language));
        LevelPanel.mForceDisplay = true;
    }
}
