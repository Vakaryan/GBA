using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class BCIControlGBA : MonoBehaviour
{
    private UdpClient udpServer;
    public GameObject player;
    private Vector3 tempPos;
    private Thread t;
    public float movementSpeed;
    private long lastSend;
    private IPEndPoint remoteEP;
    private float[] transformPosition = new float[3];
    public float currentFocus = 0f;
    public float previousFocus = 0f;
    public int focusLevel = 0;

    public static BCIControlGBA instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    void Start()
    {
        
        udpServer = new UdpClient(49295);
        remoteEP = new IPEndPoint(IPAddress.Any, 49295);

        t = new Thread(() => {
            while (true)
            {
                this.receiveData();
            }
        });
        t.Start();
    }

    void Update()
    {
        /*
        transform.position = new Vector3(transformPosition[0], transformPosition[1], transformPosition[2]);
        if (transformPosition[1] > 10)
            while (true)
            {
                transformPosition[1] = 0;
            }
        */


        StartCoroutine(DataUpdate());
    }

    IEnumerator DataUpdate()
    {
        yield return new WaitForSeconds(1f);
        currentFocus = transformPosition[1];
        int focusUpdate = 0;
        if (currentFocus < previousFocus)
        {
            focusUpdate = -2;
        }
        else
        {
            focusUpdate = 1;
        }
        focusLevel = Mathf.Clamp(focusLevel + focusUpdate, 0, 100);
        previousFocus = currentFocus;
        Debug.Log("Focus update " + focusUpdate);
        Debug.Log("Focus level " + focusLevel);
        Debug.Log("Current focus " + currentFocus);
        Debug.Log("Previous focus " + previousFocus);
        p28.UIManager.Instance.DisplayFocus(focusLevel);
        yield return new WaitForSeconds(1f);
    }

    private void OnApplicationQuit()
    {
        udpServer.Close();
        t.Abort();
    }

    private void receiveData()
    {
        byte[] data = udpServer.Receive(ref remoteEP);
        if (data.Length > 0.006)
        {
            var str = System.Text.Encoding.Default.GetString(data, 0, data.Length);
            Debug.Log("Received Data" + str);
            string[] messageParts = str.Split(',');
            transformPosition[1] = float.Parse(messageParts[0]) + float.Parse(messageParts[1]) + float.Parse(messageParts[2]);
            Debug.Log(messageParts[0] + " " + messageParts[1] + " " + messageParts[2]);
        }
    }
}