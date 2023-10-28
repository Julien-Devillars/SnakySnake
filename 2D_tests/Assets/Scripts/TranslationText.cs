using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TranslationText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text_mesh = GetComponent<TextMeshProUGUI>();
        if (text_mesh != null) 
        {
            text_mesh.text = Translation.GetTranslation(text_mesh.text, Application.systemLanguage);
        }
    }

}
