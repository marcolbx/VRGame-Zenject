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
        private Vector3 _playerPosition => PlayerManager.instance.PlayerPosition;
        private bool _isChasing;
        private bool _isAlive => _enemy.CurrentHealth > 0;

        #region Animator Hashes
        private int _chaseHash = Animator.StringToHash("Chase");
        private int _hurtHash = Animator.StringToHash("Hurt");
        private int _atkDistanceHash = Animator.StringToHash("AttackDistance");
        private int _dieHash = Animator.StringToHash("Die");
        #endregion

        [Inject]
        public void Init(WeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        private void Start() {
        }

        private void Update() {
            if (!_isAlive)
                return;
            
            AIBehaviour();
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
            if (_enemy.CurrentHealth > 1)
            {
                if (IsCriticalHit())
                    _enemy.CurrentHealth -= 3;
                else
                    _enemy.CurrentHealth--;
            }
            else
            {
                _enemy.CurrentHealth = 0;
                Die();
            }
        }

        public void Die()
        {
            Destroy(_rigidBody);
            Destroy(_agent);

            _collider.enabled = false;
            _animator.SetTrigger(_dieHash);

            Destroy(gameObject, 5f);
        }

        private void AIBehaviour()
        {
            float distance = Vector3.Distance(_playerPosition, transform.position); //Distancia entre el zombie y el jugador
            Vector3 direction = _playerPosition - this.transform.position;  //Direccion (Vector3) del zombie al jugador
            float angle = Vector3.Angle(direction, this.transform.forward); //Angulo de vision


            if (distance < _enemy.VisionArea && angle < _enemy.AngleRadius)
            {
                _isChasing = true;
                Debug.Log("Entro en vision");
            }

            //Inicializando variables en el animator.
            if (_enemy.AttackDistance > distance)
            {
                Debug.Log("Entro en AttackDistance");
               // _animator.SetFloat(_atkDistanceHash, distance);
            }
            else
            {
               // _animator.SetFloat(_atkDistanceHash, 10f);
            }

            if (_enemy.AudibleArea > distance)
            {
                Debug.Log("Entro en AudibleArea");
                _isChasing = true;
                _agent.isStopped = false;
                _agent.SetDestination(_playerPosition);
            }
        }

        public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemy.AudibleArea);
    }
    }
}