using System;
using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Infrastructure.StaticData.Item;
using CodeBase.UI.Element;
using CodeBase.UI.Form;

namespace CodeBase.SaveData
{
    [Serializable]
    public class DataBase
    {
        public int Health;
        public int EnemyHealth;
        public string[] ItemInventory;
        public string[] EquipItemInventory;

        public DataBase(HeroHealth heroHealth, ViewInventory viewInventory, EquipmentForThePlayer equipPlayer)
        {
            Health = heroHealth.CurrentHp;

            ItemInventory = new string [viewInventory.Slots.Count];
            EquipItemInventory = new string [equipPlayer.EquipSlots.Count];

            for (int i = 0; i < viewInventory.Slots.Count; i++)
            {
                if(viewInventory.Slots[i].Item != null)
                    ItemInventory[i] = viewInventory.Slots[i].Item.name;
            }
            
            for (int i = 0; i < equipPlayer.EquipSlots.Count; i++)
            {
                if(equipPlayer.EquipSlots[i].ItemData != null)
                    EquipItemInventory[i] = equipPlayer.EquipSlots[i].ItemData.name;
            }
        }
        
        [Serializable]
        public class ItemSaveData
        {
            public ItemTypeID ItemTypeID;
            public int Value;

            public ItemSaveData(ItemTypeID itemTypeID, int itemCurrentCount)
            {
                ItemTypeID = itemTypeID;
                Value = itemCurrentCount;
            }
        }
    }
}