using UnityEngine;

/// <summary>
/// ����������� ����� ��� �������
/// </summary>
public abstract class QuestsBase : ScriptableObject
{
    public string QuestName;
    public string QuestDescription;
    public bool IsQuestActiveNow;
    public bool IsCompleted;

    /// <summary>
    /// ����� ������
    /// </summary>
    public abstract void StartQuest();

    /// <summary>
    /// ���������� ������
    /// </summary>
    public abstract void CompleteQuest();
    public abstract void QuestFailure();
}
