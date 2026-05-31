using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid
{
    private readonly int _width;

    private readonly int _height;

    private readonly GridCell[,] _cells;

    private readonly List<ItemInstance> _items =
    new();

    public IReadOnlyList<ItemInstance> Items =>
    _items;


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

        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
    }

    public void RemoveItem(ItemInstance item)
    {
        foreach (var pos in item.OccupiedCells)
        {
            _cells[pos.x, pos.y].IsOccupied = false;

            _cells[pos.x, pos.y].OccupiedItem = null;
        }

        item.OccupiedCells.Clear();
        _items.Remove(item);
    }

    public ItemInstance GetItemAt(
    Vector2Int cell)
{
    foreach (var item in _items)
    {
        foreach (var occupiedCell in item.GetOccupiedCells())
        {
            if (occupiedCell == cell)
            {
                return item;
            }
        }
    }

    return null;
}

}