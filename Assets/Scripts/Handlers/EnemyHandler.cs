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
        [SerializeField] private AudioSource _hurtAudio;
        [SerializeField] private AudioSource _dieAudio;
        private Vector3 _playerPosition => PlayerManager.instance.PlayerPosition;
        private bool _isAlive => _enemy.CurrentHealth > 0;
        private bool _isChasing;
        private bool _isHurt;
        private bool _isDefending;
        private bool _isFarAway;
        #region Zenject
        private WeaponController _weaponController;
        private PlayerController _playerController;
        #endregion

        #region Animator Hashes
        private int _chaseHash = Animator.StringToHash("Chase");
        private int _hurtHash = Animator.StringToHash("Hurt");
        private int _defendHash = Animator.StringToHash("Defend");
        private int _atkDistanceHash = Animator.StringToHash("Attack");
        private int _dieHash = Animator.StringToHash("Die");
        #endregion

        [Inject]
        public void Init(WeaponController weaponController, PlayerController playerController)
        {
            _weaponController = weaponController;
            _playerController = playerController;
        }

        private void Start() {
            _agent.stoppingDistance = _enemy.AttackDistance - 1;
        }

        private void Update() {
            if (!_isAlive || _isHurt || _isDefending)
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

        public void InflictDamage()
        {
            _playerController.TakeDamage();
        }

        public void TakeDamage()
        {
            _hurtAudio.Play();
            _isHurt = true;
            _agent.isStopped = true;
            _animator.ResetTrigger(_atkDistanceHash);

            if (_isAlive)
            {
                _enemy.CurrentHealth -= CalculateDamageToReceive();

                if (IsCriticalHit())
                {
                    _animator.SetTrigger(_defendHash);
                    _isDefending = true;
                }
                else
                    _animator.SetTrigger(_hurtHash);
            }
            
            if(!_isAlive)
            {
                _enemy.CurrentHealth = 0;
                Die();
            }
        }

        private float CalculateDamageToReceive()
        {
            float damage;
            if (IsCriticalHit())
            {
                damage = _weaponController.CurrentGun.Damage * 2;
            }
            else
            {
                damage = _weaponController.CurrentGun.Damage;
            }

            if(_isDefending)
                damage = damage / 2;

            return damage;
        }

        public void IsNotHurt()
        {
            if (!_isAlive)
                return;

            _isHurt = false;

            if (!_isDefending)
                _agent.isStopped = false;
        }

        public void StopDefenseMode()
        {
            if(!_isAlive)
                return;

            _isDefending = false;
            _agent.isStopped = false;
        }

        public void Die()
        {
            _dieAudio.Play();
            Destroy(_rigidBody);
            Destroy(_agent);

            _collider.enabled = false;
            _animator.ResetTrigger(_hurtHash);
            _animator.ResetTrigger(_atkDistanceHash);
            _animator.ResetTrigger(_chaseHash);
            _animator.ResetTrigger(_defendHash);

            _animator.SetTrigger(_dieHash);
            _playerController.ObtainExperience();
            Destroy(gameObject, 5f);
        }

        private void AIBehaviour()
        {
            float distance = Vector3.Distance(_playerPosition, transform.position); //Distancia entre el zombie y el jugador
            Vector3 direction = _playerPosition - this.transform.position;  //Direccion (Vector3) del zombie al jugador
            float angle = Vector3.Angle(direction, this.transform.forward); //Angulo de vision

            if (distance > _enemy.VisionArea && distance > _enemy.AudibleArea)
            {
                if (_isFarAway)
                    return;

                _isFarAway = true;
                _isChasing = false;
                _agent.isStopped = true;
                _animator.SetBool(_chaseHash, _isChasing);
                return;
            }
            else
                _isFarAway = false;

            if (distance < _enemy.VisionArea && angle < _enemy.AngleRadius)
            {
                Debug.Log("Vision");
                _isChasing = true;
            }

            if (_enemy.AttackDistance > distance)
            {
                Debug.Log("AttackDistance");
                _isChasing = false;
               _animator.SetTrigger(_atkDistanceHash);
            }
            else
            {
               _animator.ResetTrigger(_atkDistanceHash);
            }

            if (_enemy.AudibleArea > distance && distance > _enemy.AttackDistance)
            {
                Debug.Log("AudibleArea");
                _isChasing = true;
                _agent.isStopped = false;
                _agent.SetDestination(_playerPosition);
            }

            _animator.SetBool(_chaseHash, _isChasing);
        }
    }
}