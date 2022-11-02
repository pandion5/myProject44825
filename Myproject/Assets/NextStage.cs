using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    public void MovePlayer(Transform transform)
    {
        this.transform.position = transform.position;
    }
}
