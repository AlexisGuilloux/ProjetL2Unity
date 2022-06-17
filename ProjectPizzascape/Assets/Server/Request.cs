using System;
using System.Collections;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using NetMQ;
using NetMQ.Sockets;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

[System.Serializable]
public class JSONDemo
{
	public string uuid;
	public double x;
	public double y;

	public override string ToString()
	{
		return "UUID: " + uuid + "; x: " + x + "; y: " + y;
	}
}
public class TypeInfo
{
	public string type;
}
//Correspond à l'objet qu'on va envoyer dans le réseau

public class Request : MonoBehaviour
{



	public string ip = "localhost";


	public Queue<JSONObject> RequestQueue = new Queue<JSONObject>();

	private bool requesterIsStarted = false;
	private string outMsg = "";
	private string inMsg = "";

	private RequestSocket socket;

	//Socket faite pour recevoir les réponses
	private ResponseSocket socketServer;

	public Queue<JSONObject> requests;

	//les requetes du serveur
	public Queue<JSONObject> responsesServer;

	public Dictionary<string, JSONObject> JSONObjects = new Dictionary<string, JSONObject>();
	public static Request _instance;



	public JSONObject serverIp;
	void request()
	{


	}

	// Use this for initialization
	void Start()
	{

		serverIp = new JSONObject("serverIp", "");
		serverIp.watch();
		if (_instance != null && _instance != this)
		{
			Destroy(this);
		}
		else
		{
			_instance = this;
		}
		requesterIsStarted = true;
		Connect();



		//DEbug
		//StartCoroutine(corSwitchCupboardState());
	}
	public IEnumerator corSwitchCupboardState()
	{
		while (true)
		{

			yield return new WaitForSeconds(2);
			if (JSONObjects.ContainsKey("closeCupBoardTrigger"))
			{
				JSONObjects["closeCupBoardTrigger"].changeValue(!JSONObjects["closeCupBoardTrigger"]["value"].Value<bool>());

			}

		}
	}

	// Update is called once per frame
	void Update()
	{
		/*
		if (!string.Equals (inMsg, "")) {
			int spaceIndex = inMsg.IndexOf (' ');
			string type = inMsg.Substring (0, spaceIndex);
			string args = inMsg.Substring (spaceIndex + 1);

			switch (type) {
			case "Spawn":
				switch (args) {
				case "sphere":
					Instantiate (sphere, spawnPos.position, spawnPos.rotation);
					Debug.Log ("Spawning a sphere");
					break;
				case "cube":
					Instantiate (cube, spawnPos.position, spawnPos.rotation);
					Debug.Log ("Spawning a cube");
					break;
				default:
					Debug.Log ("Spawn: Unrecognized object");
					break;
				}
				break;
			case "JSON":
				Debug.Log (JsonUtility.FromJson<JSONDemo> (args));
				break;
			case "Error":
				Debug.Log ("ResponderError: " + args);
				break;
			default:
				Debug.Log (inMsg);
				break;
			}

			inMsg = "";
		}*/

	}

	void OnDestroy()
	{
		print("Closing the game...");
		if (socket != null)
		{
			socket.Close();
			socketServer.Close();
			((IDisposable)socketServer).Dispose();
			NetMQConfig.Cleanup();
		}
	}
	private void OnDisable()
	{
		print("Closing the game...");
		if (socket != null)
		{
			socket.Close();
			((IDisposable)socket).Dispose();
		}
		if (socketServer != null)
		{
			socketServer.Close();
			((IDisposable)socketServer).Dispose();

		}
		NetMQConfig.Cleanup();

	}


	public void Connect()
	{
		if (ip == "")
		{
			return;
		}
		AsyncIO.ForceDotNet.Force();



		try
		{

			socketServer = new ResponseSocket("tcp://*:5558");
			Task task = new Task(async () => ProcessResponses());
			task.Start();
		}
		catch (Exception e)
		{
			Debug.LogError("Couldn't connect to server... " + e);
			socket = null;
			socketServer = null;
		}

		StartCoroutine(waitForServerInfos());

	}
	public IEnumerator waitForServerInfos()
	{
		print("Waiting for serverInfos");
		//On attend tant qu'on a pas de valeur

		while (serverIp["value"].Value<String>() == "")
		{
		yield return new WaitForSeconds(.1f);

		}
		yield return null;
		print("Connecting to " + serverIp["value"].Value<String>());
		string address = "tcp://" + serverIp["value"].Value<String>() + ":5555";
		try
		{
			print(address);
			socket = new RequestSocket(address);


			//Initialise the Queue and start waiting for requests
			Task t = new Task(async () => corProcessRequests());
			t.Start();

		}
		catch (Exception e)
		{
			Debug.LogError("Couldn't connect to server... " + e);
			socket = null;
			socketServer = null;
		}

	}
	public void corProcessRequests()
	{
		bool failed = false;
		while (true)
		{
			//On attend tant que la queue est vide
			while (RequestQueue.Count == 0)
			{
				Thread.Sleep(10);
			}
			var jo = RequestQueue.Dequeue();

			print("Dequeuing " + jo.ToStringDebug());
			if (jo != null)
			{
				//Envoie la requête au serveur


				if (socket != null)
				{
					if (!failed)
					{
						//socket.TrySendFrame(JsonUtility.ToJson(jo));
						socket.TrySendFrame(JsonConvert.SerializeObject(jo));
					}
					string msg;
					if (!socket.TryReceiveFrameString(TimeSpan.FromSeconds(2), out msg))
					{
						failed = true;
						print("failed");
						Thread.Sleep(300);
						RequestQueue.Enqueue(jo);
					}
					else
					{
						failed = false;
					}

				}
			}
			print("Pending Requests " + RequestQueue.Count);
			Thread.Sleep(10);

		}
	}

	public void ProcessResponses()
	{
		while (true)
		{
			//On attend une requête du serveur
			string serverRequest = "";
			serverRequest = socketServer.ReceiveFrameString();


			socketServer.TrySendFrameEmpty();
			if (serverRequest != null && serverRequest[0] == '{')
			{
				try
				{
					var data = (JObject)JsonConvert.DeserializeObject(serverRequest);
					JSONObject jo = new JSONObject(data["id"], data["value"]);
					print("Recieved data with id :" + jo["id"] + "Value : " + jo["value"]);
					if (JSONObjects.ContainsKey(jo["id"].ToString()))
					{
						JSONObjects[jo["id"].ToString()].changeValue(jo["value"]);

					}
					else
					{
						JSONObjects[jo["id"].ToString()] = jo;

					}
				}
				catch (Exception e)
				{
					Debug.LogError(e);
				}
				//on enqueue la réponse 
			}

		}
	}
	/*
	public void sendText()
    {
		outMsg = TextIF.text;
		if(socket != null)
        { 
			if (!string.Equals(outMsg, ""))
			{
                if (!socket.TrySendFrame(outMsg))
                {
					Debug.LogError("Couldn't send message to server");
                }
                else
                {
					Debug.Log("Send: " + outMsg);
				}
				outMsg = "";

				string msg;
				if (socket.TryReceiveFrameString(TimeSpan.FromSeconds(3), out msg))
				{
					inMsg = msg;
				}
			} 
		}
    }
	*/

	public int numRequests;
	public void sendJSON()
	{


		JSONObject jsonFloat = new JSONObject("bouleLevel", .005f);
		//print(JsonUtility.ToJson(jsonFloat));
		//print(JsonUtility.ToJson(jsonBool));
		string s = "";
		//Enqueue les valeurs
		for (int i = 0; i < numRequests; i++)
		{
			JSONObject jsonString = new JSONObject("correctCupboardCode", "bonjour");
			JSONObject jsonInt = new JSONObject("jsonInt" + i.ToString(), 200);
			JSONObject jsonfloat = new JSONObject("jsonfloat" + i.ToString(), (float)i / 10);
			JSONObject jsonBool = new JSONObject("jsonbool" + i.ToString(), false);

			//RequestQueue.Enqueue(jsonString);
			RequestQueue.Enqueue(jsonInt);
			//RequestQueue.Enqueue(jsonBool);
			//RequestQueue.Enqueue(jsonfloat);
			//if (socket != null)
			//{
			//	s += i.ToString(); 
			//	socket.TrySendFrame(s);
			//	string msg;
			//	socket.ReceiveFrameString();

			//}
		}
	}
}
