using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOrder : MonoBehaviour
{
    public NextStage player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<NextStage>();
        player.MovePlayer(transform);
    }
}
