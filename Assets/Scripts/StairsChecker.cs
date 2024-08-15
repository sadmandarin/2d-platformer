using UnityEngine;

public class StairsChecker : MonoBehaviour
{
    private Player _player;
    private LayerMask _stairLayer = 7;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _stairLayer )
        {
            _player.IsTouchedStairs(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _stairLayer)
        {
            _player.IsTouchedStairs(false);
        }
    }
}
