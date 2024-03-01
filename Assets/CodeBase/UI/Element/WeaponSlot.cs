using CodeBase.Hero;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Item;
using UnityEngine;

namespace CodeBase.UI.Element
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Inventory _inventory;

        public Weapon Weapon => _weapon;
        
        public void AmmoReduction(Weapon shootWeapon)
        {
            foreach (ItemStaticData itemData in _inventory.Items)
            {
                if (itemData == _weapon.Bullet)
                {
                    _inventory.UserCartridges(shootWeapon, shootWeapon.Ammo);
                }
            }
        }
    }
}