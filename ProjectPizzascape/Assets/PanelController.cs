using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private Button backButton;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private CanvasGroup contentParentCG;
    private Transform panelTransform;

    private void Awake()
    {

        panelTransform = this.gameObject.transform;
    }

    public void Init(MenuNames menuName, Color menuColor, Vector3 appTransform)
    {
        //Get the colors
        backgroundImage.color = menuColor;
        backButton.targetGraphic.color = new Color(menuColor.r-0.2f, menuColor.g-0.2f, menuColor.b-0.2f);

        //Creating and handling a DOTWEEN sequence
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(delegate { panelTransform.position = appTransform; });
        sequence.AppendCallback(delegate { panelTransform.localScale = Vector3.zero; });
        sequence.AppendCallback(delegate { contentParentCG.alpha = 0; });
        sequence.AppendCallback(delegate { gameObject.SetActive(true); });
        sequence.Append(panelTransform.DOMove(new Vector3(Screen.width/2, Screen.height/2, 0), 0.3f));
        sequence.Join(panelTransform.DOScale(1, 0.3f));
        sequence.Append(contentParentCG.DOFade(1f, 0.2f));

        
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
            case MenuNames.CALL:
                titleText.text = "Call title";
                break;
            case MenuNames.PARAMETERS:
                titleText.text = "Parameters title";
                break;
            case MenuNames.MUSIC:
                titleText.text = "Music title";
                break;
            default:
                titleText.text = "Lorem ipsum";
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
    MESSAGES,
    CALL,
    PARAMETERS,
    MUSIC
}
