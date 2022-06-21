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
        DataRequest.triggerCupboard.valueChangeHandler += InitMessageNotificationCheck;
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

    private static void InitHackCheck(object sender, EventArgs e)
    {
        _instance.ShowHackAppIcon();
    }

    public void ShowHackAppIcon()
    {
        if (DataRequest.unlockCupboard["value"].Value<bool>())
        {
            print("Hack icon ON");
            appInMainView[1].gameobjectState = true;
            appInMainView[1].NotificationParentOn();
        }
        else
        {
            print("Hack icon OFF");
            appInMainView[1].NotificationParentOff();
        }
    }

    private static void InitMessageNotificationCheck(object sender, EventArgs e)
    {
        _instance.ShowMessageAppNotification();
    }

    public void ShowMessageAppNotification()
    {
        if (DataRequest.triggerCupboard["value"].Value<bool>())
        {
            print("Message Notification ON");
            appInMainView[4].NotificationParentOn();
        }
    }
}
