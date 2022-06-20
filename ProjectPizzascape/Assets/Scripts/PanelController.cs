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

    [Space] [Header("PanelContentPrefabs")] [SerializeField]
    private PuzzleManager appContentLibrary;

    private int appIndex;
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

    public void Init(int id, Color menuColor, Vector3 appTransform)
    {
        //Get the colors
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(ClosePanel);

        this.appTransform = appTransform;

        if (!PuzzleExist(id))
        {
            AudioManager._instance.PlayClickNegativeSound();
            return;
        }
        AudioManager._instance.PlayClickNeutralSound();

        //Instantiate the right panel
        //TODO: Something more generic
        titleText.text = appContentLibrary.MainName[appIndex];
        Instantiate(appContentLibrary.Prefabs[appIndex], contentParentCG.gameObject.transform);

        //Creating and handling a DOTWEEN sequence
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(delegate { panelTransform.position = appTransform; });
        sequence.AppendCallback(delegate { panelTransform.localScale = Vector3.zero; });
        sequence.AppendCallback(delegate { contentParentCG.alpha = 0f; });
        sequence.AppendCallback(delegate { gameObject.SetActive(true); });
        sequence.Append(panelTransform.DOMove(Vector3.zero, 0.3f));
        sequence.Join(panelTransform.DOScale(1f, 0.3f));
        sequence.Append(contentParentCG.DOFade(1f, 0.2f));
    }
    
    private bool PuzzleExist(int id)
    {
        bool puzzleExists = appContentLibrary.Ids.Contains(id);
        if (puzzleExists)
        {
            appIndex = appContentLibrary.Ids.IndexOf(id);
        }
        return puzzleExists;
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
