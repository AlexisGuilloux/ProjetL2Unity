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
    [Space]
    [Header("Puzzles prefabs")]
    [SerializeField] private PuzzleManager puzzleManager;


    private string codeAttempt = "";
    int puzzleIdFound = 0;

    void Start()
    {
        codeAttemptTextFeedback.text = "enter code";
        codeAttemptTextFeedback.color = Color.grey;
    }

    private void Update()
    {
        if (codeAttempt.Length == 4)
        {
            ChangeAllButtonsInteractability(false);

            if (PuzzleExist(codeAttempt))
            {
                feedbackFieldImage.color = Color.green;
                InitPuzzle(puzzleIdFound);
            }
            else
            {
                codeAttemptTextFeedback.color = Color.grey;
                codeAttemptTextFeedback.text = "enter code";
                ChangeAllButtonsInteractability(true);
            }

            codeAttempt = "";
            return;
        }

        if (codeAttempt.Length > 4)
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

    private bool PuzzleExist(string id)
    {
        int parsedInt = 0;
        System.Int32.TryParse(id, out parsedInt);
        bool puzzleExists = puzzleManager.PuzzleIds.Contains(parsedInt);
        if (puzzleExists)
        {
            puzzleIdFound = parsedInt;
        }
        return puzzleExists;
    }

    private void InitPuzzle(int id)
    {
        if (puzzleIdFound == 0) return;

        int index = puzzleManager.PuzzleIds.IndexOf(id);
        Instantiate(puzzleManager.PuzzlePrefabs[index], panelContentCG.transform);
    }
}
