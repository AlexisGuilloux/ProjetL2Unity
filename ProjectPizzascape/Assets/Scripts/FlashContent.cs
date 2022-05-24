using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashContent : PanelContent
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image feedback01;


    private bool sliderUnlocked = false;
    private float sliderTarget = 0.65f;

    // Start is called before the first frame update
    void Start()
    {
        panelContentBackgroundImage.color = Color.gray;
        feedback01.color = Color.red;
        sliderTarget = Random.Range(0.05f, 0.95f);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value >= (sliderTarget - 0.01f) && slider.value <= (sliderTarget + 0.01f))
        {
            SliderAmountRight();
            //slider.interactable = false;
        }
        else if (sliderUnlocked)
        {
            SliderAmountWrong();
        }
    }

    private void SliderAmountRight()
    {
        sliderUnlocked = true;
        feedback01.color = Color.green;
        AppManager.IncreaseAppAccessLevel();
    }

    private void SliderAmountWrong()
    {
        sliderUnlocked = false;
        feedback01.color = Color.red;
    }
}
