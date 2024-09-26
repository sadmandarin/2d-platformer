using UnityEngine;

/// <summary>
/// Срела арбалета
/// </summary>
public class Projectile : MonoBehaviour
{
    public int Damage;
    public float FlyRange;

    private Vector3 _startPos;

    private void OnEnable()
    {
        _startPos = transform.position; 
    }

    private void Update()
    {
        float distanceTraveled = Mathf.Abs(transform.position.x - _startPos.x);

        if (distanceTraveled >= FlyRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DefaultEnemy>())
            collision.gameObject.GetComponent<DefaultEnemy>().TakeDamage(Damage);
        else if (collision.gameObject.GetComponent<BossBase>())
            collision.gameObject.GetComponent<BossBase>().TakeDamage(Damage, true);
    }

    public void SetProjectile(int damage, float range)
    {
        Damage = damage;
        FlyRange = range;
    }

}
