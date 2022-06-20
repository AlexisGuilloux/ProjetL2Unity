using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class AppManager : MonoBehaviour
{
    [SerializeField] private List<AppIcon> appInMainView = new List<AppIcon>();
    private int appAccessLevel;
    public static AppManager _instance;
    private void Start()
    {
        appInMainView[1].gameObject.SetActive(false);

        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
        appAccessLevel = PlayerPrefs.GetInt("appAccessLevel", 0);
        CheckAppsAccessibility();
        if (appAccessLevel == 0)
        {
            IncreaseAppAccessLevel();   
        }
        StartCoroutine(corDebug());
    }
    public IEnumerator corDebug()
    {
        yield return new WaitForSeconds(1);
        DataRequest.unlockCupboard.valueChangeHandler += InitHackCheck;
    }
    private void CheckAppsAccessibility()
    {
        foreach (var app in appInMainView)
        {
            if (app.appId == 2001) continue;

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
    public void ShowHackAppIcon()
    {
        if (DataRequest.unlockCupboard["value"].Value<bool>())
        {
            print("Hack icon ON");
            appInMainView[1].gameObject.SetActive(true);
            if(PlayerPrefs.GetInt("FirstHackNotification", 1) == 1)
            {
                appInMainView[1].NotificationOn();
                PlayerPrefs.SetInt("FirstHackNotification", 0);
            }
        }
        else
        {
            print("Hack icon OFF");
            appInMainView[1].NotificationOff();
        }
    }
    private static void InitHackCheck(object sender, EventArgs e)
    {
        _instance.ShowHackAppIcon();
    }
}
