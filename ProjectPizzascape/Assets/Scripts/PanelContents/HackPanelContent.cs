using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HackPanelContent : PanelContent
{
    [SerializeField] private Button[] numberButtons;
    [SerializeField] private TextMeshProUGUI codeAttemptTextFeedback;
    [SerializeField] private Image feedbackFieldImage;
    private string codeAttempt = "";
    private string passwordExemple = "1111";
    int numberPressed = 0;

    void Start()
    {
        codeAttemptTextFeedback.text = "enter code";
        codeAttemptTextFeedback.color = Color.grey;
    }

    private void Update()
    {
        if (passwordExemple == codeAttempt)
        {
            ChangeAllButtonsInteractability(false);
            feedbackFieldImage.color = Color.green;
            codeAttempt = "";
            return;
        }

        if (codeAttempt.Length >= 4)
        {
            codeAttempt = "";
        }
    }

    private void ChangeAllButtonsInteractability(bool interactable)
    {
        foreach (var button in numberButtons)
        {
            button.interactable = interactable;
        }
    }

    public void InputNumber(int index)
    {
        if(codeAttemptTextFeedback.color == Color.grey)
        {
            codeAttemptTextFeedback.color = Color.black;
        }

        codeAttempt += index.ToString();
        codeAttemptTextFeedback.text = codeAttempt;
    }
}
