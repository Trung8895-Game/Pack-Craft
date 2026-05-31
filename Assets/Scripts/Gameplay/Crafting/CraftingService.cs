using UnityEngine;

public class CraftingService
{
    private readonly CraftingDatabase _database;

    public CraftingService(
        CraftingDatabase database)
    {
        _database = database;
    }

    public ItemDefinition TryCraft(
    ItemInstance itemA,
    ItemInstance itemB)
{
    if (_database == null)
        return null;

    if (_database.Recipes == null)
        return null;

    foreach (var recipe in _database.Recipes)
    {
        bool match =
            IsRecipeMatch(
                recipe,
                itemA.Definition,
                itemB.Definition);

        if (match)
        {
            return recipe.Result;
        }
    }

    return null;
}

    private bool IsRecipeMatch(
        RecipeDefinition recipe,
        ItemDefinition a,
        ItemDefinition b)
    {
        return
            (recipe.ItemA == a &&
             recipe.ItemB == b)

            ||

            (recipe.ItemA == b &&
             recipe.ItemB == a);
    }
}