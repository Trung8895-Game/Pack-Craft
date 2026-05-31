using System.Collections.Generic;
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
    private DragController dragController;

    private InventoryGrid _inventoryGrid;

    private InventoryCellUI[,] _cellViews;

    private readonly Dictionary<ItemInstance,
        InventoryItemView> _itemViews = new();

    public InventoryGrid InventoryGrid => _inventoryGrid;

    public float CellSize => cellSize;

    private void Awake()
    {
        _inventoryGrid =
            new InventoryGrid(width, height);

        GenerateGridVisual();
    }

    private void GenerateGridVisual()
    {
        _cellViews =
            new InventoryCellUI[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                InventoryCellUI cell =
                    Instantiate(cellPrefab, gridRoot);

                RectTransform rect =
                    cell.GetComponent<RectTransform>();

                rect.sizeDelta =
                    Vector2.one * cellSize;

                rect.anchoredPosition =
                    GridToLocalPosition(
                        new Vector2Int(x, y));

                _cellViews[x, y] = cell;
            }
        }
    }

    public void SpawnItem(ItemInstance item)
    {
        if (_itemViews.ContainsKey(item))
            return;

        InventoryItemView view =
            Instantiate(itemPrefab, gridRoot);

        view.Initialize(item, this, dragController);

        _itemViews.Add(item, view);

        RefreshItemPosition(item);
    }

    public void RefreshItemPosition(ItemInstance item)
    {
        if (!_itemViews.TryGetValue(item, out var view))
            return;

        Vector2 pos =
            GridToLocalPosition(item.Origin);

        view.SetPosition(pos);

        view.RefreshVisual();
    }

    public Vector2 GridToLocalPosition(
        Vector2Int gridPos)
    {
        return new Vector2(
            gridPos.x * cellSize,
            -gridPos.y * cellSize);
    }

    public Vector2Int LocalToGridPosition(
        Vector2 localPosition)
    {
        int x =
            Mathf.FloorToInt(localPosition.x / cellSize);

        int y =
            Mathf.FloorToInt(-localPosition.y / cellSize);

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

    public void ShowPlacementPreview(
        ItemInstance item,
        Vector2Int origin,
        bool valid)
    {
        ClearHighlights();

        var shape =
            item.GetCurrentShape();

        foreach (var offset in shape)
        {
            Vector2Int pos =
                origin + offset;

            if (!_inventoryGrid
                .IsInsideBounds(pos))
                continue;

            if (valid)
            {
                _cellViews[pos.x, pos.y]
                    .SetValid();
            }
            else
            {
                _cellViews[pos.x, pos.y]
                    .SetInvalid();
            }
        }
    }
    public void RefreshGridVisual()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos =
                    new Vector2Int(x, y);

                if (_inventoryGrid.IsOccupied(pos))
                {
                    _cellViews[x, y]
                        .SetOccupied();
                }
                else
                {
                    _cellViews[x, y]
                        .SetNormal();
                }
            }
        }
    }
}