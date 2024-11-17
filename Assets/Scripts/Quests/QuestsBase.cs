using UnityEngine;

/// <summary>
/// Абстрактный класс для квестов
/// </summary>
public abstract class QuestsBase : ScriptableObject
{
    public string QuestName;
    public string QuestDescription;
    public bool IsQuestActiveNow;
    public bool IsCompleted;

    /// <summary>
    /// Старт квеста
    /// </summary>
    public abstract void StartQuest();

    /// <summary>
    /// выполнение квеста
    /// </summary>
    public abstract void CompleteQuest();
    public abstract void QuestFailure();
}
