using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ��������������� �� ������
/// </summary>
public class WallChecker : MonoBehaviour
{
    [SerializeField] private Player _player;

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
