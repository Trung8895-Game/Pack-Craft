using NUnit.Framework;
using UnityEngine;

public class RotationTests
{
    [Test]
    public void Rotate_ChangesRotationState()
    {
        ItemDefinition definition =
            ScriptableObject.CreateInstance<ItemDefinition>();

        definition.Shape =
            new[]
            {
                Vector2Int.zero,
                new Vector2Int(1,0)
            };

        ItemInstance item =
            new ItemInstance
            {
                Definition = definition,
                Rotation = RotationState.None
            };

        item.Initialize();

        RotationService.Rotate(item);

        Assert.AreEqual(
            RotationState.Right90,
            item.Rotation);
    }

    [Test]
    public void RotatedShape_IsNormalized()
    {
        ItemDefinition definition =
            ScriptableObject.CreateInstance<ItemDefinition>();

        definition.Shape =
            new[]
            {
                new Vector2Int(0,0),
                new Vector2Int(1,0),
                new Vector2Int(0,1)
            };

        ItemInstance item =
            new ItemInstance
            {
                Definition = definition
            };

        item.Initialize();

        RotationService.Rotate(item);

        Vector2Int[] shape =
            item.GetCurrentShape();

        foreach (var cell in shape)
        {
            Assert.GreaterOrEqual(cell.x, 0);
            Assert.GreaterOrEqual(cell.y, 0);
        }
    }
}