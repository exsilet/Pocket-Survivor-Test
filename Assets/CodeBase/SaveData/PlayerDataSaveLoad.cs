using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Hero;
using CodeBase.Infrastructure.StaticData.Item;
using CodeBase.UI.Form;
using UnityEngine;

namespace CodeBase.SaveData
{
    public class PlayerDataSaveLoad : MonoBehaviour
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private ViewInventory _viewInventory;
        [SerializeField] private EquipmentForThePlayer _equipPlayer;

        private const string StaticDataItemPath = "StaticData";

        private Dictionary<ItemTypeID, ItemStaticData> _items;

        private void Start()
        {
            Load();
        }

        public void SavePlayer()
        {
            BinarySavingSystem.SavePlayer(_heroHealth, _viewInventory, _equipPlayer);
        }

        public void LoadPlayer()
        {
            DataBase data = BinarySavingSystem.LoadPlayer();

            _heroHealth.CurrentHp = data.Health;

            for (int i = 0; i < _viewInventory.Slots.Count; i++)
            {
                if (data.ItemInventory[i] != null)
                {
                    _viewInventory.Inventory.RemoveItemFromSlot(i);
                    //ItemStaticData item = Resources.Load<ItemStaticData>(StaticDataItemPath);
                    ItemStaticData item = Resources.Load<ItemStaticData>($"ScriptableObjects/{data.ItemInventory[i]}");
                    _viewInventory.Inventory.AddItemToSlot(item, i);
                }
                else
                {
                    _viewInventory.Inventory.RemoveItemFromSlot(i);
                }
            }
        }

        private void Load()
        {
            _items = Resources
                .LoadAll<ItemStaticData>(StaticDataItemPath)
                .ToDictionary(x => x.ItemTypeID, x => x);
        }

        private ItemStaticData ForTower(ItemTypeID typeID) =>
            _items.TryGetValue(typeID, out ItemStaticData staticData) 
                ? staticData 
                : null;
    }
}