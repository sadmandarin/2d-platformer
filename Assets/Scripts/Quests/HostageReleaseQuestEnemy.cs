using UnityEngine;

public class HostageReleaseQuestEnemy : MonoBehaviour
{
    private EnemyBase _enemy;
    private QuestManager _questManager;


    private void Awake()
    {
        _enemy = GetComponent<EnemyBase>();
        _questManager = FindFirstObjectByType<QuestManager>();
    }

    private void OnDestroy()
    {
        
    }
}
