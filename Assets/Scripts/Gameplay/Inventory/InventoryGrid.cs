using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid
{
    private readonly int _width;

    private readonly int _height;

    private readonly GridCell[,] _cells;

    private readonly List<ItemInstance> _items = new();

    public IReadOnlyList<ItemInstance> Items => _items;


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
        return pos.x >= 0 && pos.y >= 0 && pos.x < _width && pos.y < _height;
    }

    public bool IsOccupied(Vector2Int pos)
    {
        return _cells[pos.x, pos.y].IsOccupied;
    }

    public void PlaceItem(ItemInstance item,Vector2Int origin)
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

    public ItemInstance GetItemAt(Vector2Int cell)
{
    if (!IsInsideBounds(cell))
    {
        return null;
    }

    return _cells[cell.x, cell.y].OccupiedItem;
}
public bool CanPlaceItem(
    ItemInstance item,Vector2Int origin)
{
    foreach (var offset in item.GetCurrentShape())
    {
        Vector2Int pos = origin + offset;

        if (!IsInsideBounds(pos))
        {
            return false;
        }

        ItemInstance existing = GetItemAt(pos);

        if (existing != null && existing != item)
        {
            return false;
        }
    }

    return true;
}

public bool CanPlaceCraftedItem(ItemInstance targetItem,
    ItemInstance Crafteditem,Vector2Int origin)
{
    foreach (var offset in Crafteditem.GetCurrentShape())
    {
        Vector2Int pos = origin + offset;

        if (!IsInsideBounds(pos))
        {
            return false;
        }

        ItemInstance existing = GetItemAt(pos);

        if (existing != null && existing != targetItem)
        {
            return false;
        }
    }

    return true;
}

public bool CanRotateItem(ItemInstance item)
{
    RotationState oldRotation = item.Rotation;

    item.Rotation = item.GetNextRotation();

    bool result =CanPlaceItem(item,item.Origin);

    item.Rotation = oldRotation;

    return result;
}
public bool TryFindFreePosition(ItemInstance item,out Vector2Int position)
{
    for (int y = 0; y < _height; y++)
    {
        for (int x = 0; x < _width; x++)
        {
            Vector2Int candidate = new Vector2Int(x, y);

            if (CanPlaceItem(item,candidate))
            {
                position = candidate;
                return true;
            }
        }
    }

    position = Vector2Int.zero;
    return false;
}

public bool TryRotateItem(ItemInstance item)
{
    if (!CanRotateItem(item))
    {
        return false;
    }

    ClearOccupiedCells(item);

    item.Rotation = item.GetNextRotation();

    PlaceItem(item,item.Origin);

    return true;
}

private void ClearOccupiedCells(ItemInstance item)
{
    foreach (var pos in item.OccupiedCells)
    {
        _cells[pos.x, pos.y].IsOccupied = false;

        _cells[pos.x, pos.y].OccupiedItem = null;
    }

    item.OccupiedCells.Clear();
}
}