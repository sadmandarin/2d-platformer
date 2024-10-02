using UnityEngine;

/// <summary>
/// Обнаружение игрока
/// </summary>
public class PlayerDetection : MonoBehaviour
{
    private EnemyBase _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && _enemy.IsPlayerDetected == false)
        {
            _enemy.SetPlayerDetection(true);
            _enemy.SetPlayerTransform(collision.gameObject.transform);
        }
    }
}
