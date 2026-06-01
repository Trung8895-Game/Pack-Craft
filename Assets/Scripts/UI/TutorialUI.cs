using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text label;

    [SerializeField]
    private InventoryCraftController inventoryCraftController;
    private GoalProgress _progress;

    public void Bind(
        GoalProgress progress)
    {
        _progress = progress;

        Refresh();
    }

    public void Refresh()
    {
        var _itemDefinitions= inventoryCraftController.listItemDefinitions(_progress.Goal.TargetItem);
        label.text =
            $"Drag {_itemDefinitions[0].name} " +
            $"onto {_itemDefinitions[1].name} " +
            $"to craft {_progress.Goal.TargetItem.name} !!!";
    }
}