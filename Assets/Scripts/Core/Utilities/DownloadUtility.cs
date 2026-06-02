using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public static class DownloadUtility
{
    public static async UniTask<long>
        GetDownloadSize(
            string label)
    {
        return await
            Addressables
                .GetDownloadSizeAsync(
                    label)
                .ToUniTask();
    }
}