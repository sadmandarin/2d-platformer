using UnityEngine;

/// <summary>
/// �����, ����������� ��������� ����������� ����� ������� �����
/// </summary>
[RequireComponent (typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class EnemyLongRangeAttackArrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _criticalDamageMultiplayer;

    private Vector2 _direction;
    private Vector2 _startPos;
    private Rigidbody2D _rb;
    private int _damage;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ��������� ���������� �����
    /// </summary>
    /// <param name="direction">����������� �����</param>
    /// <param name="damage">����</param>
    public void Initialize(Vector2 direction, int damage)
    {
        _direction = direction;
        _startPos = transform.position;
        _damage = damage;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_direction.x * _speed, _rb.velocity.y);

        if (Vector2.Distance(_startPos, transform.position) > _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (!player.IsRolling)
            {
                var chanceForBonus = Random.value;

                if (chanceForBonus < 0.05f)
                {
                    player.TakeDamage(_damage * _criticalDamageMultiplayer, transform);
                }

                else if (chanceForBonus < 0.1f)
                {
                    player.TakeDamage(_damage, transform);
                    player.SetStunState(0.3f);
                }
                Destroy(gameObject);
            }
        }
    }
}
