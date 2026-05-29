using UnityEngine;

[CreateAssetMenu]
public class ItemDefinition : ScriptableObject
{
    public string Id;

    public Sprite Icon;

    public Vector2Int[] Shape;

    public bool Rotatable;
}