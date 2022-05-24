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


    public void InitMessage(MessageData data)
    {
        nameText.text = data.sender;
        messageSampleText.text = data.messageContent;
    }
}
