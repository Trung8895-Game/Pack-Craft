using UnityEngine;

[CreateAssetMenu]
public class ItemDefinition : ScriptableObject
{
    public string Id;

    public Sprite Icon;

    public string AddressableKey;
    //public string IconAddressableKey;

    public Vector2Int[] Shape;

    public bool Rotatable;

    public bool isLoot;
}