using NUnit.Framework;
using UnityEngine;

public class PlacementValidatorTests
{
    [Test]
    public void CanPlace_ValidPosition_ReturnsTrue()
    {
        InventoryGrid grid =
            new InventoryGrid(6, 8);

        PlacementValidator validator =
            new PlacementValidator(grid);

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
                Definition = definition,
                Rotation = RotationState.None
            };

        item.Initialize();

        bool result =
            validator.CanPlace(
                item,
                Vector2Int.zero);

        Assert.IsTrue(result);
    }

    [Test]
    public void CanPlace_OutOfBounds_ReturnsFalse()
    {
        InventoryGrid grid =
            new InventoryGrid(6, 8);

        PlacementValidator validator =
            new PlacementValidator(grid);

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

        bool result =
            validator.CanPlace(
                item,
                new Vector2Int(100, 100));

        Assert.IsFalse(result);
    }

    [Test]
    public void CanPlace_OverlappingItem_ReturnsFalse()
    {
        InventoryGrid grid =
            new InventoryGrid(6, 8);

        PlacementValidator validator =
            new PlacementValidator(grid);

        ItemDefinition definition =
            ScriptableObject.CreateInstance<ItemDefinition>();

        definition.Shape =
            new[]
            {
                Vector2Int.zero
            };

        ItemInstance item1 =
            new ItemInstance
            {
                Definition = definition
            };

        ItemInstance item2 =
            new ItemInstance
            {
                Definition = definition
            };

        item1.Initialize();
        item2.Initialize();

        grid.PlaceItem(
            item1,
            Vector2Int.zero);

        bool result =
            validator.CanPlace(
                item2,
                Vector2Int.zero);

        Assert.IsFalse(result);
    }
}