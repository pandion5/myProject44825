using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNow : MonoBehaviour
{
    public GameObject player;
    public Transform[] pos;
    private Dictionary<KeyCode, Action> keyDictionary;

    private void Start()
    {
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            {KeyCode.Keypad1,  KeyDown_Keypad1},
            {KeyCode.Keypad2,  KeyDown_Keypad2},
            {KeyCode.Keypad3,  KeyDown_Keypad3},
            {KeyCode.Keypad4,  KeyDown_Keypad4},
        };

        player = GameObject.Find("Player");

    }
    void KeyDown_Keypad1()
    {
        if (player.CompareTag("Player"))
        {
            player.gameObject.transform.position = pos[0].position;
        }
    }
    void KeyDown_Keypad2()
    {
        if (player.CompareTag("Player"))
        {
            player.gameObject.transform.position = pos[1].position;
        }
    }
    void KeyDown_Keypad3()
    {
        if (player.CompareTag("Player"))
        {
            player.gameObject.transform.position = pos[2].position;
        }
    }
    void KeyDown_Keypad4()
    {
        if (player.CompareTag("Player"))
        {
            player.gameObject.transform.position = pos[3].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
}
