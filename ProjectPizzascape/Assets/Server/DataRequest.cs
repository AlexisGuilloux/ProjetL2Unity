using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRequest : MonoBehaviour
{


    //Utilis� par l'appli chaise pour envoyer au serveur l'�tat de la barre (true pour lev�)
    public static JSONObject RaiseChair;

    //Utilis� par l'appli levier pour envoyer au serveur l'�tat du levier (true quand abaiss�, false au bout de 3 secondes)
    public static JSONObject SwitchLever;

    //Utilis� pour savoir si le cupboard est d�bloqu�
    public static JSONObject unlockCupboard;

    //appel� � la fin de la partie pour afficher les cr�dits
    public static JSONObject triggerEndGame;

    //Passe a true quand le joueur passe devant le levier 
    public static JSONObject triggerLever;

    //Passe a true quand le joueur est devant l'armoire (mettre une notification sur les messages)
    public static JSONObject triggerCupboard;


    public static JSONObject xSpeed;
    public static JSONObject ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        xSpeed = new JSONObject("ballXSpeed", 0);
        ySpeed = new JSONObject("ballYSpeed", 0);

        RaiseChair = new JSONObject("RaiseChair", false);
        SwitchLever = new JSONObject("switchLever", false);


        unlockCupboard = new JSONObject("unlockCupboard", false);


        triggerLever = new JSONObject("triggerNotificationLever", false);
        triggerCupboard = new JSONObject("triggerNotificationCupboard", false);

        triggerEndGame = new JSONObject("triggerEndGame", false);

        triggerLever.valueChangeHandler += notifLeverHandler;
        triggerCupboard.valueChangeHandler += notifCupboardHandler;
        unlockCupboard.valueChangeHandler += correctCode;
        triggerEndGame.valueChangeHandler += notifEndGame;

        StartCoroutine(triggerLever.corWatch());
        StartCoroutine(triggerCupboard.corWatch());
        StartCoroutine(unlockCupboard.corWatch());
        StartCoroutine(triggerEndGame.corWatch());
        StartCoroutine(corDebug());
    }
    public IEnumerator corDebug()
    {
        print("waiting");
        yield return new WaitForSeconds(2);

        //RaiseChair["value"] = true;
        //RaiseChair.send();
        //print("send raisechair");

        yield return new WaitForSeconds(2);
        SwitchLever["value"] = true;
        SwitchLever.send();

        print("send switchlever");
        ySpeed["value"] = .5f;
        ySpeed.send();
        yield return null;

    }
    static void notifLeverHandler(object sender, EventArgs e)
    {
        print("Notif lever" + triggerLever["value"]);
    }

    static void notifCupboardHandler(object sender, EventArgs e)
    {
        print("Notif Cupboard" + triggerCupboard["value"]);
    }
    static void correctCode(object sender, EventArgs e)
    {
        print("Correct code" + unlockCupboard["value"]);
    }

    static void notifEndGame(object sender, EventArgs e)
    {
        print("End Game" + triggerEndGame["value"]);
    }

    // Update is called once per frame
    void Update()
    {


    }
     
}
