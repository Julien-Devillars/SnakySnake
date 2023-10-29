using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public static class Translation
{

    static public Dictionary<SystemLanguage, string> lang = new Dictionary<SystemLanguage, string>()
    {
        { SystemLanguage.English, "English" },
        { SystemLanguage.French, "Français" },
        { SystemLanguage.Spanish, "Español" },
        { SystemLanguage.German, "Deutsch" },
        { SystemLanguage.Italian, "Italiano" },
        { SystemLanguage.Russian, "Русский" },
    };

    static Dictionary<string, Dictionary<SystemLanguage, string>> translation = new Dictionary<string, Dictionary<SystemLanguage, string>>()
    {
        { "PLAY", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Play" },
            { SystemLanguage.French, "Jouer" },
            { SystemLanguage.Spanish, "JUGAR" },
            { SystemLanguage.German, "Spielen" },
            { SystemLanguage.Italian, "Gioca" },
            { SystemLanguage.Russian, "Играть" },
        }},
        { "OPTIONS", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "OPTIONS" },
            { SystemLanguage.French, "Options" },
            { SystemLanguage.Spanish, "OPCIONES" },
            { SystemLanguage.German, "Optionen" },
            { SystemLanguage.Italian, "Opzioni" },
            { SystemLanguage.Russian, "Настройки" },
        }},
        { "CREDITS", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Credits" },
            { SystemLanguage.French, "Crédits" },
            { SystemLanguage.Spanish, "CRÉDITOS" },
            { SystemLanguage.German, "Kredite" },
            { SystemLanguage.Italian, "Crediti" },
            { SystemLanguage.Russian, "Заслуги" },
        }},
        { "QUIT", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Quit" },
            { SystemLanguage.French, "Quitter" },
            { SystemLanguage.Spanish, "ABANDONAR" },
            { SystemLanguage.German, "Beenden" },
            { SystemLanguage.Italian, "Esci" },
            { SystemLanguage.Russian, "Выйти" },
        }},
        { "MUSIC", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Music" },
            { SystemLanguage.French, "Musique" },
            { SystemLanguage.Spanish, "MÚSICA" },
            { SystemLanguage.German, "Musik" },
            { SystemLanguage.Italian, "Musica" },
            { SystemLanguage.Russian, "Музыка" },
        }},
        { "LUMINOSITY", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Luminosity" },
            { SystemLanguage.French, "Luminosité" },
            { SystemLanguage.Spanish, "LUMINOSIDAD" },
            { SystemLanguage.German, "Helligkeit" },
            { SystemLanguage.Italian, "Luminosità" },
            { SystemLanguage.Russian, "Яркость" },
        }},
        { "CLEAR GAME SAVE", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "CLEAR GAME SAVE" },
            { SystemLanguage.French, "Réinitialiser la sauvegarde" },
            { SystemLanguage.Spanish, "BORRAR JUEGO GUARDADO" },
            { SystemLanguage.German, "Spielstand löschen" },
            { SystemLanguage.Italian, "Cancella il salvataggio del gioco" },
            { SystemLanguage.Russian, "Очистить сохранение игры" },
        }},
        { "BACK", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Back" },
            { SystemLanguage.French, "Retour" },
            { SystemLanguage.Spanish, "ATRÁS" },
            { SystemLanguage.German, "Zurück" },
            { SystemLanguage.Italian, "Indietro" },
            { SystemLanguage.Russian, "Назад" },
        }},
        { "TIME", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Time" },
            { SystemLanguage.French, "Temps" },
            { SystemLanguage.Spanish, "TIEMPO" },
            { SystemLanguage.German, "Zeit" },
            { SystemLanguage.Italian, "Tempo" },
            { SystemLanguage.Russian, "Время" },
        }},
        { "NAME", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Name" },
            { SystemLanguage.French, "Nom" },
            { SystemLanguage.Spanish, "NOMBRE" },
            { SystemLanguage.German, "Name" },
            { SystemLanguage.Italian, "Nome" },
            { SystemLanguage.Russian, "Имя" },
        }},
        { "RANK", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Rank" },
            { SystemLanguage.French, "Rang" },
            { SystemLanguage.Spanish, "RANGO" },
            { SystemLanguage.German, "Rang" },
            { SystemLanguage.Italian, "Grado" },
            { SystemLanguage.Russian, "Ранг" },
        }},
        { "NEXT LEVEL", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Next Level" },
            { SystemLanguage.French, "Niveau Suivant" },
            { SystemLanguage.Spanish, "SIGUIENTE NIVEL" },
            { SystemLanguage.German, "Nächstes Level" },
            { SystemLanguage.Italian, "Prossimo livello" },
            { SystemLanguage.Russian, "Следующий уровень" },
        }},
        { "REPLAY", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Replay" },
            { SystemLanguage.French, "Rejouer" },
            { SystemLanguage.Spanish, "REPETICIÓN" },
            { SystemLanguage.German, "Wiederholen" },
            { SystemLanguage.Italian, "Rigioca" },
            { SystemLanguage.Russian, "Повторить" },
        }},
        { "MENU", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Menu" },
            { SystemLanguage.French, "Menu" },
            { SystemLanguage.Spanish, "MENÚ" },
            { SystemLanguage.German, "Menü" },
            { SystemLanguage.Italian, "Menu" },
            { SystemLanguage.Russian, "Меню" },
        }},
        { "RESUME", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Resume" },
            { SystemLanguage.French, "Continuer" },
            { SystemLanguage.Spanish, "REANUDAR" },
            { SystemLanguage.German, "Fortsetzen" },
            { SystemLanguage.Italian, "Riprendi" },
            { SystemLanguage.Russian, "Продолжить" },
        }},
        { "SKIP", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Skip" },
            { SystemLanguage.French, "Passer" },
            { SystemLanguage.Spanish, "SALTAR" },
            { SystemLanguage.German, "Überspringen" },
            { SystemLanguage.Italian, "Salta" },
            { SystemLanguage.Russian, "Пропустить" },
        }},
        { "ARE YOU SURE ?", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Are you sure ?" },
            { SystemLanguage.French, "Étes-vous sûr ?" },
            { SystemLanguage.Spanish, "ESTÁ SEGURO ?" },
            { SystemLanguage.German, "Sind Sie sicher?" },
            { SystemLanguage.Italian, "Sei sicuro ?" },
            { SystemLanguage.Russian, "Вы уверены?" },
        }},
        { "YES", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Yes" },
            { SystemLanguage.French, "Oui" },
            { SystemLanguage.Spanish, "SÍ" },
            { SystemLanguage.German, "Ja" },
            { SystemLanguage.Italian, "Sì" },
            { SystemLanguage.Russian, "Да" },
        }},
        { "NO", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "No" },
            { SystemLanguage.French, "Non" },
            { SystemLanguage.Spanish, "NO" },
            { SystemLanguage.German, "Nein" },
            { SystemLanguage.Italian, "No" },
            { SystemLanguage.Russian, "Нет" },
        }},
        { "WORLD", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "World" },
            { SystemLanguage.French, "Monde" },
            { SystemLanguage.Spanish, "MUNDO" },
            { SystemLanguage.German, "Welt" },
            { SystemLanguage.Italian, "Mondo" },
            { SystemLanguage.Russian, "Мир" },
        }},
        { "LEVEL", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Level" },
            { SystemLanguage.French, "Niveau" },
            { SystemLanguage.Spanish, "NIVEL" },
            { SystemLanguage.German, "Stufe" },
            { SystemLanguage.Italian, "Livello" },
            { SystemLanguage.Russian, "Уровень" },
        }},
        { "LANGUAGE", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Language" },
            { SystemLanguage.French, "Langue" },
            { SystemLanguage.Spanish, "Idioma" },
            { SystemLanguage.German, "Sprache" },
            { SystemLanguage.Italian, "Lingua" },
            { SystemLanguage.Russian, "Язык" },
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
            Debug.Log($"No translation for '{key}'!");
            return key;
        }
    }
}