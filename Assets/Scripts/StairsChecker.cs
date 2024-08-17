using UnityEngine;

public class StairsChecker : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

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
