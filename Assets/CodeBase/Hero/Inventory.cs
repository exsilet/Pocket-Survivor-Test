using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Item;
using CodeBase.UI.Form;
using UnityEngine;

namespace CodeBase.Hero
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ViewInventory _viewInventory;
        [SerializeField] private List<ItemStaticData> _startItem = new();
        [SerializeField] private EquipmentForThePlayer _equipment;

        [HideInInspector] public List<ItemStaticData> Items = new();

        private int _maxSlot = 29;

        private void Start()
        {
            foreach (ItemStaticData item in _startItem)
            {
                AddItemStart(item);
            }
        }

        public void Remove(ItemStaticData item)
        {
            Items.Remove(item);
        }

        public bool Add(ItemStaticData item)
        {
            if (item != null)
            {
                if (Items.Count >= _maxSlot)
                {
                    Debug.Log("Not enough room.");
                    return false;
                }

                Items.Add(item);
                _viewInventory.UpdateNewUiItem(Items);
            }

            return true;
        }

        public void RemoveItemFromSlot(int itemsSlot)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (i == itemsSlot)
                {
                    //_viewInventory.Slots[i].OnRemoveButton();
                    Remove(Items[i]);
                }
            }
        }

        public void AddItemToSlot(ItemStaticData item, int itemIndex)
        {
            _viewInventory.UpdateToLoad(itemIndex, item);
        }

        public void EquipItem(ItemStaticData item)
        {
            if (item.Equip)
            {
                _equipment.EquipItem(item);
            }
        }

        public void UserCartridges(Weapon weapon, int count)
        {
            foreach (var itemData in Items)
            {
                if (itemData == weapon.Bullet)
                {
                    _viewInventory.UpdateUiCartridges(itemData, count);
                }
            }
        }

        private void AddItemStart(ItemStaticData item)
        {
            Items.Add(item);
            _viewInventory.UpdateNewUiItem(Items);
        }
    }
}