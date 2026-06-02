using System.Collections.Generic;
using UnityEngine;

public class LootScrollView : MonoBehaviour
{
    [SerializeField]
    private LootSpawner spawner;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private LootItemButton prefab;


    private void Start()
    {
        
        Build();
    }

    private void Build()
{
    foreach (var item in AddressablePreloader.Loots)
    {
        Debug.Log("item: "+item);
        LootItemButton button = Instantiate( prefab, content);

        button.Initialize(item,spawner);
    }
}

    private void SetItemDefinition(LootItemButton button,ItemDefinition item)
    {
        var field = typeof(LootItemButton).GetField("itemDefinition",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        field.SetValue(button,item);
    }
}