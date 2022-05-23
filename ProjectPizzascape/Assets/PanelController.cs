using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button backButton;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private CanvasGroup contentParentCG;
    [Space]
    [Header("PanelContentPrefabs")]
    [SerializeField] private GameObject flashContentPrefab;
    [SerializeField] private GameObject messageContentPrefab;
    [SerializeField] private GameObject parametersContentPrefab;
    private GameObject panelGO;
    private Transform panelTransform;

    private void Awake()
    {
        panelGO = this.gameObject;
        panelTransform = panelGO.transform;
    }
    private void OnDisable()
    {
        foreach (Transform child in contentParentCG.gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void Init(MenuNames menuName, Color menuColor, Vector3 appTransform)
    {
        //Get the colors
        backgroundImage.color = new Color(menuColor.r, menuColor.g, menuColor.b, 0.25f);
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(ClosePanel);

        //Creating and handling a DOTWEEN sequence
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(delegate { panelTransform.position = appTransform; });
        sequence.AppendCallback(delegate { panelTransform.localScale = Vector3.zero; });
        sequence.AppendCallback(delegate { contentParentCG.alpha = 0; });
        sequence.AppendCallback(delegate { gameObject.SetActive(true); });
        sequence.Append(panelTransform.DOMove(new Vector3(Screen.width / 2, Screen.height / 2, 0), 0.3f));
        sequence.Join(panelTransform.DOScale(1, 0.3f));
        sequence.Append(contentParentCG.DOFade(1f, 0.2f));


        switch (menuName)
        {
            case MenuNames.HACK:
                titleText.text = "Hack title";
                break;
            case MenuNames.FLASH:
                titleText.text = "Flash title";
                Instantiate(flashContentPrefab, contentParentCG.gameObject.transform);
                break;
            case MenuNames.MESSAGES:
                titleText.text = "Messages title";
                Instantiate(messageContentPrefab, contentParentCG.gameObject.transform);
                break;
            case MenuNames.CALL:
                titleText.text = "Call title";
                break;
            case MenuNames.PARAMETERS:
                titleText.text = "Parameters title";
                Instantiate(parametersContentPrefab, contentParentCG.gameObject.transform);
                break;
            case MenuNames.MUSIC:
                titleText.text = "Music title";
                break;
            default:
                titleText.text = "Lorem ipsum";
                break;
        }
    }

    private void ClosePanel()
    {
        panelGO.SetActive(false);
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
