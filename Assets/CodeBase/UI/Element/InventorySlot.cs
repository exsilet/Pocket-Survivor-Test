using CodeBase.Hero;
using CodeBase.Infrastructure.StaticData.Item;
using CodeBase.UI.Form;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Element
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private GameObject _inventoryItemPrefab;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private ViewObject _viewObject;
        
        private ItemStaticData _itemData;
        private Item _item;

        public ItemStaticData ItemData => _itemData;
        public Item Item => _item;

        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                GameObject dropped = eventData.pointerDrag;
                DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                draggableItem.parentAfterDrag = transform;
                _itemData = draggableItem.Slot._itemData;
                _item = draggableItem.Slot._item;
                draggableItem.Slot._itemData = null;
                draggableItem.Slot._item = null;
            }
        }

        public void AddItem(ItemStaticData newItem)
        {
            if (_itemData == null)
            {
                _itemData = newItem;
                SpawnItem();
            }
        }

        public void OpenView(float weightItem)
        {
            _viewObject.OpenPanel(_itemData, this, weightItem);
        }

        public void Equip()
        {
            _inventory.EquipItem(_itemData);
            ClearSlot();
        }

        public void UsedItemAmmo(int count)
        {
            _item.UsedAmmo(count);
        }

        public void UseTreatment(GameObject player)
        {
            _item.UseHealth(player);
        }

        public void OnRemoveButton()
        {
            _inventory.Remove(_itemData);
            ClearSlot();
        }

        private void ClearSlot()
        {
            _itemData = null;
            _item.DeleteItem();
        }

        private void SpawnItem()
        {
            GameObject newItem = Instantiate(_inventoryItemPrefab, transform);
            _item = newItem.GetComponent<Item>();
            _item.InitialiseItem(_itemData, this);
        }
    }
}