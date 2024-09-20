using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Квест менеджер, управляет текущими активными квестами
/// </summary>
public class QuestManager : MonoBehaviour
{
    private List<QuestsBase> _activeQuests = new List<QuestsBase>();

    /// <summary>
    /// Добавление квеста в список активных
    /// </summary>
    /// <param name="quest">Квест</param>
    public void AddQuestToQuestList(QuestsBase quest)
    {
        _activeQuests.Add(quest);
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
}
    
