using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextHelper : MonoBehaviour
{
    public void HelpTMP_txt()
    {
        GameManager.Instance.uiManagerScript.txtDebug = this.gameObject.GetComponent<TMP_Text>();
    }

}
