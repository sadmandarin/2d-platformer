using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogs", menuName = "ScriptableObjects/Dialogs/Dialog", order = 0)]
public class DialogsSO : ScriptableObject
{
    [Tooltip("���� ��������� ������� ����������, ���� ���������� ������� ������ ���������, ��������� ���, ����� ������� �������� ������")]
    public List<Speech> Dialogs;
}

[System.Serializable]
public struct Speech
{
    [Tooltip("��� ���������, ������� �������")]
    public string SpeakerName;

    [Tooltip("��� �������")]
    public string Line;

    [Tooltip("���� ���� ����� ������, ������� ��������")]
    public bool HasChoices;

    [Tooltip("�������� ������ �� �������")]
    public List<string> Choices;

    [Tooltip("��������� ������� � ����������� �� ���������� ������")]
    public List<DialogsSO> NextDialogs;
}
