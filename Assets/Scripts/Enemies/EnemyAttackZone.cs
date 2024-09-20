using UnityEngine;

/// <summary>
/// �����, �������������� ���� ������ � ���� ����� �����
/// </summary>
public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

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
