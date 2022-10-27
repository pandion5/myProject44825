using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Text txtDebug;
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void SetDebugText(string s)
    {
        txtDebug.SetText(s);
    }
    private void Start()
    {
        GameManager.Instance.uiManagerScript = this;
    }

    void Update()
    {
        
    }
}
