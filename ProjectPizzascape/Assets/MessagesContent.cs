using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesContent : MonoBehaviour
{
    [SerializeField] private GameObject messageTabPrefab;
    private List<MessageTab> messages = new List<MessageTab>();
    public List<string> names = new List<string>();
    public List<string> messagesContent = new List<string>();
    [SerializeField] private Transform messagesTransformParent;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in messagesTransformParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < names.Count && i < messagesContent.Count; i++)
        {
            MessageTab newMessage = messageTabPrefab.GetComponent<MessageTab>();
            newMessage.InitMessage(names[i], messagesContent[i]);

            messages.Add(newMessage);
            Instantiate(messageTabPrefab, messagesTransformParent);
        }

        /*foreach (var message in messages)
        {
            Instantiate(message, messagesTransformParent);
        }*/
    }
}
