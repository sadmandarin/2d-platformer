using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Квест менеджер, управляет текущими активными квестами
/// </summary>
public class QuestManager : MonoBehaviour
{
    public List<QuestsBase> _activeQuests = new List<QuestsBase>();

    private HostageReleaseQuest _releaseHostage = null;

    /// <summary>
    /// Добавление квеста в список активных
    /// </summary>
    /// <param name="quest">Квест</param>
    public void AddQuestToQuestList(QuestsBase quest)
    {
        _activeQuests.Add(quest);

        if (quest is HostageReleaseQuest releaseQuest)
        {
            _releaseHostage = releaseQuest;
        }

        quest.StartQuest();
    }

    /// <summary>
    /// Выполнение квеста, убирание его из списка активных
    /// </summary>
    /// <param name="quest">Квест</param>
    public void CompleteQuest(QuestsBase quest)
    {
        if (_activeQuests.Contains(quest))
        {
            quest.CompleteQuest();
            _activeQuests.Remove(quest);
        }
    }

    /// <summary>
    /// Подбор предмета для квеста
    /// </summary>
    /// <param name="itemName"></param>
    public void CollectItem(string itemName)
    {
        foreach (var quest in _activeQuests)
        {
            if (quest is PickUpItemsQuests fetchQuest)
            {
                fetchQuest.CollectItem(itemName);
            }
        }
    }

    //Hostage Release Quests Methods
    #region
    public void KillEnemy(EnemyBase enemy)
    {
        _releaseHostage.RemoveEnemyFromList(enemy);
    }

    #endregion
}

