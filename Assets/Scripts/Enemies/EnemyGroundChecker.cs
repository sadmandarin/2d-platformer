using UnityEngine;

/// <summary>
///  ласс, провер€ющий касание земли
/// </summary>
public class EnemyGroundChecker : MonoBehaviour
{
    [SerializeField] private EnemyBase _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<DefaultEnemy>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _enemy.GroundChecker(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _enemy.GroundChecker(false);
        }
    }
}

