using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parametersContent : MonoBehaviour
{
    [SerializeField] private ParameterButton parameterButtonPrefab;
    [SerializeField] private Transform parameterTransformParent;
    private List<ParameterButton> parameters = new List<ParameterButton>();
    public List<string> names = new List<string>();
    public List<string> messagesContent = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in parameterTransformParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < names.Count && i < messagesContent.Count; i++)
        {
            ParameterButton newParameter = Instantiate(parameterButtonPrefab, parameterTransformParent);
            newParameter.InitMessage(names[i], messagesContent[i]);

            parameters.Add(newParameter);

        }
    }
}
