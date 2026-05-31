using NUnit.Framework;
using UnityEngine;

public class ShapeCacheTests
{
    [Test]
    public void Cache_ReturnsSameReference()
    {
        ItemDefinition definition =
            ScriptableObject.CreateInstance<ItemDefinition>();

        definition.Shape =
            new[]
            {
                Vector2Int.zero
            };

        ShapeCache cache =
            new ShapeCache(definition);

        var shape1 =
            cache.GetShape(
                RotationState.None);

        var shape2 =
            cache.GetShape(
                RotationState.None);

        Assert.AreSame(
            shape1,
            shape2);
    }
}