using System.Collections;
using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Infrastructure.StaticData.Item;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Enemy
{
    public class EnemyWithDamage : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private List<ItemStaticData> _itemDatas;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private EnemyWeapon _weapon;
        
        private int _currentHealth;
        
        public event UnityAction<int, int> EnemyHealthChanged;
        public event UnityAction<int, int> EnemyHealthTextChanged;
        
        private void Start()
        {
            EnemyHealthTextChanged?.Invoke(_currentHealth, _health);
        }
        
        private void Awake() => _currentHealth = _health;
        
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            EnemyHealthChanged?.Invoke(_currentHealth, _health);
            EnemyHealthTextChanged?.Invoke(_currentHealth, _health);
            
            _weapon.ApplyDamage();

            if (_currentHealth <= 0)
            {
                StartCoroutine(StartDie());
            }
        }
        
        private IEnumerator StartDie()
        {
            ItemDrop();

            Destroy(gameObject, 0.3f);
            yield break;
        }
        private void ItemDrop()
        {
            int randomIndex = Random.Range(0, _itemDatas.Count);
            ItemStaticData data = _itemDatas[randomIndex];

            _inventory.Add(data);
        }
    }
}