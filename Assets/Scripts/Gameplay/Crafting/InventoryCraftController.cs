using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class InventoryCraftController
    : MonoBehaviour
{
    
    private CraftingDatabase database;

    [SerializeField]
    private InventoryGridUI gridUI;

    private CraftingService _craftingService;

    private async void Awake()
    {
         database = await AddressableManager.LoadAssetAsync<CraftingDatabase>(AddressableKeys.CraftingDatabase);
        _craftingService =new CraftingService(database);
    }

    public bool TryCraft(ItemInstance source,ItemInstance target)
{
    ItemDefinition result =_craftingService.TryCraft(source,target);
    if(result == null)
        {
            return false;
        }
    ItemInstance itemResult =
            new ItemInstance
            {
                Definition = result,
                Origin = target.Origin
            };

        Debug.Log("resultDefinition: " + result);
        itemResult.Initialize();

    bool canPlaceItem = gridUI.InventoryGrid.CanPlaceCraftedItem(target,itemResult,itemResult.Origin);

    if (!canPlaceItem)
    {
        return false;
    }

    CraftItems(source,target,result);

    return true;
}
    private async UniTask CraftItems(ItemInstance source,ItemInstance target,ItemDefinition result)
{
    Vector2Int spawnPosition = target.Origin;

    gridUI.InventoryGrid.RemoveItem(source);

    gridUI.InventoryGrid.RemoveItem(target);

    gridUI.RemoveItemView(source);
    gridUI.RemoveItemView(target);

    ItemInstance crafted =CraftResultSpawner.Spawn(result);

    gridUI.InventoryGrid.PlaceItem(crafted,spawnPosition);

    await gridUI.SpawnItemViewAsync(crafted);
    gridUI.RefreshAll();

    GoalEventBus.OnItemCrafted?.Invoke(result);
}
    public List<ItemDefinition> listItemDefinitions(ItemDefinition itemResult)
    {
        List<ItemDefinition> itemDefinitions= new List<ItemDefinition>();
        foreach(var recipe in database.Recipes)
        {
            if(recipe.Result.Id==itemResult.Id)
            {
                itemDefinitions.Add(recipe.ItemA);
                itemDefinitions.Add(recipe.ItemB);
                break;
            }
        }
        return itemDefinitions;
    }
}