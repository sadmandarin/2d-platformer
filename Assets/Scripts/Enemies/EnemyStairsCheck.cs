using UnityEngine;

/// <summary>
/// ѕроверка соприкосновени€ с лестницей
/// </summary>
public class EnemyStairsCheck : MonoBehaviour
{
    private EnemyBase _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StairsZone>())
        {
            _enemy.IsTouchedStairs(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StairsZone>())
        {
            _enemy.IsTouchedStairs(false);
        }
    }
}
