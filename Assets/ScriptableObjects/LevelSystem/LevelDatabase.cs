using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "LevelDatabase",
    menuName = "Inventory/Level Database")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelDefinition> Levels =
        new();
}