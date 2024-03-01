using CodeBase.Infrastructure.StaticData.Item;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "StaticData/Weapon")]
    public class Weapon : ScriptableObject
    {
        public string Name;
        [Range(1f, 100f)] public int Damage;
        [Range(1f, 5f)] public int Ammo;
        public ItemStaticData Bullet;
    }
}