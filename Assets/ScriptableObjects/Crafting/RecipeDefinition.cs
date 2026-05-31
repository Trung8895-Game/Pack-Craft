using UnityEngine;

[CreateAssetMenu(
    fileName = "Recipe",
    menuName = "Inventory/Recipe")]
public class RecipeDefinition : ScriptableObject
{
    public ItemDefinition ItemA;

    public ItemDefinition ItemB;

    public ItemDefinition Result;
}