using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageTab : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    [SerializeField] public TextMeshProUGUI messageSampleText;
    [SerializeField] private Button button;


    public void InitMessage(string name, string message)
    {
        this.nameText.text = name;
        messageSampleText.text = message;
    }
}
