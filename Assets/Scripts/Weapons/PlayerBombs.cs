using System.Collections;
using UnityEngine;

public class PlayerBombs : ThrowingWeaponBase
{
    private bool _isOnGround = false;

    public override IEnumerator ThrowingTrajectory(int direction, Vector2 attackPoint)
    {
        float gravity = -9.8f; // ���� ����������
        float velocityX = 7.0f * direction; // ��������� �������� �� ��� X
        float velocityY = 4.0f; // ��������� �������� �� ��� Y (���������� ������ ��������)


        while (!_isOnGround)
        {
            // ����������� ��������� �� ��� X �������
            float x = transform.position.x + velocityX * Time.deltaTime;

            // ������������ ����� �������� �� ��� Y � ������ ����������
            velocityY += gravity * Time.deltaTime;

            // ����������� ��������� �� ��� Y � ����������� �� ��������
            float y = transform.position.y + velocityY * Time.deltaTime;

            // ��������� ������� �������
            transform.position = new Vector2(x, y);

            // ���� �� ���������� �����
            yield return null;
        }

        // ���� ������ ������ �����, ��������� ��� �� ������ �����
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
        Debug.Log("�����!!!!!!!!!!!!!!!!");

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
