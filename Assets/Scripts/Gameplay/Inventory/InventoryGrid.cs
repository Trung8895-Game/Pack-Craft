using UnityEngine;

public enum RotationState
{
    None = 0,
    Right90 = 1,
    Right180 = 2,
    Right270 = 3
}

public class GridCell
{
    public Vector2Int Position;

    public bool IsOccupied;

    public ItemInstance OccupiedItem;

    
}

public class ItemInstance
{
    public ItemDefinition Definition;

    public Vector2Int GridPosition;

    public RotationState Rotation;

    public Vector2Int[] GetCurrentShape()
    {
        return ShapeUtility.GetRotatedShape(
            Definition.Shape,
            Rotation);
    }
}

public class InventoryGrid
{
    private GridCell[,] _cells;

    public int Width { get; }
    public int Height { get; }

    public InventoryGrid(int width, int height)
    {
        Width = width;
        Height = height;

        _cells = new GridCell[width, height];

        Initialize();
    }

    private void Initialize()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _cells[x, y] = new GridCell
                {
                    Position = new Vector2Int(x, y)
                };
            }
        }
    }

    private bool IsInsideBounds(Vector2Int pos)
    {
        return pos.x >= 0 &&
           pos.y >= 0 &&
           pos.x < Width &&
           pos.y < Height;
    }
    
    public bool CanPlaceItem(
    ItemInstance item,
    Vector2Int origin)
    {
        var shape = item.GetCurrentShape();

        foreach (var offset in shape)
        {
            Vector2Int cellPos = origin + offset;

            if (!IsInsideBounds(cellPos))
                return false;

            if (_cells[cellPos.x, cellPos.y].IsOccupied)
                return false;
        }

        return true;
    }

    

    public void PlaceItem(
    ItemInstance item,
    Vector2Int origin)
    {
    var shape = item.GetCurrentShape();

    foreach(var offset in shape)
    {
        Vector2Int pos = origin + offset;

        _cells[pos.x, pos.y].IsOccupied = true;
        _cells[pos.x, pos.y].OccupiedItem = item;
    }

    item.GridPosition = origin;
    }
}