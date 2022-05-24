using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class BootService : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainCG;
    [SerializeField] private CanvasGroup PasswordCG;
    [SerializeField] private Transform feedbackParentTransform;
    [SerializeField] private Button[] numberButtons;
    [SerializeField] private Image[] feedbackImages;
    [SerializeField] private Image waitingImage;

    private string codeAttempt = "";
    private const string PASSWORD = "1010";
    int numberPressed = 0;
    private AsyncOperation currentOperation;

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
        DOTween.Init();
        waitingImage.gameObject.SetActive(false);
        PasswordCG.alpha = 1f;
    }

    private void Update()
    {
        if(PASSWORD == codeAttempt)
        {
            codeAttempt = "";
            StartCoroutine(PhoneUnlocked());
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
    private void OnDestroy()
    {
        StopCoroutine(PhoneUnlocked());
    }

    public void InputNumber(int index)
    {
        codeAttempt += index.ToString();
    }

    private IEnumerator PhoneUnlocked()
    {

        for (int i = 0; i < numberButtons.Length; i++)
        {
            numberButtons[i].interactable = false;
        }

        currentOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        waitingImage.color = new Color(1f, 1f, 1f, 0f);
        waitingImage.gameObject.SetActive(true);
        Sequence waitingSequence = DOTween.Sequence();
        waitingSequence.Append(waitingImage.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -120f), 0.4f).SetEase(Ease.Linear))
                       .Append(waitingImage.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -240f), 0.4f).SetEase(Ease.Linear))
                       .Append(waitingImage.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.4f).SetEase(Ease.Linear))
                       .SetLoops(-1);

        Sequence focusOnLoading = DOTween.Sequence();
        focusOnLoading.Append(PasswordCG.DOFade(0f, 0.4f))
                      .Append(waitingImage.DOFade(1f, 0.4f));
                      //.Append(waitingImage.gameObject.transform.DOLocalMoveY(0f, 0.5f).SetEase(Ease.Linear));

        while (currentOperation.progress < 1)
        {
            yield return new WaitForSecondsRealtime(2f);
        }

        Sequence sequence = DOTween.Sequence();
        sequence.Append(waitingImage.DOFade(0f, 0.25f))
                .Join(mainCG.DOFade(0f, 0.5f))
                .AppendCallback(delegate { PasswordCG.gameObject.SetActive(false); waitingImage.gameObject.SetActive(false); })
                .AppendCallback(delegate { SceneManager.UnloadSceneAsync("Boot"); });
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
