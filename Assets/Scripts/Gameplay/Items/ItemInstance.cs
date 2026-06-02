using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public ItemDefinition Definition;

    public Vector2Int Origin;

    public RotationState Rotation;

    public List<Vector2Int> OccupiedCells = new();

    public ShapeCache _shapeCache;

    public void Initialize()
    {
        _shapeCache = new ShapeCache(Definition);
    }

    public Vector2Int[] GetCurrentShape()
    {
        return _shapeCache.GetShape(Rotation);
    }

    public List<Vector2Int> GetOccupiedCells()
{
    List<Vector2Int> result = new();

    foreach (var cell in GetCurrentShape())
    {
        result.Add(Origin + cell);
    }

    return result;
}

public RotationState GetNextRotation()
{
    return Rotation switch
    {
        RotationState.None => RotationState.Right90,
        RotationState.Right90 => RotationState.Right180,
        RotationState.Right180 => RotationState.Right270,
        RotationState.Right270 => RotationState.None,
        _ => RotationState.None
    };
}
}