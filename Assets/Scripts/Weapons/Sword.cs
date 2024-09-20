using UnityEngine;

/// <summary>
/// Описание меча
/// </summary>
[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/Weapons/Sword", order = 1)]
public class Sword : WeaponsBase
{
    public float AttackRange;

    public override void QuickAttack(Transform attackpoint, MonoBehaviour owner)
    {
        BaseAttack(attackpoint, QuickAttackDamage);
    }

    public override void StrongAttack(Transform attackPoint, MonoBehaviour owner)
    {
        BaseAttack(attackPoint, StrongAttackDamage);
    }

    void BaseAttack(Transform attackPoint, int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>())
                enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        Debug.Log("Attack by Sword " + damage);
    }
}
