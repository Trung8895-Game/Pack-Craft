using System;

public static class InventoryEvents
{
    public static Action<ItemInstance>
        OnItemPlaced;

    public static Action<ItemInstance>
        OnItemRemoved;
}