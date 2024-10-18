using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovingPlatform : MonoBehaviour
{
    [Tooltip("Крайняя левая позиция платформы")]
    [SerializeField] private Vector2 _leftPosition;

    [Tooltip("Крайняя правая позиция платформы")]
    [SerializeField] private Vector2 _rightPosition;

    [Tooltip("Скорость движения платформы")]
    [SerializeField] private float _speed;

    [Tooltip("Направление движения платформы(left - влево, right - вправо")]
    [SerializeField] private Direction _directionMove;

    private Rigidbody2D _rb;

    private enum Direction
    {
        Left,
        right
    }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();  

        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            if (_directionMove == Direction.Left)
            {
                _rb.velocity = new Vector2(-_speed, _rb.velocity.y);

                if (transform.position.x <= _leftPosition.x)
                {
                    _directionMove = Direction.right;
                }
            }
            else
            {
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);

                if (transform.position.x >= _rightPosition.x)
                {
                    _directionMove = Direction.Left;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
