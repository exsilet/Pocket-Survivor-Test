using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Item
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "StaticData/Item")]
    public class ItemStaticData : ScriptableObject, IItemAction
    {
        public string Name;
        public bool Stackable = true;
        [Range(1f, 100f)] public int MaxCount;
        [Range(0.01f, 20f)] public float WeightOfOne;
        
        public bool Equip;
        public bool Head;
        public bool Treat;
        public bool Buy;
        public Sprite UIIcon;

        public ItemTypeID ItemTypeID;
        
        public List<ModifierData> _modifierData = new();

        public string ActionName => "Consume";
        
        public bool PerformAction(GameObject character)
        {
            foreach (ModifierData data in _modifierData)
            {
                data.StatModifier.AffectCharacter(character, data.Value, Head);
            }

            return true;
        }
        
        [Serializable]
        public class ModifierData
        {
            public CharactersStatModifierSO StatModifier;
            public int Value;
        }
    }
}