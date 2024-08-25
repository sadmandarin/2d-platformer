using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _player.TouchedWall(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Ground>())
        {
            _player.TouchedWall(false);
        }
    }
}
