using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public int idx = 0;

    private void Start()
    {
        GameManager.Instance.MovePlayer(transform);
    }
}
