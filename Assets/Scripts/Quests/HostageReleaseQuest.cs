using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "HostageReleaseQuest", menuName = "ScriptableObjects/Quests/HostageReleaseQuest", order = 1)]
public class HostageReleaseQuest : QuestsBase
{
    [SerializeField] private GameObject[] _enemyToSpawn;
    [SerializeField] private Vector2[] _enemyPositions; 
    [SerializeField] private GameObject _hostageToRelease;
    [SerializeField] private GameObject _hostage;
    private GameObject _friendlyNPC;
    [SerializeField] private GameObject _questTriggerZone;
    [SerializeField] private Vector2 _questTriggerZonePosition;
    [SerializeField] private int _timeForQuest;

    private List<EnemyBase> _spawnedEnemies;

    public override void CompleteQuest()
    {
        Debug.Log("Все уничтожены");
        Destroy(_friendlyNPC);
        Instantiate(_hostage);
        IsCompleted = true;
    }

    public override void QuestFailure()
    {
        Destroy(_friendlyNPC);
    }

    [ContextMenu("my method")]
    public override void StartQuest()
    {
        IsQuestActiveNow = true;

        for (int i = 0; i < 1; i++)
        {
            GameObject enemy =  Instantiate(_enemyToSpawn[i], _enemyPositions[i], Quaternion.Euler(0, 180, 0));
            enemy.AddComponent<HostageReleaseQuestEnemy>();
            _spawnedEnemies.Add(enemy.GetComponent<EnemyBase>());
        }

        var trigger = Instantiate(_questTriggerZone, _questTriggerZonePosition, Quaternion.identity);
        trigger.GetComponent<EnterReleaseQuestTrigger>().Init(this);



        //_friendlyNPC = Instantiate(_hostageToRelease);
    }

    // Присваивать в поле QuestEnemy название или ID квеста, искать в списках квестах по названию
    //Удалять противника из списка, по очищению списка завершать квест
     public void RemoveEnemyFromList(EnemyBase enemy)
     {
        Debug.Log("Убран" + enemy);
        if (_spawnedEnemies.Count > 0)
        {
            _spawnedEnemies.Remove(enemy);
        }
        else if (_spawnedEnemies.Count == 0)
        {
            CompleteQuest();
        }
     }

    public IEnumerator QuestTimer()
    {
        yield return new WaitForSeconds(_timeForQuest);

        Debug.Log("Таймер стартовал");

        QuestFailure();
    }

}
