using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class AddressableManager
{
    public static async UniTask<T>
        LoadAssetAsync<T>(
            string key)
        where T : Object
    {
        return await
            Addressables
                .LoadAssetAsync<T>(key)
                .ToUniTask();
    }

    public static async UniTask ReleaseAssetAsync<T>(
        T asset)
        where T : Object
    {
        Addressables.Release(asset);

        await UniTask.CompletedTask;
    }
}