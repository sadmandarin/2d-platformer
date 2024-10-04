using UnityEngine;

[CreateAssetMenu(fileName = "HealthRecovery", menuName = "ScriptableObjects/RecoveryItem/healthRecovery", order = 1)]
public class HealthRecovery : RecoveryItemBase
{
    private int _maxLevel = 3;

    private void OnEnable()
    {
        if (ItemLevel == 1)
        {
            ItemCount = 2;
            AmountOfRecovery = 10;
        }
    }

    public override void IncreaseItemLevel()
    {
        if (ItemLevel < _maxLevel)
        {
            ItemLevel++;

            if (ItemLevel == 2)
            {
                ItemCount = 4;
                AmountOfRecovery = 20;
            }
            else if (ItemLevel == 3)
            {
                ItemCount = 6;
                AmountOfRecovery = 30;
            }
        }
    }
}
