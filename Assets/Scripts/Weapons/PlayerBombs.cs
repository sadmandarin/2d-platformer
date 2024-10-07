using System.Collections;
using UnityEngine;

public class PlayerBombs : ThrowingWeaponBase
{
    private bool _isOnGround = false;

    public override IEnumerator ThrowingTrajectory(int direction, Vector2 attackPoint)
    {
        float gravity = -9.8f; // Сила гравитации
        float velocityX = 7.0f * direction; // Начальная скорость по оси X
        float velocityY = 4.0f; // Начальная скорость по оси Y (регулирует высоту параболы)


        while (!_isOnGround)
        {
            // Увеличиваем положение по оси X линейно
            float x = transform.position.x + velocityX * Time.deltaTime;

            // Рассчитываем новую скорость по оси Y с учетом гравитации
            velocityY += gravity * Time.deltaTime;

            // Увеличиваем положение по оси Y в зависимости от скорости
            float y = transform.position.y + velocityY * Time.deltaTime;

            // Обновляем позицию объекта
            transform.position = new Vector2(x, y);

            // Ждем до следующего кадра
            yield return null;
        }

        // Если объект достиг земли, фиксируем его на высоте земли
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.GetComponent<EnemyBase>() || collision.gameObject.GetComponent<BossBase>())
        {
            Explode();
        }
        
        if (collision.gameObject.GetComponent<Ground>())
        { 
            _isOnGround = true;
            StartCoroutine(ExplodeAfterTheGroundTouch());
        }
    }

    private IEnumerator ExplodeAfterTheGroundTouch()
    {
        yield return new WaitForSeconds(2);

        Explode();
    }

    private void Explode()
    {
        Debug.Log("ВЗРЫВ!!!!!!!!!!!!!!!!");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyBase>())
                enemy.GetComponent<EnemyBase>().TakeDamage(4);
            else if (enemy.GetComponent<BossBase>())
                enemy.GetComponent<BossBase>().TakeDamage(4, true);
        }

        Destroy(gameObject);
    }
}
