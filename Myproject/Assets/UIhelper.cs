using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIhelper : MonoBehaviour
{
    public TextHelper textHelper;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.debugUI = this.gameObject;
        textHelper.HelpTMP_txt();
        DontDestroyOnLoad(this);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
