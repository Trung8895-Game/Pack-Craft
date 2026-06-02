using UnityEngine;

public class CraftingService
{
    private readonly CraftingDatabase _database;

    public CraftingService(CraftingDatabase database)
    {
        _database = database;
    }

    public ItemDefinition TryCraft(ItemInstance itemA, ItemInstance itemB)
    {   
        if(itemA==null||itemB==null)
        {
            return null;
        }

    Debug.Log("Database: " + _database);
    if (_database == null)
        {
            Debug.Log("Database: " + _database);
            return null;
        }
    

    if (_database.Recipes == null)
        {
            Debug.Log("Recipes: " + _database.Recipes);
            return null;
        }
    

    foreach (var recipe in _database.Recipes)
    {
        bool match = IsRecipeMatch(recipe,itemA.Definition,itemB.Definition);
        Debug.Log("match: " + match);
        if (match)
        {
            return recipe.Result;
        }
    }
    Debug.Log("bi null luon");
    return null;
}

    private bool IsRecipeMatch(RecipeDefinition recipe, ItemDefinition a, ItemDefinition b)
    {
        return (recipe.ItemA.Id == a.Id && recipe.ItemB.Id == b.Id)||(recipe.ItemA.Id == b.Id &&recipe.ItemB.Id == a.Id);
    }
}