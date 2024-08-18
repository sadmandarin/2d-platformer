using UnityEngine;

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
        if (collision.gameObject.GetComponent<Enemy>())
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
    }

    public void SetProjectile(int damage, float range)
    {
        Damage = damage;
        FlyRange = range;
    }

}
