using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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
        };
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
            "Fix Front: "+ kinectGameManagerScript.fixFront+ "\n" +
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
}
