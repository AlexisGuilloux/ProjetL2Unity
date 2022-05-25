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
        AppManager.IncreaseAppAccessLevel();
    }

    private void ToggleOnOffButton()
    {
        if (lightOn)
        {
            lightOn = false;
            onOffFeedbackImage.color = Color.grey;
        }
        else
        {
            lightOn = true;
            onOffFeedbackImage.color = Color.white;
        }
    }
}
