using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField]
    private InventoryGridUI inventoryUI;

    [SerializeField]
    private ItemDefinition wood;
    [SerializeField]
    private ItemDefinition stone;
    [SerializeField]
    private ItemDefinition silver;
    [SerializeField]
    private ItemDefinition gold;
    [SerializeField]
    private ItemDefinition gem;
    [SerializeField]
    private ItemDefinition bone;
    [SerializeField]
    private ItemDefinition leather;
    [SerializeField]
    private ItemDefinition woodStick;

    [SerializeField]
    private ItemDefinition arrow;
    [SerializeField]
    private ItemDefinition sword;
    [SerializeField]
    private ItemDefinition shield;
    [SerializeField]
    private ItemDefinition axe;
    [SerializeField]
    private ItemDefinition cape;
    [SerializeField]
    private ItemDefinition crown;
    [SerializeField]
    private ItemDefinition ring;
    [SerializeField]
    private ItemDefinition key;
    [SerializeField]
    private ItemDefinition Bow;
    [SerializeField]
    private ItemDefinition Neck;

    private void Start()
    {
        ItemInstance item =
            new ItemInstance
            {
                Definition = axe,
                Origin = new Vector2Int(0, 0)
            };
        item.Initialize();
        inventoryUI.InventoryGrid
            .PlaceItem(item, item.Origin);

        inventoryUI.SpawnItem(item);
        inventoryUI.RefreshGridVisual();
        //inventoryUI.ShowPlacementPreview(item, item.Origin, true);
    }
}