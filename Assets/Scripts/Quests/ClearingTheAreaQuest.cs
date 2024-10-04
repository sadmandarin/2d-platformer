using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ClearingQuest", menuName = "ScriptableObjects/Quests/ClearingQuest", order = 1)]
public class ClearingTheAreaQuest : QuestsBase
{
    private int _questTimeToLose = 30;
    private GameObject _friendlyNPC;

    [SerializeField] private GameObject _friendlyNPCPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    public override void CompleteQuest()
    {
        throw new System.NotImplementedException();
    }

    public override void StartQuest()
    {
        throw new System.NotImplementedException();
    }
}
