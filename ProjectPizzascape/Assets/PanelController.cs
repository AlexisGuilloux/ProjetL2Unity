using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI mainText;

    public void Init(MenuNames menuName)
    {
        gameObject.SetActive(true);
        switch (menuName)
        {
            case MenuNames.HACK:
                titleText.text = "Hack title";
                break;
            case MenuNames.FLASH:
                titleText.text = "Flash title";
                break;
            case MenuNames.MESSAGES:
                titleText.text = "Messages title";
                break;
            default:
                break;
        }

        mainText.text = "";
    }
}
public enum MenuNames
{
    DEFAULT,
    FLASH,
    HACK,
    MESSAGES
}
