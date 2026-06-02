using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AddressablePreloader : MonoBehaviour
{
   public static List<ItemDefinition> Items = new List<ItemDefinition>();
   public static List<ItemDefinition> Loots = new List<ItemDefinition>();
   public static CraftingDatabase craftingDatabase;
   public static LevelDatabase levelDatabase;

   private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public static async UniTask Preload()
    {
        ItemDefinition Item;
        Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.ArrowItem);
            Items.Add(Item);

        Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.AxeItem);
            Items.Add(Item);
            
            Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.BowItem);
            Items.Add(Item);

            Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.CapeItem);
            Items.Add(Item);

            Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.CrownItem);
            Items.Add(Item);
            
            Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.KeyItem);
            Items.Add(Item);

            Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.NeckItem);
            Items.Add(Item);

            Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.RingItem);
            Items.Add(Item);

            Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.ShieldItem);
            Items.Add(Item);

            Item = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.SwordItem);
            Items.Add(Item);



             ItemDefinition Loot;
            Loot = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.BoneLoot);
            Loots.Add(Loot);

            Loot = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.GoldLoot);
            Loots.Add(Loot);

            Loot = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.GemLoot);
            Loots.Add(Loot);

            Loot = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.LeatherLoot);
            Loots.Add(Loot);

            Loot = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.SilverLoot);
            Loots.Add(Loot);

            Loot = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.StoneLoot);
            Loots.Add(Loot);

            Loot = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.WoodLoot);
            Loots.Add(Loot);

            Loot = await AddressableManager
                .LoadAssetAsync<ItemDefinition>(
                    AddressableKeys.WoodStickLoot);
            Loots.Add(Loot);

            
            craftingDatabase= await AddressableManager
                .LoadAssetAsync<CraftingDatabase>(
                    AddressableKeys.CraftingDatabase);
                    
            levelDatabase = await AddressableManager
                .LoadAssetAsync<LevelDatabase>(
                    AddressableKeys.LevelDatabase);

        Debug.Log(
            "Preload Complete");

    }
   
}