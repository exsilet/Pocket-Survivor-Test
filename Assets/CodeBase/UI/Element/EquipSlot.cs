using CodeBase.Infrastructure.StaticData.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Element
{
    public class EquipSlot : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _protectionText;

        private bool _head;
        private ItemStaticData _item;

        public ItemStaticData ItemData => _item;
        public bool Head => _head;
        
        public void AddItem(ItemStaticData newItem)
        {
            if (_item == null)
            {
                _item = newItem;
                _icon.sprite = _item.UIIcon;
                _protectionText.text = _item._modifierData[0].Value.ToString();
                EquipHead();
            }
        }

        public void ReplaceClothes(ItemStaticData replaceItem)
        {
            if (_item != null)
            {
                _item = replaceItem;
                _icon.sprite = _item.UIIcon;
                _protectionText.text = _item._modifierData[0].Value.ToString();
                EquipHead();
            }
        }

        private void EquipHead()
        {
            _head = _item.Head;
        }
    }
}