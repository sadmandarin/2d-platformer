using UnityEngine;

/// <summary>
/// Проверка касания лестниц
/// </summary>
public class StairsChecker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StairsZone>())
        {
            _player.IsTouchedStairs(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StairsZone>())
        {
            _player.IsTouchedStairs(false);
        }
    }
}
