using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootItemButton
    : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI name;

    private ItemDefinition _definition;

    private LootSpawner _spawner;

    public void Initialize(ItemDefinition definition, LootSpawner spawner)
    {
        _definition = definition;
        _spawner = spawner;

        icon.sprite = definition.Icon;
        name.text= definition.name;

        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        _spawner.SpawnLoot(_definition);
    }
}