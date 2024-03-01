using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHp;
        [SerializeField] private Transform _gameOver;

        private int _currentHp;
        private int _protectionHead;
        private int _protectionBody;

        public event UnityAction<int, int> HealthChanged;
        public event UnityAction<int, int> HealthTextChanged;
        public event UnityAction Died;

        public int CurrentHp
        {
            get => _currentHp;
            set => _currentHp = value;
        }

        public int MaxHp => _maxHp;

        private void Start()
        {
            _currentHp = _maxHp;
            _gameOver.gameObject.SetActive(false);
            HealthTextChanged?.Invoke(_currentHp, _maxHp);
            HealthChanged?.Invoke(_currentHp, _maxHp);
        }

        public void AddProtection(int count, bool head)
        {
            if (head)
            {
                _protectionHead = count;
            }
            else
            {
                _protectionBody = count;
            }
        }

        public void UseHealth(int count)
        {
            if (_currentHp < _maxHp)
            {
                _currentHp += count;
                
                if (_currentHp > _maxHp)
                {
                    _currentHp = _maxHp;
                }
            }
            
            HealthChanged?.Invoke(_currentHp, _maxHp);
            HealthTextChanged?.Invoke(_currentHp, _maxHp);
        }

        public void ApplyDamage(int damage, bool head)
        {
            if (head)
            {
                _currentHp -= damage - _protectionHead;
            }
            else
            {
                _currentHp -= damage - _protectionBody;
            }

            HealthChanged?.Invoke(_currentHp, _maxHp);
            HealthTextChanged?.Invoke(_currentHp, _maxHp);

            if (_currentHp <= 0)
                Die();
        }

        private void Die()
        {
            _gameOver.gameObject.SetActive(true);
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}