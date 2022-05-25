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
    [SerializeField] private GameObject hackContentPrefab;
    private GameObject panelGO;
    private Transform panelTransform;
    private Vector3 appTransform;

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
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(ClosePanel);

        this.appTransform = appTransform;

        //Creating and handling a DOTWEEN sequence
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(delegate { panelTransform.position = appTransform; });
        sequence.AppendCallback(delegate { panelTransform.localScale = Vector3.zero; });
        sequence.AppendCallback(delegate { contentParentCG.alpha = 0f; });
        sequence.AppendCallback(delegate { gameObject.SetActive(true); });
        sequence.Append(panelTransform.DOMove(Vector3.zero, 0.3f));
        sequence.Join(panelTransform.DOScale(1f, 0.3f));
        sequence.Append(contentParentCG.DOFade(1f, 0.2f));

        //Instantiate the right panel
        //TODO: Something more generic
        switch (menuName)
        {
            case MenuNames.HACK:
                titleText.text = "Hack";
                Instantiate(hackContentPrefab, contentParentCG.gameObject.transform);
                break;
            case MenuNames.FLASH:
                titleText.text = "Torch";
                Instantiate(flashContentPrefab, contentParentCG.gameObject.transform);
                break;
            case MenuNames.MESSAGES:
                titleText.text = "Messages";
                Instantiate(messageContentPrefab, contentParentCG.gameObject.transform);
                break;
            case MenuNames.CALL:
                titleText.text = "Calls";
                break;
            case MenuNames.PARAMETERS:
                titleText.text = "Parameters";
                Instantiate(parametersContentPrefab, contentParentCG.gameObject.transform);
                break;
            case MenuNames.MUSIC:
                titleText.text = "Music";
                break;
            default:
                titleText.text = "Lorem ipsum";
                break;
        }
    }

    private void ClosePanel()
    {
        //Fade out animation
        Sequence sequence = DOTween.Sequence();
        sequence.Append(contentParentCG.DOFade(0f, 0.25f))
                .Append(panelTransform.DOMove(appTransform, 0.25f))
                .Join(panelTransform.DOScale(0f, 0.25f))
                .AppendCallback(delegate { gameObject.SetActive(false); });
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
