using UnityEngine;

public abstract class QuestsBase : ScriptableObject
{
    public string QuestName;
    public string QuestDescription;
    public bool IsQuestActiveNow;
    public bool IsCompleted;

    public abstract void StartQuest();
    public abstract void CompleteQuest();
}
