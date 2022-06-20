using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using UnityEngine.Serialization;

public class LeverContent : PanelContent
{
    [SerializeField] private Slider leverSlider;
    [SerializeField] private Button testButton;
    public static JSONObject triggerLever;
    private JSONObject leverDownTrigger;

    private float sliderValue = 0f;
    private bool sliderDown = false;
    private static bool puzzleDone = false;
    private static bool waitingForAnswer = false;
    private AppIcon messageAppIcon;

    void Start()
    {
#if !UNITY_EDITOR

        testButton.gameobject.SetActive(false);
        
#endif

        messageAppIcon = GameObject.Find("Messages").GetComponent<AppIcon>();
        //Listener
        triggerLever = new JSONObject("triggerLever", false);
        triggerLever.valueChangeHandler += notifLeverHandler;
        triggerLever.watch();

        //Trigger
        leverDownTrigger = new JSONObject("SwitchLever", sliderDown);
        sliderValue = leverSlider.normalizedValue;

        if (PlayerPrefs.GetInt("leverDone", 0) == 0)
        {
            StartCoroutine(GetSliderValue());
            testButton.onClick.RemoveAllListeners();
            testButton.onClick.AddListener(ForceCompletion);
        }
        else
        {
            leverSlider.interactable = false;
            leverSlider.normalizedValue = 1f;
        }

    }

    private void OnDisable()
    {
        StopCoroutine(GetSliderValue());
    }

    private IEnumerator GetSliderValue()
    {
        while (!puzzleDone)
        {
            /*
            //Info send to Unreal, waiting for an answer
            if (waitingForAnswer)
            {
                print("Waiting for an answer");
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            //Answer is negative and players need to retry
            if (sliderDown && !waitingForAnswer && !puzzleDone)
            {
                StartAgain();
            }*/

            //Get slider value
            if (Math.Abs(sliderValue - leverSlider.normalizedValue) > 0f)
            {
                sliderValue = leverSlider.normalizedValue;
            }

            //Slider down, sending the info to Unreal
            if (sliderValue >= 0.999f)
            {
                AudioManager._instance.PlayPushNeutralSound();
                sliderDown = true;
                leverSlider.interactable = false;
                SendLeverDownData();

                //Wait for 3 senconds then send the slider up again
                yield return new WaitForSeconds(3);
                StartAgain();
                leverDownTrigger["value"] = false;
                leverDownTrigger.send();

            }

            yield return new WaitForSeconds(0.1f);
        }

        if (puzzleDone)
        {
            leverSlider.interactable = false;
            PlayerPrefs.SetInt("leverDone", 1);
            GetMessageNotificationOn();
        }

        yield return null;
    }

    private void SendLeverDownData()
    {
        waitingForAnswer = true;
        leverDownTrigger["value"] = sliderDown;
        leverDownTrigger.send();

        print("Lever down, sending info");
    }

    static void notifLeverHandler(object sender, EventArgs e)
    {
        puzzleDone = (((JSONObject)sender)["value"] ?? false).Value<bool>();

        triggerLever["value"] = ((JSONObject)sender)["value"];
        waitingForAnswer = false;
    }

    private void StartAgain()
    {
        sliderValue = 0f;
        leverSlider.normalizedValue = sliderValue;
        leverSlider.interactable = true;
        AudioManager._instance.PlayClickNegativeSound();
    }
    
    private void GetMessageNotificationOn()
    {
        messageAppIcon.NotificationOn(false);
    }

    private void ForceCompletion()
    {
        print("Forcing completion!");
        puzzleDone = true;
        PlayerPrefs.SetInt("leverDone", 1);
        GetMessageNotificationOn();
    }
}