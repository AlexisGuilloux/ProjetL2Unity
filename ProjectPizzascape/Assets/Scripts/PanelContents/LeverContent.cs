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

    void Start()
    {

#if !UNITY_EDITOR

        testButton.gameObject.SetActive(false);
        
#endif

        //Trigger
        leverDownTrigger = new JSONObject("SwitchLever", sliderDown);
        sliderValue = leverSlider.normalizedValue;

        StartCoroutine(GetSliderValue());
        testButton.onClick.RemoveAllListeners();
        testButton.onClick.AddListener(ForceCompletion);
    }

    private void OnDisable()
    {
        StopCoroutine(GetSliderValue());
    }

    private IEnumerator GetSliderValue()
    {
        while (!puzzleDone)
        {
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

        yield return null;
    }

    private void SendLeverDownData()
    {
        waitingForAnswer = true;
        leverDownTrigger["value"] = sliderDown;
        leverDownTrigger.send();

        print("Lever down, sending info");
    }

    private void StartAgain()
    {
        sliderValue = 0f;
        leverSlider.normalizedValue = sliderValue;
        leverSlider.interactable = true;
        AudioManager._instance.PlayClickNegativeSound();
    }

    private void ForceCompletion()
    {
        print("Forcing completion!");
        puzzleDone = true;
    }
}