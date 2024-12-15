using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BestiaryStorage", menuName = "ScriptableObjects/Bestiary/BestiaryStorage", order = 1)]
public class BestiaryStorage : ScriptableObject
{
    public List<Bestiary> BestiaryList;
}
