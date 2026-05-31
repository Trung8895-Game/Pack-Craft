using NUnit.Framework;
using UnityEngine;

public class ItemInstanceTests
{
    [Test]
    public void GetCurrentShape_ReturnsShape()
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
                Definition = definition
            };

        item.Initialize();

        Vector2Int[] shape =
            item.GetCurrentShape();

        Assert.AreEqual(
            2,
            shape.Length);
    }
}