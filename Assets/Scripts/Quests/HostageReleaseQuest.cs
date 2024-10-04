using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HostageReleaseQuest", menuName = "ScriptableObjects/Quests/HostageReleaseQuest", order = 1)]
public class HostageReleaseQuest : QuestsBase
{
    [SerializeField] private GameObject[] _enemyToSpawn;
    [SerializeField] private GameObject _hostageToRelease;
    [SerializeField] private GameObject _hostage;
    private GameObject _friendlyNPC;
    [SerializeField] private GameObject _questTriggerZone;
    [SerializeField] private int _timeForQuest;

    public List<GameObject> SpawnedEnemies;

    public override void CompleteQuest()
    {
        Destroy(_friendlyNPC);
        Instantiate(_hostage);
        IsCompleted = true;
    }

    public override void StartQuest()
    {
        IsQuestActiveNow = true;

        for (int i = 0; i < 3; i++)
        {
            GameObject enemy =  Instantiate(_enemyToSpawn[i]);
            SpawnedEnemies.Add(enemy);
        }

        _friendlyNPC = Instantiate(_hostageToRelease);
    }

    private IEnumerator QuestTimer()
    {
        yield return new WaitForSeconds(_timeForQuest);

        Destroy(_friendlyNPC);
    }

}
