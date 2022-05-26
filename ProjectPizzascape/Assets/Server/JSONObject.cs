using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

 
public class JSONObject :JObject
{


    public event EventHandler valueChangeHandler;

    protected virtual void OnValueChanged(EventArgs e)
    {
        EventHandler handler = valueChangeHandler;
        handler?.Invoke(this, e);
    }


    public JSONObject()
    { 
    }
    public JSONObject(JToken key, dynamic value)
    {
        this["id"] = key;
        this["value"] = value;

    }
     

    public string ToStringDebug()
    {
        return "ID: " + this["id"] + "; value: " + this["value"];
    }
     
    public void send()
    {
        Request._instance.JSONObjects[this["id"].ToString()] = this;
        if (this["id"].Value<string>() !="" && Request._instance != null)
        {
            Request._instance.RequestQueue.Enqueue(this);
        }
    }

    public void watch()
    {
        Task task = new Task(async () => watchAsync());
        task.Start();

        
    } 
    public void watchAsync()
    {
        while(Request._instance == null || Request._instance.JSONObjects == null)
        {
            Thread.Sleep(1);
        }

        Request._instance.JSONObjects[this["id"].ToString()] = this;
    }
    public void changeValue(JToken token)
    {
        if (this["value"]!= token)
        {
            //Value has changed, change the value and send events
            this["value"] = token;
            OnValueChanged(null);
        }
    }
}
  