using UnityEngine;

[CreateAssetMenu(
    fileName = "LevelDefinition",
    menuName = "Inventory/Level Definition")]
public class LevelDefinition : ScriptableObject
{
    public int LevelIndex;

    public string SceneName;

    public LevelGoalDefinition Goals;
}