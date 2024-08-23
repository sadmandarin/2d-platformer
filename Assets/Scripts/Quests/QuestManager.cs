using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<QuestsBase> _activeQuests = new List<QuestsBase>();

    public void AddQuestToQuestList(QuestsBase quest)
    {
        _activeQuests.Add(quest);
        quest.StartQuest();
    }

    public void CompleteQuest(QuestsBase quest)
    {
        if (_activeQuests.Contains(quest))
        {
            quest.CompleteQuest();
            _activeQuests.Remove(quest);
        }
    }

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
    
