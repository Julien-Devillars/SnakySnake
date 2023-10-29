using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TranslationText : MonoBehaviour
{
    static bool mToUpdate = false;
    static SystemLanguage mCurrentLanguage;
    private string mOriginalText = null;
    void Start()
    {
        TextMeshProUGUI text_mesh = GetComponent<TextMeshProUGUI>();
        if (text_mesh != null) 
        {
            mCurrentLanguage = ES3.Load<SystemLanguage>("Language", Application.systemLanguage);
            mOriginalText = text_mesh.text;
            text_mesh.text = Translation.GetTranslation(text_mesh.text, mCurrentLanguage);
        }
    }
    void OnEnable()
    {
        TextMeshProUGUI text_mesh = GetComponent<TextMeshProUGUI>();
        if (text_mesh != null && mOriginalText != null)
        {
            text_mesh.text = Translation.GetTranslation(mOriginalText, mCurrentLanguage);
        }
    }

    private void Update()
    {
        if(mToUpdate)
        {
            TextMeshProUGUI text_mesh = GetComponent<TextMeshProUGUI>();
            Debug.Log($"{text_mesh.text} should change text with {mCurrentLanguage}");
            if (text_mesh != null)
            {
                string tr = Translation.GetTranslation(mOriginalText, mCurrentLanguage);
                Debug.Log($"Found translation {tr}");
                text_mesh.text = tr;
            }
        }
    }

    public static IEnumerator UpdateText(SystemLanguage new_language)
    {
        mCurrentLanguage = new_language;
        mToUpdate = true;
        Debug.Log($"{mToUpdate} update with {mCurrentLanguage}");
        yield return null;
        mToUpdate = false;
        Debug.Log($"{mToUpdate} update with {mCurrentLanguage}");
    }


}
