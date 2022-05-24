using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParameterButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI message;
    [SerializeField] private Button button;


    public void InitMessage(string name, string message)
    {
        this.nameText.text = name;
        this.message.text = message;
    }
}
