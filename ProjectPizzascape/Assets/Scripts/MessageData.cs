using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/MessageDataScriptableObject", order = 1)]
public class MessageData : ScriptableObject
{
    public string sender;
    public string messageContent;
}
