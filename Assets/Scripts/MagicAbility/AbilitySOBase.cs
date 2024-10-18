using System.Collections;
using UnityEngine;

/// <summary>
/// Базовый класс для умений
/// </summary>
public abstract class AbilitySOBase : MonoBehaviour
{
    public string AbilityName;
    public Sprite Icon;
    public string ShortDescription;
    public int Damage;
    public int ManaCost;
    public float Delay;

    public abstract void InitializeStats(Vector2 direction, Vector2 startPos);

    protected abstract IEnumerator StartAttacking();
}
