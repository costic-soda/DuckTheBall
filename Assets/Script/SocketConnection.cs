using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
//using WebSocketSharp;
using System.Text;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using Newtonsoft.Json;
using UnityEngine.Networking;
using TMPro;




public class SocketConnection : MonoBehaviour
{

    // Socket Code 
    bool alive = true;
    //public bool increase = false;
    System.Threading.Thread SocketThread;
    volatile bool keepReading = false;
    //byte[] data;
    //byte[] data2;
    public static ushort pointX = 1, pointY;
    //public static float data;
    public static string message, test;
    public static float temp;
    uint pointP;
    Socket listener;
    Socket handler;

    //Data data;
    WebSocket websocket;

    public TextMeshProUGUI camValuesText;
    public Transform body;
    public class Data
    {
        public string x { get; set; }
        public string y { get; set; }
        //public string camX { get; set; }
        //public string camY { get; set; }
        //public string camZ { get; set; }
        //public string camData { get; set; }

        //public Dictionary<string, Dictionary<string, List<string>>> camData { get; set; }
        //public List<string> camData { get; set; }
        public Dictionary<string, Dictionary<string, List<string>>> camData;
    }
    public class CamData
    {
        public class camValues
        {
            public List<string> NOSE { get; set; }
            public List<string> LEFT_SHOULDER { get; set; }
            public List<string> LEFT_ELBOW { get; set; }
            public List<string> LEFT_WRIST { get; set; }
            public List<string> RIGHT_SHOULDER { get; set; }
            public List<string> RIGHT_ELBOW { get; set; }
            public List<string> RIGHT_WRIST { get; set; }
            public List<string> LEFT_HIP { get; set; }
            public List<string> LEFT_KNEE { get; set; }
            public List<string> LEFT_ANKLE { get; set; }
            public List<string> RIGHT_HIP { get; set; }
            public List<string> RIGHT_KNEE { get; set; }
            public List<string> RIGHT_ANKLE { get; set; }
        }

        public class Root
        {
            public camValues camValues { get; set;}
        }
    }

    // Start is called before the first frame update

    void Start()
    {
        Application.runInBackground = true;
        Begin();

        StartCoroutine(getRequest("http://localhost:5000/mGetGameInfo"));
        StartCoroutine(getRequest("http://localhost:5000/godotReady"));
    }

    async void Begin()
    {
        Data data;


        websocket = new WebSocket("ws://127.0.0.1:5678");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
            Application.Quit();
        };

        websocket.OnMessage += (bytes) =>
        {

            /*
            // getting the message as a string and deserialize the json string
            // also store it in the global variable

            message = System.Text.Encoding.UTF8.GetString(bytes);
            data = JsonConvert.DeserializeObject<Data>(message);

            
            Globals.Variables.matX = data.x;
            Globals.Variables.matY = data.y;
            //Globals.Variables.camData = data.camData;

            //else
            //{
            //    Globals.Variables.camValues.Add(nullString); 
            //}
            camValuesText.text = "MatX: " + Globals.Variables.matX + "MatY: " + Globals.Variables.matY;
            //Debug.Log(Globals.Variables.camData);

            //Globals.Variables.camX = data.camX;
            //Globals.Variables.camY = data.camY; 
            //Globals.Variables.camY = data.camZ;                   
            */



            message = System.Text.Encoding.UTF8.GetString(bytes);

            //data = JsonConvert.DeserializeObject<Data>(message);

            List<string> zero = new List<string>();
            CamData.Root myDeserializedClass = JsonConvert.DeserializeObject<CamData.Root>(message);
            var camValues = myDeserializedClass.camValues;

            //Globals.Variables.NOSE = (camValues.NOSE != null) ? camValues.NOSE : zero;
            Globals.Variables.LEFT_SHOULDER = (camValues.LEFT_SHOULDER != null) ? camValues.LEFT_SHOULDER : zero;
            //Globals.Variables.LEFT_ELBOW = (camValues.LEFT_ELBOW != null) ? camValues.LEFT_ELBOW : zero;
            //Globals.Variables.LEFT_WRIST = (camValues.LEFT_WRIST != null) ? camValues.LEFT_WRIST : zero;
            //Globals.Variables.RIGHT_SHOULDER = (camValues.RIGHT_SHOULDER != null) ? camValues.RIGHT_SHOULDER : zero;
            //Globals.Variables.RIGHT_ELBOW = (camValues.RIGHT_ELBOW != null) ? camValues.RIGHT_ELBOW : zero;
            //Globals.Variables.RIGHT_WRIST = (camValues.RIGHT_WRIST != null) ? camValues.RIGHT_WRIST : zero;
            Globals.Variables.LEFT_HIP = (camValues.LEFT_HIP != null) ? camValues.LEFT_HIP : zero;
            //Globals.Variables.LEFT_KNEE = (camValues.LEFT_KNEE != null) ? camValues.LEFT_KNEE : zero;
            //Globals.Variables.LEFT_ANKLE = (camValues.LEFT_ANKLE != null) ? camValues.LEFT_ANKLE : zero;
            //Globals.Variables.RIGHT_HIP = (camValues.RIGHT_HIP != null) ? camValues.RIGHT_HIP : zero;
            //Globals.Variables.RIGHT_KNEE = (camValues.RIGHT_KNEE != null) ? camValues.RIGHT_KNEE : zero;
            //Globals.Variables.RIGHT_ANKLE = (camValues.RIGHT_ANKLE != null) ? camValues.RIGHT_ANKLE : zero;

            Debug.Log("Brand" + camValues.NOSE[0]);
            camValuesText.text = "NOSE: " + string.Join(", ", Globals.Variables.NOSE) +
            "\nLEFT_SHOULDER: " + string.Join(", ", Globals.Variables.LEFT_SHOULDER) +
            "\nLEFT_HIP: " + string.Join(", ", Globals.Variables.LEFT_HIP);
            //"\nLEFT_ELBOW: " + string.Join(", ", Globals.Variables.RIGHT_ELBOW) +
            //"\nLEFT_WRIST: " + string.Join(", ", Globals.Variables.LEFT_WRIST) +
            //"\nRIGHT_SHOULDER: " + string.Join(", ", Globals.Variables.RIGHT_SHOULDER) +
            //"\nRIGHT_ELBOW: " + string.Join(", ", Globals.Variables.RIGHT_ELBOW) +
            //"\nRIGHT_WRIST: " + string.Join(", ", Globals.Variables.RIGHT_WRIST) +
            //"\nLEFT_KNEE: " + string.Join(", ", Globals.Variables.LEFT_KNEE) +
            //"\nLEFT_HEEL: " + string.Join(", ", Globals.Variables.LEFT_ANKLE) +
            //"\nRIGHT_HIP: " + string.Join(", ", Globals.Variables.RIGHT_HIP) +
            //"\n RIGHT_KNEE: " + string.Join(", ", Globals.Variables.RIGHT_KNEE) +
            //"\n RIGHT_ANKLE" + string.Join(", ", Globals.Variables.RIGHT_ANKLE);

        };

        // Keep sending messages at every 0.3s
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);


        await websocket.Connect();

    }




    void Update()
    {


#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif




    }

    // For sending messages 

    //async void SendWebSocketMessage()
    //{
    //    if (websocket.State == WebSocketState.Open)
    //    {
    //        // Sending bytes
    //        await websocket.Send(new byte[] { 10, 20, 30 });

    //        // Sending plain text
    //        await websocket.SendText("plain text message");
    //    }
    //}

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }

    }
}
