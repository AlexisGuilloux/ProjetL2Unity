using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class AppIcon : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject notificationParent;
    [SerializeField] private PanelController panelController;
    [SerializeField] public int appId = 0;
    [SerializeField] public int levelToAccessApp;

    private Transform transform;
    private Sequence sequence;

    private bool notificationParentState;
    public bool gameobjectState;

    private void Awake()
    {
        gameobjectState = true;
        notificationParentState = false;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { panelController.Init(appId, iconImage.color, gameObject.transform.position);  NotificationOff();});
        transform = this.GetComponent<Transform>();

        InvokeRepeating("NotificationChecker", 0.5f, 0.5f);

        if(appId == 2001)
        {
            gameobjectState = false;
            InvokeRepeating("GameObjectChecker", 0.5f, 0.5f);
        }
    }

    private void OnDestroy()
    {
        CancelInvoke("GameObjectChecker");
        CancelInvoke("NotificationChecker");
    }

    public void NotificationOn(bool withAnimation = true)
    {
        if (withAnimation)
        {
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, -8f), 0.2f)).SetEase(Ease.Linear)
                    .Join(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f))
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, 5f), 0.25f)).SetEase(Ease.Linear)
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, -3f), 0.2f)).SetEase(Ease.Linear)
                    .Join(transform.DOScale(Vector3.one, 0.15f))
                    .Append(transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f)).SetEase(Ease.Linear)
                    .SetLoops(-1);
        }

        notificationParent.SetActive(true);
        
        //Play Notification sound
        AudioManager._instance.PlayNotificationSound();
    }

    public void NotificationParentOn()
    {
        notificationParentState = true;
    }

    public void NotificationParentOff()
    {
        notificationParentState = false;
    }

    public void NotificationChecker()
    {
        if (notificationParentState)
        {
            NotificationOn();
            notificationParentState = false;
        }
    }

    public void NotificationOff()
    {
        sequence.Kill();
        transform.localScale = Vector3.one;
        transform.DORotate(Vector3.zero, 0f);
        notificationParent.SetActive(false);
        notificationParentState = false;
    }


    public void GameObjectChecker()
    {
        print("while");
        if (gameobjectState)
        {
            print("game object true");
            gameObject.SetActive(true);
        }
        else
        {
            print("game object false");
            gameObject.SetActive(false);
        }
    }
}
