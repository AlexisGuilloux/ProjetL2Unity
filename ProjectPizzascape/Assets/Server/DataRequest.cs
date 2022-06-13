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

    //Pas utilis� pour l'instant mais peut servir a relier le code contenu dans les messages a voir
    public static JSONObject unlockCupboard;



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

        triggerLever.valueChangeHandler += notifLeverHandler;
        triggerCupboard.valueChangeHandler += notifCupboardHandler;
        unlockCupboard.valueChangeHandler += correctCode;

        StartCoroutine(triggerLever.corWatch());
        StartCoroutine(triggerCupboard.corWatch());
        StartCoroutine(unlockCupboard.corWatch());
        StartCoroutine(corDebug());
    }
    public IEnumerator corDebug()
    {
        print("waiting");
        yield return new WaitForSeconds(2);

        RaiseChair["value"] = true;
        RaiseChair.send();
        print("send raisechair");

        yield return new WaitForSeconds(2);
        SwitchLever["value"] = true;
        SwitchLever.send();

        print("send switchlever");
        xSpeed["value"] = .5f;
        xSpeed.send();
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

    // Update is called once per frame
    void Update()
    {


    }
     
}
