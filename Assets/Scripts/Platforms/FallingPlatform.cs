using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class FallingPlatform : MonoBehaviour
{
    [Tooltip("Скорость, с которой платформа будет падать вниз")]
    [SerializeField] private float _fallSpeed;

    private int _timeToFall = 2;
    private Rigidbody2D _rb;
    private Vector2 _startPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GroundChecker>())
        {
            StartCoroutine(StartFalling());
        }
    }

    private IEnumerator StartFalling()
    {
        yield return new WaitForSeconds(_timeToFall);

        while (true)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_fallSpeed);

            if (transform.position.y <= _startPos.y - 4)
            {
                Destroy(gameObject);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
