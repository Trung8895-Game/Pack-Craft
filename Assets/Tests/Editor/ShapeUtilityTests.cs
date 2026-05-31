using NUnit.Framework;
using UnityEngine;

public class ShapeUtilityTests
{
    [Test]
    public void Normalize_RemovesNegativeCoordinates()
    {
        Vector2Int[] shape =
        {
            new Vector2Int(-1,0),
            new Vector2Int(0,0),
            new Vector2Int(0,1)
        };

        Vector2Int[] normalized =
            ShapeUtility.Normalize(shape);

        foreach (var cell in normalized)
        {
            Assert.GreaterOrEqual(cell.x, 0);
            Assert.GreaterOrEqual(cell.y, 0);
        }
    }
}