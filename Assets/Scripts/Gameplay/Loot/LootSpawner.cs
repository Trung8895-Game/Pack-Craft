using Cysharp.Threading.Tasks;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField]
    private InventoryGridUI gridUI;

    public async UniTask<bool> SpawnLootAsync(string itemKey)
    {
        ItemDefinition definition =
            await AddressableManager
            .LoadAssetAsync<ItemDefinition>(
                itemKey);

        if (definition == null)
        {
            return false;
        }
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

        await gridUI.SpawnItemViewAsync(item);
        gridUI.RefreshAll();
        return true;
    }
}