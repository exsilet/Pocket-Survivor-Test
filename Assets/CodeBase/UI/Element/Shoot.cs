using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.StaticData.Item;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Element
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private WeaponSlot _pistolButton;
        [SerializeField] private WeaponSlot _automaticButton;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private float _attackDistance;
        [SerializeField] private Transform _warningWeapon;
        
        private bool _selectPistol;
        private bool _selectAutomatic;
        private RaycastHit2D hit;
        
        private void Start()
        {
            _selectPistol = true;
            _pistolButton.GetComponent<Button>().image.color = Color.green;
            _warningWeapon.gameObject.SetActive(false);
        }
        
        private void FixedUpdate()
        {
            hit = Physics2D.Raycast(transform.position, transform.right, _attackDistance, _enemyLayerMask);

            Debug.DrawRay(transform.position, transform.right * _attackDistance, Color.red);
        }

        public void SelectPistol()
        {
            if (_selectPistol == false)
            {
                _selectPistol = true;
                _pistolButton.GetComponent<Button>().image.color = Color.green;
                _automaticButton.GetComponent<Button>().image.color = Color.white;
            }
            else
            {
                _selectPistol = false;
                _pistolButton.GetComponent<Button>().image.color = Color.white;
            }
            
            if (_selectAutomatic)
            {
                _selectAutomatic = false;
            }
        }

        public void SelectAutomatic()
        {
            if (_selectAutomatic == false)
            {
                _selectAutomatic = true;
                _automaticButton.GetComponent<Button>().image.color = Color.green;
                _pistolButton.GetComponent<Button>().image.color = Color.white;
            }
            else
            {
                _selectAutomatic = false;
                _automaticButton.GetComponent<Button>().image.color = Color.white;
            }

            if (_selectPistol)
            {
                _selectPistol = false;
            }
        }

        public void ShootButton()
        {
            if (_selectPistol && !_selectAutomatic)
            {
                ShootWeapon(_pistolButton);
            }
            else if (_selectAutomatic && !_selectPistol)
            {
                ShootWeapon(_automaticButton);
            }
            else
            {
                _warningWeapon.gameObject.SetActive(true);
                Debug.Log(" NO WEAPON SELECTED ");
            }
        }

        private void ShootWeapon(WeaponSlot weaponSlot)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent<EnemyWithDamage>(out EnemyWithDamage enemy))
                {
                    foreach (ItemStaticData item in _inventory.Items)
                    {
                        if (item == weaponSlot.Weapon.Bullet)
                        {
                            enemy.TakeDamage(weaponSlot.Weapon.Damage);
                            weaponSlot.AmmoReduction(weaponSlot.Weapon);
                        }
                    }
                }
            }
        }
    }
}
