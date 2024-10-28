using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StakqqqeTraps : TrapsBase
{
    private Rigidbody2D _rb;
    private int _speed = 1;
    private Vector3 _startPos;
    private bool _isWorkedOnce = false;

    [SerializeField] private Vector3 _maxPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        TrapAction();

        _isWorkedOnce = false;
        _startPos = transform.position;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isWorkedOnce)
        {
            if (collision.gameObject.GetComponent<Player>())
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(_damage, transform);
                _isWorkedOnce = true;
            }
        }
    }

    protected override void TrapAction()
    {
        StartCoroutine(OpeningTheTrap());
    }

    private IEnumerator OpeningTheTrap()
    {
        while (transform.position.y < _maxPosition.y)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 1 * _speed);

            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(ClosingTheTrap());
    }

    private IEnumerator ClosingTheTrap()
    {
        while (transform.position.y > _maxPosition.y)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -1 * _speed);

            yield return new WaitForSeconds(0.1f);
        }

        transform.position = _startPos;
        gameObject.SetActive(false);
    }
}
