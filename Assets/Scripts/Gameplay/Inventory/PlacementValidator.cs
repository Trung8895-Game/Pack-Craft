using UnityEngine;

public class PlacementValidator
{
    private readonly InventoryGrid _grid;

    public PlacementValidator(
        InventoryGrid grid)
    {
        _grid = grid;
    }

    public bool CanPlace(
        ItemInstance item,
        Vector2Int origin)
    {
        var shape = item.GetCurrentShape();

        foreach (var offset in shape)
        {
            Vector2Int pos = origin + offset;

            if (!_grid.IsInsideBounds(pos))
                return false;

            if (_grid.IsOccupied(pos))
                return false;
        }

        return true;
    }
}