using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class BootService : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadingCG;
    [SerializeField] private Transform feedbackParentTransform;
    [SerializeField] private Button[] numberButtons;
    [SerializeField] private Image[] feedbackImages;

    public bool homeLoaded = false;
    private string codeAttempt = "";
    private const string PASSWORD = "1010";
    int numberPressed = 0;

    private void Awake()
    {
        for (int i = 0; i < numberButtons.Length; i++)
        {
            numberButtons[i].onClick.RemoveAllListeners();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        loadingCG.alpha = 1f;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        
    }

    private void Update()
    {
        if(PASSWORD == codeAttempt)
        {
            PhoneUnlocked();
            return;
        }

        if(codeAttempt.Length >= 4)
        {
            WrongPasswordAnimation();
            codeAttempt = "";
            for (int i = 0; i < feedbackImages.Length; i++)
            {
                feedbackImages[i].gameObject.SetActive(false);
            }
        }

        if (numberPressed != codeAttempt.Length)
        {
            numberPressed = codeAttempt.Length;

            for (int i = 0; i < codeAttempt.Length; i++)
            {
                feedbackImages[i].gameObject.SetActive(true);
            }
        }
    }

    public void InputNumber(int index)
    {
        codeAttempt += index.ToString();
    }

    private void PhoneUnlocked()
    {

        for (int i = 0; i < numberButtons.Length; i++)
        {
            numberButtons[i].interactable = false;
        }

        Sequence sequence = DOTween.Sequence();
        sequence.Append(loadingCG.DOFade(0f, 1f))
                .AppendCallback(delegate { loadingCG.gameObject.SetActive(false); })
                .AppendCallback(delegate { SceneManager.UnloadSceneAsync("Boot"); }); ;
    }

    private void WrongPasswordAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(delegate
        {
            for (int i = 0; i < numberButtons.Length; i++)
            {
                numberButtons[i].interactable = false;
            }
        });
        sequence.Append(feedbackParentTransform.DOLocalMoveX(-30f, 0.15f));
        sequence.Append(feedbackParentTransform.DOLocalMoveX(25f, 0.1f));
        sequence.Append(feedbackParentTransform.DOLocalMoveX(-20f, 0.1f));
        sequence.Append(feedbackParentTransform.DOLocalMoveX(10f, 0.08f));
        sequence.Append(feedbackParentTransform.DOLocalMoveX(0f, 0.04f));
        sequence.AppendCallback(delegate
        {
            for (int i = 0; i < numberButtons.Length; i++)
            {
                numberButtons[i].interactable = true;
            }
        });
    }
}
