using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotesTorageSO", menuName = "ScriptableObjects/Notes/NotesStorage")]
public class NotesStorage : ScriptableObject
{
    public List<Note> Notes;
}
