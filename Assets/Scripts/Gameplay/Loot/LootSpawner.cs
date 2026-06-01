using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField]
    private InventoryGridUI gridUI;

    public bool SpawnLoot(ItemDefinition definition)
    {
        ItemInstance item = new ItemInstance
            {
                Definition = definition,
                Rotation = RotationState.None
            };

        item.Initialize();

        bool found = gridUI.InventoryGrid.TryFindFreePosition(item,out Vector2Int position);

        if (!found)
        {
            Debug.Log("Inventory Full");

            return false;
        }

        gridUI.InventoryGrid.PlaceItem(item,position);

        gridUI.SpawnItemView(item);
        gridUI.RefreshAll();
        return true;
    }
}