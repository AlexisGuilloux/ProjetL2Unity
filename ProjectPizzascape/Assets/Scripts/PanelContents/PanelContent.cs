using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelContent : MonoBehaviour
{
    [SerializeField] protected CanvasGroup panelContentCG;
    [SerializeField] protected Image panelContentBackgroundImage;

    protected AppManager appManager;

    private void Awake()
    {
        appManager = GameObject.FindObjectOfType<AppManager>();
    }
}
