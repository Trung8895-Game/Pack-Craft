using UnityEngine;

public static class CraftResultSpawner
{
    public static ItemInstance Spawn(
        ItemDefinition definition)
    {
        ItemInstance item =
            new ItemInstance
            {
                Definition = definition,
                Rotation = RotationState.None
            };

        item.Initialize();

        return item;
    }
}