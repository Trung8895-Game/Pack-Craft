using NUnit.Framework;
using UnityEngine;

public class InventoryGridPlacementTests
{
    [Test]
    public void PlaceItem_OccupiesCell()
    {
        InventoryGrid grid =
            new InventoryGrid(6, 8);

        ItemDefinition definition =
            ScriptableObject.CreateInstance<ItemDefinition>();

        definition.Shape =
            new[]
            {
                Vector2Int.zero
            };

        ItemInstance item =
            new ItemInstance
            {
                Definition = definition
            };

        item.Initialize();

        grid.PlaceItem(
            item,
            Vector2Int.zero);

        Assert.IsTrue(
            grid.IsOccupied(
                Vector2Int.zero));
    }

    [Test]
    public void RemoveItem_FreesCell()
    {
        InventoryGrid grid =
            new InventoryGrid(6, 8);

        ItemDefinition definition =
            ScriptableObject.CreateInstance<ItemDefinition>();

        definition.Shape =
            new[]
            {
                Vector2Int.zero
            };

        ItemInstance item =
            new ItemInstance
            {
                Definition = definition
            };

        item.Initialize();

        grid.PlaceItem(
            item,
            Vector2Int.zero);

        grid.RemoveItem(item);

        Assert.IsFalse(
            grid.IsOccupied(
                Vector2Int.zero));
    }
}