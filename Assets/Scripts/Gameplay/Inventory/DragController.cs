using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragController : MonoBehaviour
{
    [SerializeField]
    private InventoryGridUI gridUI;

    [SerializeField]
    private InventoryCraftController craftController;

    private PlacementValidator _validator;

    private InventoryItemView _draggingView;

    private ItemInstance _draggingItem;

    private Vector2Int _originalPosition;

    private CanvasGroup _canvasGroup;

    private PointerEventData _eventData;

    private void Start()
    {
        _validator =
            new PlacementValidator(
                gridUI.InventoryGrid);
    }

    public void BeginDrag(
        InventoryItemView view,
        PointerEventData eventData)
    {
        _eventData = eventData;

        _draggingView = view;

        _draggingItem = view.Item;

        _originalPosition =
            _draggingItem.Origin;

        gridUI.InventoryGrid
            .RemoveItem(_draggingItem);

        _canvasGroup =
            view.GetComponent<CanvasGroup>();

        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 0.7f;

            _canvasGroup.blocksRaycasts = false;
        }
    }

    public void UpdateDrag(
    PointerEventData eventData)
    {
        if (_draggingView == null)
            return;

        _draggingView.SetDragPosition(
            eventData.position);

        UpdatePreview(_draggingItem);
    }

    public void EndDrag(
        PointerEventData eventData)
    {
        if (_draggingItem == null|| _eventData == null)
            return;

        _draggingItem.Initialize();

        Vector2Int gridPos =
    GetCurrentGridPosition();

        bool valid =
            _validator.CanPlace(
                _draggingItem,
                gridPos);

        if (valid)
        {

            gridUI.InventoryGrid
                .PlaceItem(
                    _draggingItem,
                    gridPos);

            //craftController.CheckCrafting();

            Vector2 targetPosition =
            gridUI.GridToLocalPosition(
                gridPos);

            _draggingView._rectTransform
                .DOAnchorPos(
                    targetPosition,
                    0.15f);
        }
        else
        {
            ItemInstance targetItem = gridUI.InventoryGrid.GetItemAt(gridPos);
            if (targetItem != null && targetItem != _draggingItem)
            {
            bool crafted =
            craftController.TryCraft(
                _draggingItem,
                targetItem);

            if (crafted)
            {
                return;
            }
            }
            gridUI.InventoryGrid
                .PlaceItem(
                    _draggingItem,
                    _originalPosition);

            //craftController.CheckCrafting();
        }

        gridUI.RefreshItemPosition(
            _draggingItem);

        

        gridUI.ClearHighlights();

        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 1f;

            _canvasGroup.blocksRaycasts = true;
        }

        _draggingView = null;

        _draggingItem = null;
    }

    private void UpdatePreview(ItemInstance item)
    {
        if (_eventData == null)
            return;

        Vector2Int gridPos =
            GetCurrentGridPosition();

        bool valid =
            _validator.CanPlace(
                item,
                gridPos);

        gridUI.ShowPlacementPreview(
            item,
            gridPos,
            valid);
    }

    private Vector2Int GetCurrentGridPosition()
    {
           

        RectTransform gridRect =
            gridUI.GetComponent<RectTransform>();

            RectTransformUtility
            .ScreenPointToLocalPointInRectangle(
                gridRect,
                _eventData.position,
                _eventData.pressEventCamera,
                out var localPos);
        
            

        return gridUI.LocalToGridPosition(
            localPos);
    }

    private void rotateDraggingItem(ItemInstance item, InventoryItemView view)
    {
        RotationService.Rotate(
    item);

        view.RefreshVisual();

        UpdatePreview(item);
    }
    public void RotateDraggingItem(ItemInstance item, InventoryItemView view)
    {
        rotateDraggingItem(item, view);
    }
}