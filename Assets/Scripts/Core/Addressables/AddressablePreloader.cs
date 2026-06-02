using Cysharp.Threading.Tasks;
using UnityEditor.Build.Content;
using UnityEngine;

public class AddressablePreloader : MonoBehaviour
{
   
    public async UniTask Preload()
    {
        await UniTask.WhenAll(
            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.ArrowItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.AxeItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.BoneLoot),
            
            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.BowItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.CapeItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.CrownItem),
            
            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.GemLoot),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.GoldLoot),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.KeyItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.LeatherLoot),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.NeckItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.RingItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.ShieldItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.SilverLoot),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.StoneLoot),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.SwordItem),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.WoodLoot),

            AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.WoodStickLoot),

            AddressableManager
                .LoadAssetAsync<CraftingDatabase>(
                    AddressableKeys.CraftingDatabase),
                    
                    AddressableManager
                .LoadAssetAsync<LevelDatabase>(
                    AddressableKeys.LevelDatabase)
                    );

        Debug.Log(
            "Preload Complete");
    }
   
}