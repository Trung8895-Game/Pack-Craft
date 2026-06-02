using System;
using System.Reflection.Emit;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class DownloadManager
{

     public static async UniTask
        DownloadLabel(string label)
    {
        await Addressables.DownloadDependenciesAsync(label).ToUniTask();
    }

    public static async UniTask DownloadLabels(
    string[] labels,
    Action<float> onProgress)
{
    var handle = Addressables.DownloadDependenciesAsync(labels,Addressables.MergeMode.Union);

    while (!handle.IsDone)
    {
        var status = handle.GetDownloadStatus();

        if (status.TotalBytes > 0)
        {
            onProgress?.Invoke((float)status.DownloadedBytes / status.TotalBytes);
        }

        await UniTask.Yield();
    }

    await handle.ToUniTask();

    onProgress?.Invoke(1f);
}

public static async UniTask<long>
        GetTotalDownloadSize(string[] labels)
    {
        long totalSize = 0;

        foreach (string label in labels)
        {
            long size =
                await Addressables.GetDownloadSizeAsync(label).ToUniTask();

            totalSize += size;
        }

        return totalSize;
    }
}