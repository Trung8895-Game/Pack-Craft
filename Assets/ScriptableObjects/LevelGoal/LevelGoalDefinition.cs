using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "LevelGoal",
    menuName = "Inventory/Level Goal")]
public class LevelGoalDefinition : ScriptableObject
{
    public List<GoalDefinition> Goals =
        new();
}