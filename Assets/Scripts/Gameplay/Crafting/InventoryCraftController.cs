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
        _craftingService =new CraftingService(database);
    }

    public bool TryCraft(ItemInstance source,ItemInstance target)
{
    ItemDefinition result =_craftingService.TryCraft(source,target);

    ItemInstance itemResult =
            new ItemInstance
            {
                Definition = result,
                Origin = target.Origin
            };
        itemResult.Initialize();

    bool canPlaceItem = gridUI.InventoryGrid.CanPlaceCraftedItem(target,itemResult,itemResult.Origin);

    if (result == null|| !canPlaceItem)
    {
        return false;
    }

    CraftItems(source,target,result);

    return true;
}
    private void CraftItems(ItemInstance source,ItemInstance target,ItemDefinition result)
{
    Vector2Int spawnPosition = target.Origin;

    gridUI.InventoryGrid.RemoveItem(source);

    gridUI.InventoryGrid.RemoveItem(target);

    gridUI.RemoveItemView(source);
    gridUI.RemoveItemView(target);

    ItemInstance crafted =CraftResultSpawner.Spawn(result);

    gridUI.InventoryGrid.PlaceItem(crafted,spawnPosition);

    gridUI.SpawnItemView(crafted);
    gridUI.RefreshAll();

    GoalEventBus.OnItemCrafted?.Invoke(result);
}
    public List<ItemDefinition> listItemDefinitions(ItemDefinition itemResult)
    {
        List<ItemDefinition> itemDefinitions= new List<ItemDefinition>();
        foreach(var recipe in database.Recipes)
        {
            if(recipe.Result==itemResult)
            {
                itemDefinitions.Add(recipe.ItemA);
                itemDefinitions.Add(recipe.ItemB);
                break;
            }
        }
        return itemDefinitions;
    }
}