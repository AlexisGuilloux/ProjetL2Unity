using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WreckingBallContent : MonoBehaviour
{
    [SerializeField] private Slider xSlider;
    [SerializeField] private Slider ySlider;

    private float xLastValue = 0f;
    private float yLastValue = 0f;
    
    public static JSONObject xSpeed;
    public static JSONObject ySpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        xSpeed = new JSONObject("ballXSpeed", 0);
        ySpeed = new JSONObject("ballYSpeed", 0);
        
        
        xSlider.normalizedValue = 0.5f;
        ySlider.normalizedValue = 0.5f;
        xLastValue = xSlider.normalizedValue;
        yLastValue = ySlider.normalizedValue;
        StartCoroutine(UpdateAndSendData());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateAndSendData());
    }

    private IEnumerator UpdateAndSendData()
    {
        while (true)
        {
            if (Math.Abs(xLastValue - xSlider.normalizedValue) > 0.01f)
            {
                xLastValue = xSlider.normalizedValue;
                print("Update x value");
                //Send data for X axis
                ySpeed["value"] = xLastValue-0.5f;
                ySpeed.send();
            }

            if (Math.Abs(yLastValue - ySlider.normalizedValue) > 0.01f)
            {
                yLastValue = ySlider.normalizedValue;
                print("Update y value");
                //Send data for Y axis
                xSpeed["value"] = (yLastValue-0.5f)*-1;
                xSpeed.send();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
