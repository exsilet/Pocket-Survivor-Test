using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData.Item;
using CodeBase.UI.Element;
using CodeBase.UI.Form;
using UnityEngine;

namespace CodeBase.Hero
{
    public class EquipmentForThePlayer : MonoBehaviour
    {
        [SerializeField] private List<EquipSlot> _equipSlots;
        [SerializeField] private ViewInventory _view;
        [SerializeField] private GameObject _player;

        private List<ItemStaticData> _playerEquipment = new();

        private readonly int _numberSlotHead = 0;
        private readonly int _numberSlotBody = 1;

        public List<EquipSlot> EquipSlots => _equipSlots;

        private void Start()
        {
            for (int i = 0; i < _equipSlots.Count; i++)
            {
                _playerEquipment.Add(null);
            }
        }

        public void EquipItem(ItemStaticData data)
        {
            if (data.Head)
            {
                NewItemGear(data, _numberSlotHead);
            }
            else
            {
                NewItemGear(data, _numberSlotBody);
            }
        }

        private void NewItemGear(ItemStaticData data, int numberGearSlot)
        {
            if (_playerEquipment[numberGearSlot] != null)
            {
                _view.UpdateUiItemClothing(_playerEquipment[numberGearSlot]);
                _playerEquipment.RemoveAt(numberGearSlot);
                _playerEquipment.Insert(numberGearSlot, data);
                _equipSlots[numberGearSlot].ReplaceClothes(data);
                data.PerformAction(_player);
            }
            else
            {
                _playerEquipment.Insert(numberGearSlot, data);;
                _equipSlots[numberGearSlot].AddItem(data);
                data.PerformAction(_player);
            }
        }
    }
}