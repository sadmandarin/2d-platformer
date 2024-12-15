using UnityEngine;

[CreateAssetMenu(fileName = "NotesTorageSO", menuName = "ScriptableObjects/Notes/Note")]
public class Note : ScriptableObject
{
    public string Name;
    public string NoteText;
    public bool IsReaded;
}
