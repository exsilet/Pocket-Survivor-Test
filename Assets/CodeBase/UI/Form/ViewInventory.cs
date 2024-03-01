using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Infrastructure.StaticData.Item;
using CodeBase.UI.Element;
using UnityEngine;

namespace CodeBase.UI.Form
{
    public class ViewInventory : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private List<InventorySlot> _slots;

        public List<InventorySlot> Slots => _slots;
        public Inventory Inventory => _inventory;

        public void UpdateNewUiItem(List<ItemStaticData> items)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (i < items.Count)
                {
                    _slots[i].AddItem(items[i]);
                }
                else
                {
                    break;
                }
            }
        }

        public void UpdateToLoad(int itemIndex, ItemStaticData item)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (i == itemIndex)
                {
                    _slots[i].AddItem(item);
                    break;
                }
            }
        }

        public void UpdateUiItemClothing(ItemStaticData item)
        {
            foreach (InventorySlot slot in _slots)
            {
                if (slot.ItemData == null)
                {
                    slot.AddItem(item);
                    break;
                }
            }
        }

        public void UpdateUiCartridges(ItemStaticData item, int count)
        {
            foreach (InventorySlot slot in _slots)
            {
                if (slot.ItemData == item)
                {
                    slot.UsedItemAmmo(count);
                    break;
                }
            }
        }
    }
}