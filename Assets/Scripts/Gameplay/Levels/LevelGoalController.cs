using Unity.VisualScripting;
using UnityEngine;

public class LevelGoalController
    : MonoBehaviour
{

    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private LevelGoalUI levelGoalUI;

    [SerializeField]
    private LevelCompletePopup popup;

    private GoalTracker _tracker;

    public GoalTracker Tracker =>
        _tracker;

    private void Awake()
    {

        Debug.Log("Awake !!!!");
        _tracker = new GoalTracker(levelManager.Database.Levels[GameProgress.CurrentLevel].Goals);
    }
    
    private void OnEnable()
    {
        GoalEventBus.OnItemCrafted += OnItemCrafted;
    }

    private void OnDisable()
    {
        GoalEventBus.OnItemCrafted -= OnItemCrafted;
    }

    private void OnItemCrafted(ItemDefinition item)
    {
        _tracker.RegisterCraft(item);

        foreach(var _progress in _tracker.Progresses)
            {
                if(_progress.IsCompleted())
                {
                    if(_tracker.IsFinalGoal())
                    {
                        CompleteLevel();
                    }
                    else
                    {
                
                       continue;
                
                    }
                    
                }
                else
                {
                    levelGoalUI.entry.Bind(_progress);
                    levelGoalUI.tutorial.Bind(_progress);
                    break;
                }
            }
        
    }

    private void CompleteLevel()
    {
        Debug.Log("LEVEL COMPLETE");

        popup.Show();
    }
}