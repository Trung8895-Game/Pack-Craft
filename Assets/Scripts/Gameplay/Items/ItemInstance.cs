using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public ItemDefinition Definition;

    public Vector2Int Origin;

    public RotationState Rotation;

    public List<Vector2Int> OccupiedCells = new();

    private ShapeCache _shapeCache;

    public void Initialize()
    {
        _shapeCache =
            new ShapeCache(
                Definition);
    }

    public Vector2Int[] GetCurrentShape()
    {
        return _shapeCache.GetShape(
            Rotation);
    }
}