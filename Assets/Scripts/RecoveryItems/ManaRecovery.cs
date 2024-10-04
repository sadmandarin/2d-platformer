using UnityEngine;

[CreateAssetMenu(fileName = "ManaRecovery", menuName = "ScriptableObjects/RecoveryItem/ManaRecovery", order = 1)]
public class ManaRecovery : RecoveryItemBase
{
    [SerializeField] private int _maxLevel = 2;

    private void OnEnable()
    {
        if (ItemLevel == 1)
        {
            ItemCount = 1;
            AmountOfRecovery = 15;
        }
    }

    public override void IncreaseItemLevel()
    {
        if (ItemLevel < _maxLevel)
        {
            ItemLevel++;

            if (ItemLevel == 2)
            {
                ItemCount = 3;
                AmountOfRecovery = 30;
            }
        }
    }
}
