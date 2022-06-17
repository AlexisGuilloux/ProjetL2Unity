using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{

    public bool active = false;
    public JSONObject activateLightsTrigger;
    // Start is called before the first frame update
    void Start()
    {
        activateLightsTrigger = new JSONObject("activateLightsTrigger", active);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleLights()
    {
        activateLightsTrigger["value"] = !activateLightsTrigger["value"].Value<bool>();
        activateLightsTrigger.send();
        if (activateLightsTrigger["value"].Value<bool>())
        {
            print("Lights on");
        }
        else
        {
            print("Lights out");
        }
    }


}
