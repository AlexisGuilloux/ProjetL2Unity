using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HackPanelContent : PanelContent
{
    [SerializeField] private Button[] numberButtons;
    [SerializeField] private Image[] numberInnerImage;
    [Space]
    [Header("Puzzles prefabs")]
    [SerializeField] private PuzzleManager puzzleManager;


    private string codeAttempt = "";
    int puzzleIdFound = 0;

    private void Update()
    {
        if (codeAttempt.Length == 3)
        {
            ChangeAllButtonsInteractability(false);

            if (PuzzleExist(codeAttempt))
            {
                DataRequest.dashBoardCode["value"] = true;
                DataRequest.dashBoardCode.send();
                InitPuzzle(puzzleIdFound);
                AudioManager._instance.PlayUnlockSound();
            }
            else
            {
                ChangeAllButtonsInteractability(true);
                AudioManager._instance.PlayClickNegativeSound();
            }

            codeAttempt = "";
            return;
        }

        if (codeAttempt.Length > 3)
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

        foreach (var image in numberInnerImage)
        {
            image.color = Color.black;
        }
    }

    public void InputNumber(int index)
    {
        numberInnerImage[index - 1].color = new Color(0f, 0f, 0f, 0.6f);
        codeAttempt += index.ToString();
        AudioManager._instance.PlayClickNeutralSound();
    }

    private bool PuzzleExist(string id)
    {
        System.Int32.TryParse(id, out var parsedInt);
        bool puzzleExists = puzzleManager.Ids.Contains(parsedInt);
        if (puzzleExists)
        {
            puzzleIdFound = parsedInt;
        }
        return puzzleExists;
    }

    private void InitPuzzle(int id)
    {
        if (puzzleIdFound == 0) return;

        int index = puzzleManager.Ids.IndexOf(id);
        Instantiate(puzzleManager.Prefabs[index], panelContentCG.transform);

        foreach (var button in numberButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
