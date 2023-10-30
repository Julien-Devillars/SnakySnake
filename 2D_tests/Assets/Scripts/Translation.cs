using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public static class Translation
{
    static public SystemLanguage getLanguage()
    {
        return ES3.Load<SystemLanguage>("Language", Application.systemLanguage);
    }
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
        { "USE THE ARROWS OR ZQSD TO MOVE.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Use the arrows or ZQSD to move." },
            { SystemLanguage.French, "Utilisez les flèches ou ZQSD pour vous déplacer." },
            { SystemLanguage.Spanish, "Utilice las flechas o ZQSD para moverse." },
            { SystemLanguage.German, "Verwenden Sie die Pfeiltasten oder ZQSD, um sich zu bewegen." },
            { SystemLanguage.Italian, "Usa le frecce o ZQSD per muoverti." },
            { SystemLanguage.Russian, "Используйте стрелки или ZQSD для перемещения." },
        }},
        { "CATCH ALL THE STARS TO END THE LEVEL.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Catch all the stars to end the level." },
            { SystemLanguage.French, "Attrapez toutes les étoiles pour terminer le niveau." },
            { SystemLanguage.Spanish, "Atrapa todas las estrellas para terminar el nivel." },
            { SystemLanguage.German, "Fange alle Sterne ein, um das Level zu beenden." },
            { SystemLanguage.Italian, "Cattura tutte le stelle per completare il livello." },
            { SystemLanguage.Russian, "Соберите все звёзды, чтобы завершить уровень." },
        }},
        { "YOU LOSE IF SOMEONE HITS YOUR TRAIL !", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "You lose if someone hits your trail!" },
            { SystemLanguage.French, "Vous perdez si quelqu'un touche votre trait !" },
            { SystemLanguage.Spanish, "¡Pierdes si alguien toca tu estela!" },
            { SystemLanguage.German, "Du verlierst, wenn jemand deine Spur trifft!" },
            { SystemLanguage.Italian, "Perdi se qualcuno colpisce la tua scia!" },
            { SystemLanguage.Russian, "Вы проиграли, если кто-то касается вашего следа!" },
        }},
        { "PRESS SPACE BAR TO MOVE SLOWLY", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Press Space bar to move slowly" },
            { SystemLanguage.French, "Appuyez sur la barre d'espace pour vous déplacer lentement." },
            { SystemLanguage.Spanish, "Presiona la barra espaciadora para dejar de moverte. Sigue presionando para moverte lentamente." },
            { SystemLanguage.German, "Drücke die Leertaste, um dich zu stoppen. Halte gedrückt, um langsam zu bewegen." },
            { SystemLanguage.Italian, "Premi il tasto Spazio per smettere di muoverti. Continua a premere per muoverti lentamente." },
            { SystemLanguage.Russian, "Нажмите пробел, чтобы остановиться. Продолжайте нажимать, чтобы медленно двигаться." },
        }},
        { "THE CREATED BORDERS WILL HELP YOU OUT!", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "The created borders will help you out!" },
            { SystemLanguage.French, "Les bordures créées vous aideront !" },
            { SystemLanguage.Spanish, "¡Las fronteras creadas te ayudarán!" },
            { SystemLanguage.German, "Die erstellten Grenzen werden Ihnen helfen!" },
            { SystemLanguage.Italian, "I confini creati ti aiuteranno!" },
            { SystemLanguage.Russian, "Созданные границы помогут вам!" },
        }},
        { "YOU CAN SKIP THE LEVEL AND COME BACK LATER.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "You can skip the level and come back later." },
            { SystemLanguage.French, "Vous pouvez sauter le niveau et revenir plus tard." },
            { SystemLanguage.Spanish, "Puedes saltarte el nivel y volver más tarde." },
            { SystemLanguage.German, "Du kannst das Level überspringen und später zurückkommen." },
            { SystemLanguage.Italian, "Puoi saltare il livello e tornare più tardi." },
            { SystemLanguage.Russian, "Вы можете пропустить уровень и вернуться позже." },
        }},
        { "SOME ENEMIES CAN BE LINKED, DO NOT CROSS IT!", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Some enemies can be linked, do not cross it!" },
            { SystemLanguage.French, "Certains ennemis peuvent être liés, ne les traversez pas !" },
            { SystemLanguage.Spanish, "Algunos enemigos pueden estar vinculados, ¡no los cruces!" },
            { SystemLanguage.German, "Einige Feinde können verknüpft sein, überquere sie nicht!" },
            { SystemLanguage.Italian, "Alcuni nemici possono essere collegati, non attraversarli!" },
            { SystemLanguage.Russian, "Некоторые враги могут быть связаны, не пересекайте их!" },
        }},
        { "NEW ENEMY : CHANGES DIRECTIONS IN CYCLES !", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "New enemy: Changes directions in cycles!" },
            { SystemLanguage.French, "Nouvel ennemi : Change de direction en cycles !" },
            { SystemLanguage.Spanish, "Nuevo enemigo: Cambia de dirección en ciclos!" },
            { SystemLanguage.German, "Neuer Feind: Ändert Richtungen in Zyklen!" },
            { SystemLanguage.Italian, "Nuovo nemico: Cambia direzione in cicli!" },
            { SystemLanguage.Russian, "Новый враг: Меняет направление в циклах!" },
        }},
        { "TRY TO UNDERSTAND THEIR PATTERN.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Try to understand their pattern." },
            { SystemLanguage.French, "Essayez de comprendre leur déplacement." },
            { SystemLanguage.Spanish, "Intenta entender su patrón." },
            { SystemLanguage.German, "Versuche ihr Muster zu verstehen." },
            { SystemLanguage.Italian, "Cerca di capire il loro modello." },
            { SystemLanguage.Russian, "Попробуйте понять их шаблон." },
        }},
        { "FIND A PASSAGE BY BREAKING ENEMIES' PATTERN.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Find a passage by breaking enemies' pattern." },
            { SystemLanguage.French, "Trouvez un passage en brisant le déplacement des ennemis." },
            { SystemLanguage.Spanish, "Encuentra un pasaje rompiendo el patrón de los enemigos." },
            { SystemLanguage.German, "Finde einen Durchgang, indem du das Muster der Feinde brichst." },
            { SystemLanguage.Italian, "Trova un passaggio rompendo il modello dei nemici." },
            { SystemLanguage.Russian, "Найдите проход, разбивая шаблон врагов." },
        }},
        { "NEW ENEMY : IT WILL FOLLOW YOU !", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "New enemy: It will follow you!" },
            { SystemLanguage.French, "Nouvel ennemi : Il te suivra !" },
            { SystemLanguage.Spanish, "Nuevo enemigo: ¡Te seguirá!" },
            { SystemLanguage.German, "Neuer Feind: Er wird dir folgen!" },
            { SystemLanguage.Italian, "Nuovo nemico: Ti seguirà!" },
            { SystemLanguage.Russian, "Новый враг: Он будет следовать за тобой!" },
        }},
        { "NEW ENEMY : IT CAN CROSS THE BORDERS.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "New enemy: It can cross the borders." },
            { SystemLanguage.French, "Nouvel ennemi : Il peut traverser les bordures." },
            { SystemLanguage.Spanish, "Nuevo enemigo: Puede cruzar las fronteras." },
            { SystemLanguage.German, "Neuer Feind: Er kann die Grenzen überqueren." },
            { SystemLanguage.Italian, "Nuovo nemico: Può attraversare i confini." },
            { SystemLanguage.Russian, "Новый враг: Он может пересекать границы." },
        }},
        { "NEW ENEMY : IT WILL CHASE YOU THROUGHT THE BORDER.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "New Enemy: It will chase you through the border." },
            { SystemLanguage.French, "Nouvel ennemi : Il te poursuivra à travers les bordures." },
            { SystemLanguage.Spanish, "Nuevo enemigo: Te perseguirá a través de la frontera." },
            { SystemLanguage.German, "Neuer Feind: Er wird dich durch die Grenze verfolgen." },
            { SystemLanguage.Italian, "Nuovo nemico: Ti perseguiterà attraverso il confine." },
            { SystemLanguage.Russian, "Новый враг: Он будет преследовать тебя через границу." },
        }},
        { "BE CAREFUL TO NOT GET CAUGHT UP.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Be careful not to get caught up." },
            { SystemLanguage.French, "Faites attention de ne pas vous faire prendre." },
            { SystemLanguage.Spanish, "Ten cuidado de no quedar atrapado." },
            { SystemLanguage.German, "Sei vorsichtig, nicht erwischt zu werden." },
            { SystemLanguage.Italian, "Fai attenzione a non farti cogliere." },
            { SystemLanguage.Russian, "Будь осторожен, чтобы не попасться." },
        }},
        { "NEW ENEMY : ACCELERATE EACH HIT UNTIL 0 THEN START AGAIN.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "New enemy: Accelerate each hit until 0, then start again." },
            { SystemLanguage.French, "Nouvel ennemi : Accélère à chaque coup jusqu'à 0, puis recommence." },
            { SystemLanguage.Spanish, "Nuevo enemigo: Acelera en cada golpe hasta llegar a 0, luego vuelve a empezar." },
            { SystemLanguage.German, "Neuer Feind: Beschleunige bei jedem Treffer bis 0, dann starte erneut." },
            { SystemLanguage.Italian, "Nuovo nemico: Accelera ad ogni colpo fino a 0, poi ricomincia." },
            { SystemLanguage.Russian, "Новый враг: Ускоряется с каждым ударом до 0, затем начинает сначала." },
        }},
        { "DIRECTOR", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Director" },
            { SystemLanguage.French, "Réalisateur" },
            { SystemLanguage.Spanish, "Director" },
            { SystemLanguage.German, "Regisseur" },
            { SystemLanguage.Italian, "Regista" },
            { SystemLanguage.Russian, "Режиссер" },
        }},
        { "DEVELOPER", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Developer" },
            { SystemLanguage.French, "Développeur" },
            { SystemLanguage.Spanish, "Desarrollador" },
            { SystemLanguage.German, "Entwickler" },
            { SystemLanguage.Italian, "Sviluppatore" },
            { SystemLanguage.Russian, "Разработчик" },
        }},
        { "GRAPHICS", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Graphics" },
            { SystemLanguage.French, "Graphiques" },
            { SystemLanguage.Spanish, "Gráficos" },
            { SystemLanguage.German, "Grafik" },
            { SystemLanguage.Italian, "Grafica" },
            { SystemLanguage.Russian, "Графика" },
        }},
        { "TRANSLATION", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Translation" },
            { SystemLanguage.French, "Traduction" },
            { SystemLanguage.Spanish, "Traducción" },
            { SystemLanguage.German, "Übersetzung" },
            { SystemLanguage.Italian, "Traduzione" },
            { SystemLanguage.Russian, "Перевод" },
        }},
        { "SPECIAL THANKS", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Special Thanks" },
            { SystemLanguage.French, "Remerciements spéciaux" },
            { SystemLanguage.Spanish, "Agradecimientos especiales" },
            { SystemLanguage.German, "Besonderer Dank" },
            { SystemLanguage.Italian, "Ringraziamenti speciali" },
            { SystemLanguage.Russian, "Особая благодарность" },
        }},
        { "THANKS FOR TESTING THE GAME. UNLOCK OTHER LEVELS BY BUYING THE GAME.", new Dictionary<SystemLanguage, string>(){
            { SystemLanguage.English, "Thanks for testing the game. Unlock other levels by buying the game." },
            { SystemLanguage.French, "Merci d'avoir testé le jeu. Débloquez d'autres niveaux en achetant le jeu." },
            { SystemLanguage.Spanish, "Gracias por probar el juego. Desbloquea otros niveles comprando el juego." },
            { SystemLanguage.German, "Vielen Dank für das Testen des Spiels. Schalte weitere Level frei, indem du das Spiel kaufst." },
            { SystemLanguage.Italian, "Grazie per aver testato il gioco. Sblocca altri livelli acquistando il gioco." },
            { SystemLanguage.Russian, "Спасибо за тестирование игры. Pазблокируйте другие уровни, купив игру." },
        }}
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