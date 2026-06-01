using System.Collections.Generic;
using UnityEngine;

public class GoalTracker
{
    private readonly List<GoalProgress> _progresses = new();

    public IReadOnlyList<GoalProgress> Progresses => _progresses;


    public GoalTracker(LevelGoalDefinition levelGoal)
{
    if (levelGoal == null)
    {
        throw new System.ArgumentNullException(nameof(levelGoal));
    }

    if (levelGoal.Goals == null)
    {
        return;
    }

    for (int index = 0; index < levelGoal.Goals.Count; index++)
    {
        GoalDefinition goal = levelGoal.Goals[index];

        if (goal == null)
        {
            continue;
        }

        _progresses.Add(CreateProgress(goal, levelGoal, index));
    }
}

private GoalProgress CreateProgress( GoalDefinition goal, LevelGoalDefinition levelGoal, int index)
{
    return new GoalProgress
    {
        Goal = goal,
        LevelGoal = levelGoal,
        CurrentAmount = 0
    };
}

    public void RegisterCraft(ItemDefinition item)
    {
        foreach (var progress in _progresses)
        {
            if (progress.Goal.TargetItem != item)
            {
                continue;
            }

            progress.CurrentAmount++;
            
        }
    }

    public bool IsFinalGoal()
    {
        foreach (var progress in _progresses)
        {
            if (progress.CurrentAmount!=progress.Goal.RequiredAmount)
            {
                return false;
            }
        }

        return true;
    }
    
}