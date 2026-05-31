using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Lean.Touch;

public class InventoryItemView : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [Header("References")]
    public RectTransform rectTransform;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private RectTransform iconRoot;

    private ItemInstance _item;

    private InventoryGridUI _gridUI;

    public ItemInstance Item => _item;

    private DragController _dragController;

    public void Initialize(
        ItemInstance item,
        InventoryGridUI gridUI,
        DragController dragController)
    {
        _item = item;

        _gridUI = gridUI;

        _dragController = dragController;

        icon.sprite =
            item.Definition.Icon;

        icon.preserveAspect = true;

        RefreshVisual();
    }
    private void OnEnable()
    {
        LeanTouch.OnFingerTap += HandleTap;
    }
    public void RefreshVisual()
    {
        RefreshSize();

        RefreshRotation();
    }

    private void RefreshSize()
    {
        Vector2Int[] shape =
            _item.GetCurrentShape();

        GetShapeBounds(
            shape,
            out int minX,
            out int maxX,
            out int minY,
            out int maxY);

        int width =
            (maxX - minX) + 1;

        int height =
            (maxY - minY) + 1;

        float pixelWidth =
            width * _gridUI.CellSize;

        float pixelHeight =
            height * _gridUI.CellSize;

        rectTransform.sizeDelta =
            new Vector2(
                pixelWidth,
                pixelHeight);

        iconRoot.sizeDelta =
            new Vector2(
                pixelWidth,
                pixelHeight);
    }
    private void HandleTap(LeanFinger finger)
    {
        RotateItem();
    }
    public void RotateItem()
    {
        if (_dragController == null)
            return;

        _dragController.RotateDraggingItem(_item, this);

        _gridUI.InventoryGrid
           .RemoveItem(_item);

        _item.Initialize();
        _gridUI.InventoryGrid
            .PlaceItem(_item, _item.Origin);

       
        _gridUI.RefreshGridVisual();
    }

    private void RefreshRotation()
    {
        float zRotation =
            _item.Rotation switch
            {
                RotationState.None => 0,
                RotationState.Right90 => -90,
                RotationState.Right180 => -180,
                RotationState.Right270 => -270,
                _ => 0
            };

        iconRoot.localEulerAngles =
            new Vector3(0, 0, zRotation);
    }

    private void GetShapeBounds(
        Vector2Int[] shape,
        out int minX,
        out int maxX,
        out int minY,
        out int maxY)
    {
        minX = int.MaxValue;
        maxX = int.MinValue;

        minY = int.MaxValue;
        maxY = int.MinValue;

        foreach (var cell in shape)
        {
            if (cell.x < minX)
                minX = cell.x;

            if (cell.x > maxX)
                maxX = cell.x;

            if (cell.y < minY)
                minY = cell.y;

            if (cell.y > maxY)
                maxY = cell.y;
        }
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition =
            position;
    }

    public void SetDragPosition(
        Vector2 screenPosition)
    {
        rectTransform.position =
            screenPosition;
    }

    public void OnBeginDrag(
    PointerEventData eventData)
    {
        _dragController.BeginDrag(
            this,
            eventData);
        
    }

    public void OnDrag(
    PointerEventData eventData)
    {
        _dragController.UpdateDrag(
            eventData);
    }

    public void OnEndDrag(
    PointerEventData eventData)
    {
        _dragController.EndDrag(
            eventData);
    }
}