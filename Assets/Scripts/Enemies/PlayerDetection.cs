using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && _enemy.IsPlayerDetected == false)
        {
            _enemy.SetPlayerDetection(true);
            _enemy.SetPlayerTransform(collision.gameObject.transform);
        }
    }
}
