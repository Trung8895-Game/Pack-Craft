using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootItemButton
    : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private TextMeshProUGUI name;

    private ItemDefinition _definition;

    private LootSpawner _spawner;

    public void Initialize(ItemDefinition definition, LootSpawner spawner)
    {
        _definition = definition;
        _spawner = spawner;

        _icon.sprite = definition.Icon;
        name.text= definition.name;

        button.onClick.AddListener(OnClicked);
    }

    private async void OnClicked()
    {
        await _spawner.SpawnLootAsync(_definition.AddressableKey);
    }
}