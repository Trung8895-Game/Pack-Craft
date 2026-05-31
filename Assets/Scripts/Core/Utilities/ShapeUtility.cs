using UnityEngine;

public static class ShapeUtility
{
    public static Vector2Int[] Normalize(
        Vector2Int[] shape)
    {
        int minX = int.MaxValue;
        int minY = int.MaxValue;

        foreach (var cell in shape)
        {
            minX = Mathf.Min(minX, cell.x);
            minY = Mathf.Min(minY, cell.y);
        }

        Vector2Int[] result =
            new Vector2Int[shape.Length];

        for (int i = 0; i < shape.Length; i++)
        {
            result[i] =
                new Vector2Int(
                    shape[i].x - minX,
                    shape[i].y - minY);
        }

        return result;
    }
}