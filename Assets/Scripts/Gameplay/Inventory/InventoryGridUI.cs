using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Mono.Cecil.Cil;
using UnityEngine;

public class InventoryGridUI : MonoBehaviour
{
    [Header("Grid Config")]
    [SerializeField]
    private int width = 6;

    [SerializeField]
    private int height = 8;

    [SerializeField]
    private float cellSize = 100f;

    [Header("References")]
    [SerializeField]
    private RectTransform gridRoot;

    [SerializeField]
    private InventoryCellUI cellPrefab;

    [SerializeField]
    private InventoryItemView itemPrefab;

    [SerializeField]
    private InventoryItemView lootPrefab;

    [SerializeField]
    private DragController dragController;
    [SerializeField]
    private InventoryCraftController craftController;

    private InventoryGrid _inventoryGrid;

    public InventoryCellUI[,] _cellViews {set;get;}

    private readonly Dictionary<ItemInstance,InventoryItemView> _itemViews = new();

    public InventoryGrid InventoryGrid => _inventoryGrid;

    public float CellSize => cellSize;

    private void Awake()
    {
        _inventoryGrid = new InventoryGrid(width, height);

        GenerateGridVisual();
    }

    private void GenerateGridVisual()
    {
        _cellViews = new InventoryCellUI[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                InventoryCellUI cell = Instantiate(cellPrefab, gridRoot);

                RectTransform rect = cell.GetComponent<RectTransform>();

                rect.sizeDelta = Vector2.one * cellSize;

                rect.anchoredPosition = GridToLocalPosition(new Vector2Int(x, y));

                _cellViews[x, y] = cell;
            }
        }
    }

    public async UniTask SpawnItemViewAsync(ItemInstance item)
    {
        if (_itemViews.ContainsKey(item))
            return;

        InventoryItemView view=null;

        if(item.Definition.isLoot)
        {
            view =
            Instantiate(lootPrefab, gridRoot);
        }
        else
        {
            view =
            Instantiate(itemPrefab, gridRoot);
        }
        

        await view.InitializeAsync(item, this, dragController);

        _itemViews.Add(item, view);

        RefreshItemPosition(item);
    }
    public void RemoveItemView(
    ItemInstance item)
{
    if (_itemViews.TryGetValue(item, out InventoryItemView view))
    {
        Destroy(view.gameObject);

        _itemViews.Remove(item);
    }
}
    public void RefreshItemPosition(ItemInstance item)
    {
        if (!_itemViews.TryGetValue(item, out var view))
            return;

        Vector2 pos = GridToLocalPosition(item.Origin);

        view.SetPosition(pos);

        view.RefreshVisual();
    }

    public Vector2 GridToLocalPosition(Vector2Int gridPos)
    {
        return new Vector2(gridPos.x * cellSize,-gridPos.y * cellSize);
    }

    public Vector2Int LocalToGridPosition(Vector2 localPosition)
    {
        int x =Mathf.FloorToInt(localPosition.x / cellSize);

        int y =Mathf.FloorToInt(-localPosition.y / cellSize);

        return new Vector2Int(x, y);
    }

    public void ClearHighlights()
    {
        /*for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _cellViews[x, y]
                    .SetNormal();
            }
        }*/

        RefreshGridVisual();
    }

    public void ShowPlacementPreview(ItemInstance item, Vector2Int origin, bool valid)
    {
        ClearHighlights();

        var shape = item.GetCurrentShape();

        foreach (var offset1 in shape)
        {
            Vector2Int pos1 = origin + offset1;

            if (!_inventoryGrid.IsInsideBounds(pos1))
                continue;

            if (valid)
            {
                _cellViews[pos1.x, pos1.y].SetValid();
            }
            else
            {
                Vector2Int gridPos = dragController.GetCurrentGridPosition();
                ItemInstance targetItem = InventoryGrid.GetItemAt(gridPos);
                var result= craftController._craftingService.TryCraft(item,targetItem);
                        
                    foreach (var occupiedCell in targetItem.OccupiedCells)
                    {
                               
                        if(result!=null)
                        {
                       
                            _cellViews[occupiedCell.x, occupiedCell.y].SetCrafting();
                                
                        }
                        else
                        {
                            _cellViews[occupiedCell.x, occupiedCell.y].SetInvalid();
                        }
                            
                            
                    }
                  
                
            }
        }
       

        
               
                
    }
    public void RefreshGridVisual()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                if (_inventoryGrid.IsOccupied(pos))
                {
                    _cellViews[x, y].SetOccupied();
                }
                else
                {
                    _cellViews[x, y].SetNormal();
                }
            }
        }
    }

    public void RefreshAll()
    {
        RefreshGridVisual();

        RefreshAllItemViews();
    }

    private void RefreshAllItemViews()
    {
        foreach (var itemView in _itemViews.Values)
        {
            itemView.RefreshVisual();
        }
    }
}