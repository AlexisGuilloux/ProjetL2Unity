using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField] private List<AppIcon> appInMainView = new List<AppIcon>();
    private static int appAccessLevel = 0;
    private int tempLevel = -1;

    private void Update()
    {
        if(appAccessLevel != tempLevel)
        {
            tempLevel = appAccessLevel;
            CheckAppsAccessibility();
        }
    }

    private void CheckAppsAccessibility()
    {
        foreach (var app in appInMainView)
        {
            if(appAccessLevel < app.levelToAccessApp)
            {
                app.gameObject.SetActive(false);
            }
            else
            {
                app.gameObject.SetActive(true);
            }
        }
    }

    public static void IncreaseAppAccessLevel()
    {
        appAccessLevel++;
    }
}
