using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class BootService : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadingCG;
    [SerializeField] private Button[] numberButtons;
    [SerializeField] private Image[] feedbackImages;

    public bool homeLoaded = false;
    private string codeAttempt = "";
    private string code = "1010";
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
        if(code == codeAttempt)
        {
            PhoneUnlocked();
        }

        if(codeAttempt.Length >= 4)
        {
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
        Sequence sequence = DOTween.Sequence();
        sequence.Append(loadingCG.DOFade(0f, 1f));
        sequence.AppendCallback(delegate { loadingCG.gameObject.SetActive(false); });
    }
}
