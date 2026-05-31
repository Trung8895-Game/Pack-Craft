using NUnit.Framework;
using UnityEngine;

public class CraftingServiceTests
{
    [Test]
    public void WoodAndStone_CraftAxe()
    {
        // Arrange

        ItemDefinition wood =
            ScriptableObject.CreateInstance<ItemDefinition>();

        ItemDefinition stone =
            ScriptableObject.CreateInstance<ItemDefinition>();

        ItemDefinition axe =
            ScriptableObject.CreateInstance<ItemDefinition>();

        RecipeDefinition recipe =
            ScriptableObject.CreateInstance<RecipeDefinition>();

        recipe.ItemA = wood;
        recipe.ItemB = stone;
        recipe.Result = axe;

        CraftingDatabase database =
            ScriptableObject.CreateInstance<CraftingDatabase>();

        database.Recipes.Add(recipe);

        CraftingService service =
            new CraftingService(database);

        ItemInstance itemA =
            new ItemInstance
            {
                Definition = wood
            };

        ItemInstance itemB =
            new ItemInstance
            {
                Definition = stone
            };

        itemA.Initialize();
        itemB.Initialize();

        // Act

        ItemDefinition result =
            service.TryCraft(
                itemA,
                itemB);

        // Assert

        Assert.AreEqual(
            axe,
            result);
    }

    [Test]
    public void InvalidRecipe_ReturnsNull()
    {
        ItemDefinition wood =
            ScriptableObject.CreateInstance<ItemDefinition>();

        ItemDefinition stone =
            ScriptableObject.CreateInstance<ItemDefinition>();

        CraftingDatabase database =
            ScriptableObject.CreateInstance<CraftingDatabase>();

        CraftingService service =
            new CraftingService(database);

        ItemInstance itemA =
            new ItemInstance
            {
                Definition = wood
            };

        ItemInstance itemB =
            new ItemInstance
            {
                Definition = stone
            };

        itemA.Initialize();
        itemB.Initialize();

        ItemDefinition result =
            service.TryCraft(
                itemA,
                itemB);

        Assert.IsNull(result);
    }

    [Test]
    public void Recipe_IsOrderIndependent()
{
    // Arrange

    ItemDefinition wood =
        ScriptableObject.CreateInstance<ItemDefinition>();

    ItemDefinition stone =
        ScriptableObject.CreateInstance<ItemDefinition>();

    ItemDefinition axe =
        ScriptableObject.CreateInstance<ItemDefinition>();

    RecipeDefinition recipe =
        ScriptableObject.CreateInstance<RecipeDefinition>();

    recipe.ItemA = wood;
    recipe.ItemB = stone;
    recipe.Result = axe;

    CraftingDatabase database =
        ScriptableObject.CreateInstance<CraftingDatabase>();

    database.Recipes.Add(recipe);

    CraftingService service =
        new CraftingService(database);

    ItemInstance stoneItem =
        new ItemInstance
        {
            Definition = stone
        };

    ItemInstance woodItem =
        new ItemInstance
        {
            Definition = wood
        };

    stoneItem.Initialize();
    woodItem.Initialize();

    // Act

    ItemDefinition result =
        service.TryCraft(
            stoneItem,
            woodItem);

    // Assert

    Assert.AreEqual(
        axe,
        result);
}
}