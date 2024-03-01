using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] public LayerMask _playerLayerMask;
        [SerializeField] public float _attackDistance;

        private RaycastHit2D hit;
        private bool _head = true;

        private void FixedUpdate()
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, _attackDistance, _playerLayerMask);

            Debug.DrawRay(transform.position, -transform.right * _attackDistance, Color.red);
        }

        public void ApplyDamage()
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent<HeroHealth>(out HeroHealth player))
                {
                    if (_head)
                    {
                        player.ApplyDamage(_damage, _head);
                        _head = false;
                    }
                    else
                    {
                        player.ApplyDamage(_damage, _head);
                        _head = true;
                    }
                }
            }
        }
    }
}