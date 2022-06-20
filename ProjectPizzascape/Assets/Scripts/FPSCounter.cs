using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public float timer, refresh, avgFramerate;
    string display = "{0} FPS";
    public TextMeshProUGUI m_Text;
    private string fpsPerUpdateText;

#if UNITY_EDITOR

    private void Start()
    {
        this.gameObject.SetActive(true);
        InvokeRepeating("GetFPS", 0.5f, 0.5f);
    }
    private void GetFPS()
    {
        //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        m_Text.text = string.Format(display, avgFramerate.ToString());
    }
#endif

}
