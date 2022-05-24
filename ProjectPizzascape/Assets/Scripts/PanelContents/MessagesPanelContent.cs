using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesPanelContent : PanelContent
{
    [SerializeField] private MessageTab messageTabPrefab;
    private List<MessageTab> messages = new List<MessageTab>();
    public List<MessageData> messagesData = new List<MessageData>();
    [SerializeField] private Transform messagesTransformParent;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in messagesTransformParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < messagesData.Count; i++)
        {
            MessageTab newMessage = Instantiate(messageTabPrefab, messagesTransformParent);
            newMessage.InitMessage(messagesData[i]);
            messages.Add(newMessage);
        }
    }
}