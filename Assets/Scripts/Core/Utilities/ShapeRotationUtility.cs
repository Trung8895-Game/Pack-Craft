using UnityEngine;

public static class ShapeRotationUtility
{
    public static Vector2Int Rotate(
        Vector2Int point,
        RotationState rotation)
    {
        return rotation switch
        {
            RotationState.None =>
                new Vector2Int(point.x, point.y),

            RotationState.Right90 =>
                new Vector2Int(-point.y, point.x),

            RotationState.Right180 =>
                new Vector2Int(-point.x, -point.y),

            RotationState.Right270 =>
                new Vector2Int(point.y, -point.x),

            _ => point
        };
    }
}