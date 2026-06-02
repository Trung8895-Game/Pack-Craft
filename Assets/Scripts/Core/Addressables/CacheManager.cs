using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public static class CacheManager
{
    public static async UniTask
        ClearCache()
    {
        await Addressables
            .CleanBundleCache()
            .ToUniTask();
    }
}