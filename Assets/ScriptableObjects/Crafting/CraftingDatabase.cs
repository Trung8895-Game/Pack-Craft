using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CraftingDatabase",
    menuName = "Inventory/Crafting Database")]
public class CraftingDatabase : ScriptableObject
{
   public List<RecipeDefinition> Recipes =
    new List<RecipeDefinition>();
}