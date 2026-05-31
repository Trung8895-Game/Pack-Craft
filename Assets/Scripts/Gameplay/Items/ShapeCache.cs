using System.Collections.Generic;
using UnityEngine;

public class ShapeCache
{
    private readonly Dictionary<
        RotationState,
        Vector2Int[]> _cache
        = new();

    private readonly ItemDefinition _definition;

    public ShapeCache(
        ItemDefinition definition)
    {
        _definition = definition;
    }

    public Vector2Int[] GetShape(
        RotationState rotation)
    {
        if (_cache.TryGetValue(
            rotation,
            out var shape))
        {
            return shape;
        }

        shape =
            BuildShape(rotation);

        _cache.Add(
            rotation,
            shape);

        return shape;
    }

    private Vector2Int[] BuildShape(
        RotationState rotation)
    {
        Vector2Int[] original =
            _definition.Shape;

        Vector2Int[] rotated =
            new Vector2Int[original.Length];

        for (int i = 0; i < original.Length; i++)
        {
            rotated[i] =
                ShapeRotationUtility.Rotate(
                    original[i],
                    rotation);
        }

        return ShapeUtility.Normalize(
            rotated);
    }
}