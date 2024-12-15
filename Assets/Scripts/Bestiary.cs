using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bestiary", menuName = "ScriptableObjects/Bestiary/Bestiary", order = 1)]
public class Bestiary : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public bool IsReaded;

}
