using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public List<QuestsBase> Quests;
    public bool IsQuestActive;
    public bool IsQuestComplete;

    private void Start()
    {
        IsQuestComplete = Quests[0].IsCompleted;
        IsQuestActive = Quests[0].IsQuestActiveNow;
    }

    public void CompleteQuest()
    {
        IsQuestComplete = true;
        IsQuestActive = false;
        Quests[0].CompleteQuest();
        Quests.Clear();
    }
}
