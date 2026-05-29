using UnityEngine;

public static class ShapeUtility
{
    public static Vector2Int[] GetRotatedShape(
        Vector2Int[] original,
        RotationState rotation)
    {
        Vector2Int[] result =
            new Vector2Int[original.Length];

        for(int i = 0; i < original.Length; i++)
        {
            result[i] =
                Rotate(original[i], rotation);
        }

        return Normalize(result);
    }

    private static Vector2Int Rotate(
        Vector2Int point,
        RotationState rotation)
    {
        return rotation switch
        {
            RotationState.None =>
                point,

            RotationState.Right90 =>
                new Vector2Int(
                    -point.y,
                    point.x),

            RotationState.Right180 =>
                new Vector2Int(
                    -point.x,
                    -point.y),

            RotationState.Right270 =>
                new Vector2Int(
                    point.y,
                    -point.x),

            _ => point
        };
    }

    public static Vector2Int[] Normalize(
    Vector2Int[] shape)
{
    int minX = int.MaxValue;
    int minY = int.MaxValue;

    foreach(var p in shape)
    {
        if(p.x < minX)
            minX = p.x;

        if(p.y < minY)
            minY = p.y;
    }

    Vector2Int offset =
        new Vector2Int(-minX, -minY);

    Vector2Int[] normalized =
        new Vector2Int[shape.Length];

    for(int i = 0; i < shape.Length; i++)
    {
        normalized[i] = shape[i] + offset;
    }

    return normalized;
}
}