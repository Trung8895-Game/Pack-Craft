using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGoalUI : MonoBehaviour
{
    [SerializeField]
    private LevelGoalController controller;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private GoalEntryUI prefab;

    public GoalEntryUI entry {set;get;}
    public TutorialUI tutorial;

    public GoalProgress currentProgress{set;get;}

    private void Start()
    {
     
        
    }
    private void Update()
    {
        Refresh();
    }

     private void build()
    {
        entry = Instantiate(prefab,content);
        entry.Bind(currentProgress);
        tutorial.Bind(currentProgress);
        
    }
    public void Build()
    {
        build();
    }
    private void Refresh()
    {
        foreach (var entry in GetComponentsInChildren<GoalEntryUI>())
        {
            entry.Refresh();
        }
    }
}