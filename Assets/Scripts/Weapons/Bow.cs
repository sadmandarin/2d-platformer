using UnityEngine;

/// <summary>
/// Описание оружия лука
/// </summary>
[CreateAssetMenu(fileName = "Bow", menuName = "ScriptableObjects/Weapons/Bow", order = 1)]
public class Bow : WeaponsBase
{
    public float ProjectileRange;
    public float ProjectileSpeed;
    public GameObject ProjectilePrefab;

    public override void QuickAttack(Transform attackpoint, MonoBehaviour owner)
    {
        GameObject projectile = Instantiate(ProjectilePrefab, attackpoint.position, owner.transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = owner.transform.right * ProjectileSpeed;

        Projectile proj = projectile.GetComponent<Projectile>();
        proj.SetProjectile(QuickAttackDamage, ProjectileRange);
    }

    public override void StrongAttack(Transform attackPoint, MonoBehaviour owner)
    {
        
    }
}
