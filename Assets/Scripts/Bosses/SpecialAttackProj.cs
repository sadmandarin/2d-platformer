using UnityEngine;

[RequireComponent (typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class SpecialAttackProj : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistance;

    private Vector2 _direction;
    private Vector2 _startPos;
    private Rigidbody2D _rb;
    private int _damage;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

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
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
