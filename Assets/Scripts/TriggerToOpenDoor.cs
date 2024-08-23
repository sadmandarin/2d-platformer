using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToOpenDoor : MonoBehaviour
{
    public GameObject Door;
    private bool _canOpenTheDoor;
    private bool _isDoorOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
