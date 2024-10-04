using UnityEngine;

public abstract class RecoveryItemBase : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public int ItemCount;
    public int ItemLevel = 1;
    public int AmountOfRecovery;

    public abstract void IncreaseItemLevel();
}
