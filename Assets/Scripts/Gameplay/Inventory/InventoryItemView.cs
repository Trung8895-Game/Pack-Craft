using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Lean.Touch;
using Cysharp.Threading.Tasks;

public class InventoryItemView : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [Header("References")]
    public RectTransform _rectTransform;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private RectTransform iconRoot;

    private ItemInstance _item;

    private InventoryGridUI _gridUI;

    public ItemInstance Item => _item;

    private DragController _dragController;

    public async UniTask InitializeAsync(ItemInstance item, InventoryGridUI gridUI, DragController dragController)
    {
        _item = item;

        _gridUI = gridUI;

        _dragController = dragController;

        ItemDefinition itemDefinition = await AddressableManager.LoadAssetAsync<ItemDefinition>(item.Definition.AddressableKey);

        _icon.sprite = itemDefinition.Icon;

        _icon.preserveAspect = true;

        RefreshVisual();
    }
   /* private void OnEnable()
    {
        LeanTouch.OnFingerTap += HandleTap;
    }*/
    public void RefreshVisual()
    {
        RefreshSize();

        RefreshRotation();
    }

    private void RefreshSize()
    {
        Vector2Int[] shape = _item.GetCurrentShape();

        GetShapeBounds( shape, out int minX, out int maxX, out int minY, out int maxY);

        int width = (maxX - minX) + 1;

        int height = (maxY - minY) + 1;

        float pixelWidth = width * _gridUI.CellSize;

        float pixelHeight = height * _gridUI.CellSize;

        _rectTransform.sizeDelta = new Vector2( pixelWidth, pixelHeight);

        iconRoot.sizeDelta = new Vector2( pixelWidth, pixelHeight);
    }
   /* private void HandleTap(LeanFinger finger)
    {
        RotateItem();
        
    }
    */
    public void RotateItem()
    {bool success =
        _gridUI.InventoryGrid.TryRotateItem(_item);

    if (!success)
    {
        Debug.Log("Rotation blocked");
        return;
    }

        _gridUI.RefreshAll();
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

        iconRoot.localEulerAngles = new Vector3(0, 0, zRotation);
    }

    private void GetShapeBounds( Vector2Int[] shape, out int minX, out int maxX, out int minY, out int maxY)
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
        _rectTransform.anchoredPosition = position;
    }

    public void SetDragPosition( Vector2 screenPosition)
    {
        _rectTransform.position = screenPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragController.BeginDrag( this, eventData);
        
    }

    public void OnDrag( PointerEventData eventData)
    {
        _dragController.UpdateDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragController.EndDrag(eventData);
    }
}