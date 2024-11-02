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

    private List<GameObject> SpawnedEnemies;

    public override void CompleteQuest()
    {
        Destroy(_friendlyNPC);
        Instantiate(_hostage);
        IsCompleted = true;
    }

    public void QuestFailure()
    {
        Destroy(_friendlyNPC);
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

    // ����������� � ���� QuestEnemy �������� ��� ID ������, ������ � ������� ������� �� ��������
    //������� ���������� �� ������, �� �������� ������ ��������� �����
     private void RemoveEnemyFromList()
    {

    }

    private IEnumerator QuestTimer()
    {
        yield return new WaitForSeconds(_timeForQuest);

        
    }

}
