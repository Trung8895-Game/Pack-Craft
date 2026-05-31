using UnityEngine;

public class InventoryGrid
{
    private readonly int _width;

    private readonly int _height;

    private readonly GridCell[,] _cells;

    public InventoryGrid(int width, int height)
    {
        _width = width;
        _height = height;

        _cells = new GridCell[width, height];

        Initialize();
    }

    private void Initialize()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _cells[x, y] = new GridCell();
            }
        }
    }

    public bool IsInsideBounds(Vector2Int pos)
    {
        return pos.x >= 0 &&
               pos.y >= 0 &&
               pos.x < _width &&
               pos.y < _height;
    }

    public bool IsOccupied(Vector2Int pos)
    {
        return _cells[pos.x, pos.y].IsOccupied;
    }

    public void PlaceItem(
        ItemInstance item,
        Vector2Int origin)
    {
        var shape = item.GetCurrentShape();

        item.OccupiedCells.Clear();

        foreach (var offset in shape)
        {
            Vector2Int pos = origin + offset;

            _cells[pos.x, pos.y].IsOccupied = true;

            _cells[pos.x, pos.y].OccupiedItem = item;

            item.OccupiedCells.Add(pos);
        }

        item.Origin = origin;
    }

    public void RemoveItem(ItemInstance item)
    {
        foreach (var pos in item.OccupiedCells)
        {
            _cells[pos.x, pos.y].IsOccupied = false;

            _cells[pos.x, pos.y].OccupiedItem = null;
        }

        item.OccupiedCells.Clear();
    }
}