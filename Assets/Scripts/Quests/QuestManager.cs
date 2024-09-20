using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��������, ��������� �������� ��������� ��������
/// </summary>
public class QuestManager : MonoBehaviour
{
    private List<QuestsBase> _activeQuests = new List<QuestsBase>();

    /// <summary>
    /// ���������� ������ � ������ ��������
    /// </summary>
    /// <param name="quest">�����</param>
    public void AddQuestToQuestList(QuestsBase quest)
    {
        _activeQuests.Add(quest);
        quest.StartQuest();
    }

    /// <summary>
    /// ���������� ������, �������� ��� �� ������ ��������
    /// </summary>
    /// <param name="quest">�����</param>
    public void CompleteQuest(QuestsBase quest)
    {
        if (_activeQuests.Contains(quest))
        {
            quest.CompleteQuest();
            _activeQuests.Remove(quest);
        }
    }

    /// <summary>
    /// ������ �������� ��� ������
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
    
