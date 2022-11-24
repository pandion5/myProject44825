using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNext : MonoBehaviour
{
    public Transform nextPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = nextPosition.position;
        }
    }
}
