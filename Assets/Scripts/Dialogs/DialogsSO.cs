using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogs", menuName = "ScriptableObjects/Dialogs/Dialog", order = 0)]
public class DialogsSO : ScriptableObject
{
    [Tooltip("—юда вписывать реплики персонажей, если выбираема€ реплика нашего персонажа, заполнить им€, после реплики оставить пустой")]
    public List<Speech> Dialogs;
}

[System.Serializable]
public struct Speech
{
    [Tooltip("»м€ персонажа, который говорит")]
    public string SpeakerName;

    [Tooltip("≈го реплика")]
    public string Line;

    [Tooltip("≈сли есть выбор ответа, укажите варианты")]
    public bool HasChoices;

    [Tooltip("¬арианты ответа на реплику")]
    public List<string> Choices;

    [Tooltip("—ледующие диалоги в зависимости от выбранного ответа")]
    public List<DialogsSO> NextDialogs;
}
