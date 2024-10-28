using UnityEngine;

public class RopeDetect : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rope>())
        {
            if (!_player.IsOnRope)
            {
                _player.AttachToRope(collision.attachedRigidbody, collision.gameObject.GetComponent<Rope>().RopeAttachPoint);
            }
        }
    }
}
