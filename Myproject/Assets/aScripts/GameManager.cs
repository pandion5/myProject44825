using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO.Ports;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private Dictionary<KeyCode, Action> keyDictionary;
    private bool isPause = false;

    public KinectGameManager kinectGameManagerScript;
    public KinectWalk kinectWalkScript;
    public UIManager uiManagerScript;

    public GameObject debugUI;
    public GameObject player;

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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    //외부 참조
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void ChangeScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public void MovePlayer(Transform transform)
    {
        player.transform.position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            {KeyCode.F4,  KeyDown_F4},
            {KeyCode.Space,  KeyDown_Space},
            {KeyCode.UpArrow,  KeyDown_UpArrow},
            {KeyCode.DownArrow,  KeyDown_DownArrow},
            {KeyCode.Escape,  KeyDown_Esc},
            {KeyCode.Delete,  KeyDown_Delete},
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

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            foreach(var dic in keyDictionary)
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
        if (!serial.IsOpen)
        {
            serial.Open();
        }
    }

    private void LateUpdate()
    {
        uiManagerScript.SetDebugText(TextCheck());
    }

    string TextCheck()
    {
        string s = 
            "DEBUG\n\n" +
            "< Kinect >\n" +
            "Is Walking: " + kinectWalkScript.isMoving + "\n" +
            "Step Count : "+ kinectGameManagerScript.deltaStep + "\n" +
            "Fix Front: " + kinectGameManagerScript.fixFront+ "\n" +
            "Rotation Limit : ±"+ kinectGameManagerScript.rotationLimit+ "\n\n" +
            "<Test>\n" +
            "Test Order: 1 > 2 > 3 > 4\n\n" +
            "< Key Setting >\n" +
            "Debug Text: F4\n" +
            "Fix Front: Space\n" +
            "Up Limit: ↑\n" +
            "Down Limit : ↓\n" +
            "Pause / Resume : Esc\n";
        return s;
    }
    void KeyDown_F4()
    {
        debugUI.SetActive(!debugUI.activeSelf);
    }
    void KeyDown_Space()
    {
        kinectGameManagerScript.FixFront();
    }

    void KeyDown_UpArrow()
    {
        kinectGameManagerScript.rotationLimit++;
    }
    void KeyDown_DownArrow()
    {
        kinectGameManagerScript.rotationLimit--;
    }
    void KeyDown_Esc()
    {
        if (!isPause)
        {
            Time.timeScale = 0;
            isPause = true;
        }
        else
        {
            Time.timeScale = 1;
            isPause = false;
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("작동종료");
        fanvalue = "5";
        serial.Write(fanvalue);
    }
}
