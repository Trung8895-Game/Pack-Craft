using System.Collections.Generic;
using UnityEngine;

public class InventoryCraftController
    : MonoBehaviour
{
    [SerializeField]
    private CraftingDatabase database;

    [SerializeField]
    private InventoryGridUI gridUI;

    private CraftingService _craftingService;

    private void Awake()
    {
        _craftingService =
            new CraftingService(
                database);
    }

    public bool TryCraft(
    ItemInstance source,
    ItemInstance target)
{
    ItemDefinition result =
        _craftingService.TryCraft(
            source,
            target);

    if (result == null)
    {
        return false;
    }

    CraftItems(
        source,
        target,
        result);

    return true;
}
    private void CraftItems(
    ItemInstance source,
    ItemInstance target,
    ItemDefinition result)
{
    Vector2Int spawnPosition =
        target.Origin;

    gridUI.InventoryGrid
        .RemoveItem(source);

    gridUI.InventoryGrid
        .RemoveItem(target);

    gridUI.RemoveItemView(source);
    gridUI.RemoveItemView(target);

    ItemInstance crafted =
        CraftResultSpawner
            .Spawn(result);

    gridUI.InventoryGrid
        .PlaceItem(
            crafted,
            spawnPosition);

    gridUI.SpawnItemView(crafted);
        gridUI.RefreshAll();
}
}