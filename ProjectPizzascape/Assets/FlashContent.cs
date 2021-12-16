using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashContent : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image feedback01;


    private bool sliderUnlocked = false;
    private float sliderTarget = 0.65f;

    // Start is called before the first frame update
    void Start()
    {
        feedback01.color = Color.red;
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
    }

    private void SliderAmountWrong()
    {
        sliderUnlocked = false;
        feedback01.color = Color.red;
    }
}
