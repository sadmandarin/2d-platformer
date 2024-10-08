using UnityEngine;

/// <summary>
/// �������� ������� �����
/// </summary>
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _player.GroundChecker(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _player.GroundChecker(false);
        }
    }
}
