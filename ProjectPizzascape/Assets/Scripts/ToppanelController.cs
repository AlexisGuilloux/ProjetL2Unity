using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToppanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clock;

    private void Awake()
    {
        StartCoroutine(GetMicrosoftTime());
    }

    private IEnumerator GetMicrosoftTime()
    {
        while (true)
        {
            clock.text = System.DateTime.Now.ToString("t");
            yield return new WaitForSeconds(1);
        }
        
    }

    private void OnDestroy()
    {
        StopCoroutine(GetMicrosoftTime());
    }
}
