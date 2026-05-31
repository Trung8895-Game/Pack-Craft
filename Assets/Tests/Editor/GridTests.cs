using NUnit.Framework;
using UnityEngine;

public class GridTests
{
    [Test]
    public void Grid_Created_CorrectBounds()
    {
        InventoryGrid grid =
            new InventoryGrid(6, 8);

        Assert.IsTrue(
            grid.IsInsideBounds(
                new Vector2Int(5, 7)));

        Assert.IsFalse(
            grid.IsInsideBounds(
                new Vector2Int(6, 8)));
    }

    [Test]
    public void Grid_NewCell_NotOccupied()
    {
        InventoryGrid grid =
            new InventoryGrid(6, 8);

        Assert.IsFalse(
            grid.IsOccupied(
                Vector2Int.zero));
    }
}