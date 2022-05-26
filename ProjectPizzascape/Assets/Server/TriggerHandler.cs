using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{

    public JSONObject closeCupboardTrigger;
    // Start is called before the first frame update
    void Start()
    {
        closeCupboardTrigger = new JSONObject("closeCupBoardTrigger", false);
        closeCupboardTrigger.valueChangeHandler += onCloseCupboardTriggerChange;
        closeCupboardTrigger.watch();
    }

    public static void onCloseCupboardTriggerChange(object sender, EventArgs e)
    {
        JSONObject jo = (JSONObject)sender;
        if (jo["value"].Value<bool>())
        {
            print("value is now true");
        }
        else
        {
            print("value is now false");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
