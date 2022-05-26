using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{

    public bool active = false;
    public JSONObject activeLightsTrigger;
    // Start is called before the first frame update
    void Start()
    {
        activeLightsTrigger = new JSONObject("activeLightsTrigger", active);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleLights()
    {
        activeLightsTrigger["value"] = !activeLightsTrigger["value"].Value<bool>();
        activeLightsTrigger.send();
        if (activeLightsTrigger["value"].Value<bool>())
        {
            print("Lights on");
        }
        else
        {
            print("Lights out");
        }
    }


}
