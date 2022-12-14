using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class DemoArduino : MonoBehaviour
{

    public enum PortNumber
    {
        COM1, COM2, COM3, COM4,
        COM5, COM6, COM7, COM8,
        COM9, COM10, COM11, COM12,
        COM13, COM14, COM15, COM16
    }
    public string key = "Delete";

    private SerialPort serial;
    private string fanvalue;
    [SerializeField]
    private PortNumber portNumber = PortNumber.COM5;
    [SerializeField]
    private string baudRate = "9600";

    private Dictionary<KeyCode, Action> keyDictionary;
    private bool isWalking = false;


    // Start is called before the first frame update
    void Start()
    {
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            {KeyCode.Delete,  KeyDown_Delete},
            {KeyCode.KeypadPlus,  KeyDown_UpArrow},
        };

        serial = new SerialPort(portNumber.ToString(), int.Parse(baudRate));
        if (!serial.IsOpen)
        {
            serial.Open();
        }


    }

    void KeyDown_Delete()
    {
        if (fanvalue == "5")
            fanvalue = "4";
        else
            fanvalue = "5";

        serial.Write(fanvalue);
        if (!serial.IsOpen)
        {
            fanvalue = "5";
            serial.Write(fanvalue);
        }
    }

    void KeyDown_UpArrow()
    {
        isWalking = !isWalking;
    }

    private void Update()
    {
        
        if (!serial.IsOpen)
        {
            serial.Open();
        }
        
        if (Input.anyKeyDown)
        {
            foreach (var dic in keyDictionary)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(isWalking)
            transform.Translate(Vector3.forward * Time.deltaTime);
        
    }
    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tree")) // ??????
        {
            Debug.Log("????");
            fanvalue = "4";
            serial.Write(fanvalue);
            if (!serial.IsOpen)
            {
                fanvalue = "5";
                serial.Write(fanvalue);
            }
            /*            servoValue = "u";
                        serial.Write(servoValue);*/
        }
        if (other.gameObject.CompareTag("Flower")) // ????
        {
            fanvalue = "4";
            serial.Write(fanvalue);
            /*            servoValue = "u";
                        serial.Write(servoValue);*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tree")) //??????
        {
            Debug.Log("???? ??");
            fanvalue = "5";
            serial.Write(fanvalue);
        }
        if (other.gameObject.CompareTag("Flower")) //??????
        {
            Debug.Log("???? ??");
            fanvalue = "5";
            serial.Write(fanvalue);
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("????????");
        fanvalue = "5";
        serial.Write(fanvalue);
    }
}