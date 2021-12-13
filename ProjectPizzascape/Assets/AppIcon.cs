using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppIcon : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private PanelController panelController;
    [SerializeField] private MenuNames menuNames = MenuNames.DEFAULT;

    private void Awake()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { panelController.Init(menuNames); });
    }
}
