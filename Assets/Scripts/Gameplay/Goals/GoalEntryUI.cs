using TMPro;
using UnityEngine;

public class GoalEntryUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text label;

    [SerializeField]
    private TMP_Text levelLabel;

    private GoalProgress _progress;

    public void Bind(
        GoalProgress progress)
    {
        _progress = progress;

        Refresh();
    }

    public void Refresh()
    {
        label.text =
            $"{_progress.Goal.TargetItem.name} " +
            $"{_progress.CurrentAmount}/" +
            $"{_progress.Goal.RequiredAmount}";

        levelLabel.text =
            $"LEVEL {GameProgress.CurrentLevel+1}";
    }
}