using Base.Controller;
using Base.Model;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Base.Handler
{
    public class EnemyHandler : MonoBehaviour, IDamageable
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidBody;
        private WeaponController _weaponController;

        [Inject]
        public void Init(WeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        private void Start() {
            _animator.SetTrigger("Die");
        }

        private bool IsCriticalHit()
        {
            if (_weaponController.CurrentGun.GunType == GunType.Handgun && _enemy.ColorType == EnemyType.Red)
            {
                return true;
            }
            else if (_weaponController.CurrentGun.GunType == GunType.Shotgun && _enemy.ColorType == EnemyType.Green)
            {
                return true;
            }
            else if (_weaponController.CurrentGun.GunType == GunType.Machinegun && _enemy.ColorType == EnemyType.Blue)
            {
                return true;
            }

            return false;
        }

        public void TakeDamage()
        {
            if (_enemy.CurrentHealth > 0)
            {
                if (IsCriticalHit())
                    _enemy.CurrentHealth -= 3;
                else
                    _enemy.CurrentHealth--;
            }
            else
                Die();
        }

        public void Die()
        {
            // TODO die method
        }
    }
}