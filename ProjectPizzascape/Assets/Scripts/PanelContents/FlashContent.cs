using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashContent : PanelContent
{
    [SerializeField] private Button onOffButton;
    [SerializeField] private Image onOffFeedbackImage;

    private bool lightOn = false;
    // Start is called before the first frame update
    void Start()
    {
        panelContentBackgroundImage.color = Color.black;
        onOffButton.onClick.RemoveAllListeners();
        onOffButton.onClick.AddListener(ToggleOnOffButton);

        if(PlayerPrefs.GetInt("LightState", 0) == 1)
        {
            lightOn = true;
            onOffFeedbackImage.color = Color.white;
        }
    }

    private void ToggleOnOffButton()
    {
        if (lightOn)
        {
            lightOn = false;
            onOffFeedbackImage.color = Color.grey;
            PlayerPrefs.SetInt("LightState", 0);
        }
        else
        {
            lightOn = true;
            onOffFeedbackImage.color = Color.white;
            if(PlayerPrefs.GetInt("FirstFlash", 1) == 1)
            {
                appManager.IncreaseAppAccessLevel();
                PlayerPrefs.SetInt("FirstFlash", 0);
            }
            PlayerPrefs.SetInt("LightState", 1);
        }

        AudioManager._instance.PlayPushNeutralSound();
    }
}
