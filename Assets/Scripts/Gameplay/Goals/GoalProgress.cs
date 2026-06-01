using System;
using System.Diagnostics;
using Unity.Multiplayer.PlayMode;

[Serializable]
public class GoalProgress
{
    public GoalDefinition Goal;
    public LevelGoalDefinition LevelGoal;

    public int CurrentAmount;
    

    public bool IsCompleted()
    {
        return (CurrentAmount >= Goal.RequiredAmount);
    }
    

}