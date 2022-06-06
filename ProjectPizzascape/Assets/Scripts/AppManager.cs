using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField] private List<AppIcon> appInMainView = new List<AppIcon>();
    private int appAccessLevel;

    private void Start()
    {
        appAccessLevel = PlayerPrefs.GetInt("appAccessLevel", 0);
        CheckAppsAccessibility();
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
                if (!app.isActiveAndEnabled)
                {
                    app.NotificationOn();
                }
                app.gameObject.SetActive(true);
            }
        }
    }

    public void IncreaseAppAccessLevel()
    {
        appAccessLevel++;
        PlayerPrefs.SetInt("appAccessLevel", appAccessLevel);
        CheckAppsAccessibility();
    }
}
