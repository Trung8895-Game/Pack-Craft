using UnityEngine;
using UnityEngine.UI;

public class InventoryCellUI : MonoBehaviour
{
    [SerializeField]
    private Image background;

    [Header("Colors")]
    [SerializeField]
    private Color normalColor = Color.white;

    [SerializeField]
    private Color occupiedColor = Color.gray;

    [SerializeField]
    private Color validColor = Color.green;

    [SerializeField]
    private Color invalidColor = Color.red;

    public void SetNormal()
    {
        background.color = normalColor;
    }

    public void SetOccupied()
    {
        background.color = occupiedColor;
    }


    public void SetValid()
    {
        background.color = validColor;
    }

    public void SetInvalid()
    {
        background.color = invalidColor;
    }
}