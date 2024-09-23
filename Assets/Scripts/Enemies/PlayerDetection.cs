using UnityEngine;

/// <summary>
/// Обнаружение игрока
/// </summary>
public class PlayerDetection : MonoBehaviour
{
    private EnemyBase _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<DefaultEnemy>();
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
