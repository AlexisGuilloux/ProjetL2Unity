using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChairContent : PanelContent
{
    [SerializeField] private Transform rodTransform;

    [SerializeField] private Button buttonToPush;
    private bool buttonTapped = false;
    
    
    private float objectif = 100;
    private Vector3 defaultRodPosition;
    // Start is called before the first frame update
    void Start()
    {
        panelContentBackgroundImage.color = Color.black;
        defaultRodPosition = rodTransform.localPosition;
        objectif = defaultRodPosition.y + 500f;
        buttonToPush.onClick.RemoveAllListeners();
        buttonToPush.onClick.AddListener(TapButton);
    }
    
    private void TapButton()
    {
        //buttonTapped = true;
        var position1 = rodTransform.localPosition;
        position1 = new Vector3(position1.x, position1.y + 20f, position1.z);

        if (position1.y >= objectif)
        {
            buttonToPush.interactable = false;
            Debug.Log("Done!");
            appManager.IncreaseAppAccessLevel();
        }
        
        rodTransform.DOLocalMove(position1, 0.25f);
    }
}
