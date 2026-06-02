using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableTest : MonoBehaviour
{
    private async void Start()
    {
        var handle =
            Addressables.LoadAssetAsync<ItemDefinition>(
                "Wood");

        ItemDefinition item =
            await handle.ToUniTask();

        Debug.Log(item.name);
    }
}