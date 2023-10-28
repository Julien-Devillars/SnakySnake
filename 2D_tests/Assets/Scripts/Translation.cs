using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public static class Translation
{

    static Dictionary<string, Dictionary<SystemLanguage, string>> translation = new Dictionary<string, Dictionary<SystemLanguage, string>>()
    {
        { "PLAY", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Play" },
            { SystemLanguage.French, "Jouer" },
            { SystemLanguage.Spanish, "JUGAR" }
        }},
        { "OPTIONS", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "OPTIONS" },
            { SystemLanguage.French, "Options" },
            { SystemLanguage.Spanish, "OPCIONES" }
        }},
        { "CREDITS", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Credits" },
            { SystemLanguage.French, "Crédits" },
            { SystemLanguage.Spanish, "CRÉDITOS" }
        }},
        { "QUIT", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Quit" },
            { SystemLanguage.French, "Quitter" },
            { SystemLanguage.Spanish, "ABANDONAR" }
        }},
        { "MUSIC", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Music" },
            { SystemLanguage.French, "Musique" },
            { SystemLanguage.Spanish, "MÚSICA" }
        }},
        { "LUMINOSITY", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Luminosity" },
            { SystemLanguage.French, "Luminosité" },
            { SystemLanguage.Spanish, "LUMINOSIDAD" }
        }},
        { "CLEAR GAME SAVE", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "CLEAR GAME SAVE" },
            { SystemLanguage.French, "Réinitialiser la sauvegarde" },
            { SystemLanguage.Spanish, "BORRAR JUEGO GUARDADO" }
        }},
        { "BACK", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Back" },
            { SystemLanguage.French, "Retour" },
            { SystemLanguage.Spanish, "ATRÁS" }
        }},
        { "TIME", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Time" },
            { SystemLanguage.French, "Temps" },
            { SystemLanguage.Spanish, "TIEMPO" }
        }},
        { "NAME", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Name" },
            { SystemLanguage.French, "Nom" },
            { SystemLanguage.Spanish, "NOMBRE" }
        }},
        { "RANK", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Rank" },
            { SystemLanguage.French, "Rang" },
            { SystemLanguage.Spanish, "RANGO" }
        }},
        { "NEXT LEVEL", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Next Level" },
            { SystemLanguage.French, "Niveau Suivant" },
            { SystemLanguage.Spanish, "SIGUIENTE NIVEL" }
        }},
        { "REPLAY", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Replay" },
            { SystemLanguage.French, "Rejouer" },
            { SystemLanguage.Spanish, "REPETICIÓN" }
        }},
        { "MENU", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Menu" },
            { SystemLanguage.French, "Menu" },
            { SystemLanguage.Spanish, "MENÚ" }
        }},
        { "RESUME", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Resume" },
            { SystemLanguage.French, "Continuer" },
            { SystemLanguage.Spanish, "REANUDAR" }
        }},
        { "SKIP", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Skip" },
            { SystemLanguage.French, "Passer" },
            { SystemLanguage.Spanish, "SALTAR" }
        }},
        { "ARE YOU SURE ?", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Are you sure ?" },
            { SystemLanguage.French, "Étes-vous sûr ?" },
            { SystemLanguage.Spanish, "ESTÁ SEGURO ?" }
        }},
        { "YES", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Yes" },
            { SystemLanguage.French, "Oui" },
            { SystemLanguage.Spanish, "SÍ" }
        }},
        { "NO", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "No" },
            { SystemLanguage.French, "Non" },
            { SystemLanguage.Spanish, "NO" }
        }},
        { "WORLD", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "World" },
            { SystemLanguage.French, "Monde" },
            { SystemLanguage.Spanish, "MUNDO" }
        }},
        { "LEVEL", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Level" },
            { SystemLanguage.French, "Niveau" },
            { SystemLanguage.Spanish, "NIVEL" }
        }},
        { "MUSIC BY<BR>KARL CASEY @ WHITE BAT AUDIO", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "MUSIC BY<br>KARL CASEY @ WHITE BAT AUDIO" },
            { SystemLanguage.French, "MUSIQUE PAR<br>KARL CASEY @ WHITE BAT AUDIO" }
        }},
        // Add more translations here
    };

    // Function to get a translation for a given key and language
    static public string GetTranslation(string key, SystemLanguage language)
    {
        key = key.ToUpper();
        if (translation.ContainsKey(key))
        {
            Dictionary<SystemLanguage, string> translationSet = translation[key];
            if (translationSet.ContainsKey(language))
            {
                return translationSet[language];
            }
            else
            {
                Debug.Log($"Translation for '{key}' in '{language}' is not available.");
                if (language == SystemLanguage.English) return key;
                return GetTranslation(key, SystemLanguage.English); ;
            }
        }
        else
        {
            return key;
        }
    }
}