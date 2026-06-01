using UnityEngine;

[CreateAssetMenu(
    fileName = "GoalDefinition",
    menuName = "Inventory/Goal Definition")]
public class GoalDefinition : ScriptableObject
{
    public ItemDefinition TargetItem;

    public int RequiredAmount;
}