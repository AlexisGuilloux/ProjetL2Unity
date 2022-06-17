using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAndNotify : MonoBehaviour
{
    public JSONObject sendY; 
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        sendY = new JSONObject(transform.name + ":cubeFloatY",0);
         StartCoroutine(notifyY());
    }



    public IEnumerator notifyY()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            sendY.send(); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        sendY["value"] = transform.position.y;
        
    }
}
