using System.Collections.Generic;
using System.Globalization;
using CodeBase.Infrastructure.StaticData.Item;
using CodeBase.UI.Element;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Form
{
    public class ViewObject : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private Image _imageIcon;
        [SerializeField] private Image _imageDefence;
        [SerializeField] private TMP_Text _defenseText;
        [SerializeField] private TMP_Text _currentAmount;
        [SerializeField] private TMP_Text _weightText;
        [SerializeField] private ViewObject _panel;
        [SerializeField] private GameObject _itemView;
        [SerializeField] private Transform _equip;
        [SerializeField] private Transform _buy;
        [SerializeField] private Transform _treat;
        [SerializeField] private List<ItemStaticData> _staticDatas;

        private ItemStaticData _data;
        private InventorySlot _inventorySlot;

        public void OpenPanel(ItemStaticData itemData, InventorySlot slot, float weight)
        {
            foreach (ItemStaticData staticData in _staticDatas)
            {
                if (staticData == itemData)
                {
                    _data = staticData;
                    _inventorySlot = slot;
                }
            }


            _panel.gameObject.SetActive(true);
            _itemView.SetActive(true);
            TypeItem(_data);
            Show(itemData, weight);
        }

        public void ClosePanel()
        {
            _panel.gameObject.SetActive(false);
            _data = null;
            _inventorySlot = null;
        }

        public void BuyItemAmmo()
        {
            _inventorySlot.Item.BuyAmmo();
        }

        public void EquipClothes()
        {
            _inventorySlot.Equip();
        }

        public void Treat(GameObject player)
        {
            _inventorySlot.UseTreatment(player);
        }

        public void RemoveItem()
        {
            _inventorySlot.OnRemoveButton();
        }

        private void TypeItem(ItemStaticData data)
        {
            if (data.Equip)
            {
                _equip.gameObject.SetActive(true);
                _buy.gameObject.SetActive(false);
                _treat.gameObject.SetActive(false);
            }
            else if (data.Treat)
            {
                _equip.gameObject.SetActive(false);
                _buy.gameObject.SetActive(false);
                _treat.gameObject.SetActive(true);
            }
            else if (data.Buy)
            {
                _equip.gameObject.SetActive(false);
                _buy.gameObject.SetActive(true);
                _treat.gameObject.SetActive(false);
            }
        }

        private void Show(ItemStaticData data, float weight)
        {
            _textTitle.text = data.Name;
            _imageIcon.sprite = data.UIIcon;
            
            _currentAmount.text = _data.MaxCount.ToString();
            _currentAmount.text = $"{(float)_inventorySlot.Item.CurrentCount}/{data.MaxCount}";

            if (_data.Equip)
            {
                _defenseText.text = data._modifierData[0].Value.ToString();
                _imageDefence.gameObject.SetActive(data._modifierData[0].Value > 1);
                _defenseText.gameObject.SetActive(data._modifierData[0].Value > 1);
                _currentAmount.gameObject.SetActive(false);
            }
            else
            {
                _imageDefence.gameObject.SetActive(false);
                _defenseText.gameObject.SetActive(false);
                _currentAmount.gameObject.SetActive(data.MaxCount > 1);
            }

            _weightText.text = weight.ToString(CultureInfo.CurrentCulture);
        }
    }
}