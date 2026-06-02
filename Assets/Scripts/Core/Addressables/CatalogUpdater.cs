using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class CatalogUpdater
{
    public static async UniTask<bool>
        CheckForUpdates()
    {
        var checkHandle = Addressables.CheckForCatalogUpdates();

        var catalogs = await checkHandle.ToUniTask();

        if (catalogs == null ||
            catalogs.Count == 0)
        {
            return false;
        }

        var updateHandle = Addressables.UpdateCatalogs(catalogs);

        await updateHandle.ToUniTask();

        return true;
    }
}