using UnityEngine;

/// <summary>
/// Класс, обрабатывающий вход игрока в зону атаки врага
/// </summary>
public class EnemyAttackZone : MonoBehaviour
{
    private EnemyBase _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            _enemy.SetAttackState(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            _enemy.SetAttackState(false);
    }
}
