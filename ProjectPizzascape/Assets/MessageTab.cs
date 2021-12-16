using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageTab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI messageSampleText;
    [SerializeField] private Button button;

    public void InitMessage(string name, string message)
    {
        nameText = new TextMeshProUGUI();
        messageSampleText = new TextMeshProUGUI();
        nameText.text = name;
        messageSampleText.text = message;
    }
}
