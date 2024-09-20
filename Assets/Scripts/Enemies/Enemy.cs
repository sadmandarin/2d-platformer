using System.Collections;
using UnityEngine;

/// <summary>
/// Класс с основными характеристиками врага
/// </summary>
public class Enemy : MonoBehaviour
{
    private float _hp = 100;
    [SerializeField] private int _damage;
    [SerializeField] private bool _isOnGround;
    [SerializeField] private int _isMoving;
    private bool _isObstacleAhead;
    private bool _canAttack;
    private bool _isOnStairs;
    private bool _isDead = false;
    [SerializeField] private bool _isPlayerDetected;
    private Transform _playerTransform;
    private Animator _animator;

    public float Hp { get { return _hp; } private set { _hp = value; } }
    public int Damage { get { return _damage; } private set { _damage = value; } }
    public bool IsOnGround { get { return _isOnGround; } private set { _isOnGround = value; } }
    public int IsMoving
    {
        get 
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
        }
    }
    public bool CanAttack
    {
        get { return _canAttack; }
        private set
        {
            _canAttack = value;
        }
    }
    public bool IsObstacleAhead { get { return _isObstacleAhead; } private set { _isObstacleAhead = value; } }
    public bool IsDead { get { return _isDead; } private set { _isDead = value; } }
    public bool IsOnStairs { get { return _isOnStairs; } private set { _isOnStairs = value; } }
    public bool IsPlayerDetected { get { return _isPlayerDetected; } private set { _isPlayerDetected = value; } }
    public Transform PlayerTransform { get { return _playerTransform; } private set { _playerTransform = value; } }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        IsOnStairs = false;
    }

    /// <summary>
    /// Обнаружение игрока
    /// </summary>
    /// <param name="detect"></param>
    /// <returns></returns>
    public bool SetPlayerDetection(bool detect)
    {
        return IsPlayerDetected = detect;
    }

    /// <summary>
    /// Установка позиции игрока для преследования
    /// </summary>
    /// <param name="playerTransform">Transform игрока</param>
    public void SetPlayerTransform(Transform playerTransform)
    {
        PlayerTransform = playerTransform;
    }

    /// <summary>
    /// Установка флага возможности атаки
    /// </summary>
    /// <param name="canAttack"></param>
    /// <returns></returns>
    public bool SetAttackState(bool canAttack)
    {
        return CanAttack = canAttack;
    }

    /// <summary>
    /// Анимация атаки
    /// </summary>
    public void AttackAnim()
    {
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// Проверка касания триггера лестницы
    /// </summary>
    /// <param name="isTouchedStairs"></param>
    public void IsTouchedStairs(bool isTouchedStairs)
    {
        IsOnStairs = isTouchedStairs;
    }

    /// <summary>
    /// Установка состояния движения
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public int SetMovingState(int state)
    {
        return IsMoving = state;
    }

    /// <summary>
    /// Установка флага соприкосновения с землей
    /// </summary>
    /// <param name="touchedGround"></param>
    public void GroundChecker(bool touchedGround)
    {
        IsOnGround = touchedGround;
    }

    /// <summary>
    /// Нанесение урона игроком
    /// </summary>
    /// <param name="damage">Количество нанесенного урона</param>
    public void TakeDamage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0 && !_isDead)
        {
            Die();
        }

        Debug.Log($"Получено урона врагом: {damage}");
        Debug.Log($"Осталось HP у врага: {Hp}");
    }

    /// <summary>
    /// Смерть противника
    /// </summary>
    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Death");

        StartCoroutine(DestroyEnemyOnDeath());
    }

    /// <summary>
    /// Удаление врага с игрового поля через заданный интервал
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyEnemyOnDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
