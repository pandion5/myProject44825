using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipTutorial : MonoBehaviour
{
    public int idx = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameManager.Instance.ChangeScene(idx);
        }
    }
}
