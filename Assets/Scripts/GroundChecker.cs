using UnityEngine;

/// <summary>
/// Проверка касания земли
/// </summary>
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CheckCloseRangeGround _closeGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            if (!_player.IsOnGround)
            {
                Physics2D.IgnoreLayerCollision(10, collision.gameObject.layer, false);
                Debug.Log("False");
            }
            _player.GroundChecker(true);
            //_player.RemoveLayer(collision.gameObject.layer);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            if (!_player.IsOnGround)
            {
                _player.GroundChecker(true);

                Physics2D.IgnoreLayerCollision(10, collision.gameObject.layer, false);

                Debug.Log("False");
            }
            

            //_player.RemoveLayer(collision.gameObject.layer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            if (_closeGround.GroundLayerMask != collision.gameObject.layer)
            {
                Physics2D.IgnoreLayerCollision(10, collision.gameObject.layer, true);

                Debug.Log("True");
            }

            _player.GroundChecker(false);
            //_player.AddLayer(collision.gameObject.layer);
        }
    }
}
