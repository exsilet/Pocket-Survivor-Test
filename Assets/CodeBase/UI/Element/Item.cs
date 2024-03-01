using CodeBase.Infrastructure.StaticData.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Element
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _countText;

        private ItemStaticData _itemData;
        private InventorySlot _slot;

        private float _weightItem;
        private int _currentCount;
        private int _useHealth = 1;
        private int _numberOfPurchasedCartridges;

        public InventorySlot Slot => _slot;
        public int CurrentCount => _currentCount;

        public void InitialiseItem(ItemStaticData newItem, InventorySlot inventorySlot)
        {
            _itemData = newItem;
            _currentCount = _itemData.MaxCount;
            _icon.sprite = _itemData.UIIcon;
            _countText.text = _itemData.MaxCount.ToString();
            _weightItem = _itemData.WeightOfOne * _itemData.MaxCount;
            _countText.gameObject.SetActive(_itemData.MaxCount > 1);
            _slot = inventorySlot;
        }

        public void UpdateSlot(InventorySlot slot)
        {
            _slot = slot;
        }

        public void OpenItemView()
        {
            _slot.OpenView(_weightItem);
        }

        public void UseHealth(GameObject player)
        {
            _itemData.PerformAction(player);
            _currentCount -= _useHealth;
            _countText.text = _currentCount.ToString();
        }

        public void UsedAmmo(int count)
        {
            _currentCount -= count;
            UpdateUIAmmo(_currentCount);
        }

        public void BuyAmmo()
        {
            if (_currentCount < _itemData.MaxCount)
            {
                _numberOfPurchasedCartridges = _itemData.MaxCount - _currentCount;
                _currentCount += _numberOfPurchasedCartridges;
                
                if (_currentCount > _itemData.MaxCount)
                {
                    _currentCount = _itemData.MaxCount;
                    UpdateUIAmmo(_currentCount);
                }
                
                UpdateUIAmmo(_currentCount);
            }
        }

        public void DeleteItem()
        {
            Destroy(gameObject);
        }

        private void UpdateUIAmmo(int count)
        {
            _weightItem = (count * _itemData.WeightOfOne);
            _countText.text = _currentCount.ToString();
        }
    }
}