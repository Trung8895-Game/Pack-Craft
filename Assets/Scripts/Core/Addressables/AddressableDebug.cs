using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableDebug : MonoBehaviour
{
    [ContextMenu("Clear Addressables Cache")]
    public void ClearCache()
    {
        Addressables.ClearDependencyCacheAsync("Items");

        Caching.ClearCache();

        Debug.Log("Cache Cleared");
    }
}