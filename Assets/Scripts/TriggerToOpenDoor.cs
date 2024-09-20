using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Открытие двери
/// </summary>
public class TriggerToOpenDoor : MonoBehaviour
{
    public GameObject Door;
    private bool _canOpenTheDoor;
    private bool _isDoorOpen;

    private void Start()
    {
        _isDoorOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (_canOpenTheDoor)
                OpenTheDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isDoorOpen && collision.gameObject.GetComponent<Player>())
        {
            Debug.Log("Press E to open the door");
            _canOpenTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isDoorOpen && collision.gameObject.GetComponent<Player>())
        {
            _canOpenTheDoor = false;
        }
    }

    void OpenTheDoor()
    {
        Door.SetActive(false);
        _canOpenTheDoor = false;
        _isDoorOpen = true;
    }
}
