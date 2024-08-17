using UnityEngine;

public class EnemyGroundChecker : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
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

