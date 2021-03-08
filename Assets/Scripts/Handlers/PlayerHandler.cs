using Base.Controller;
using Base.Signal;
using UnityEngine;
using Zenject;

namespace Base.Handler
{
    public class PlayerHandler : MonoBehaviour
    {
        [SerializeField] private WeaponsHandler _weaponsHandler;
        [SerializeField] private ParticleSystem[] _muzzleFlash;
        [SerializeField] private AudioSource _hurtSound;
        [SerializeField] private GameObject _redHurtLight;
        private WeaponController _weaponController;
        private ControlsController _controlsController;
        private Camera _camera;
        private int _layerMask = 1 << 10;

        // This would cast rays only against colliders in layer 10.
        // But instead we want to collide against everything except layer 10. The ~ operator does this, it inverts a bitmask.

        [Inject]
        public void Init(WeaponController weaponController, SignalBus bus, ControlsController controlsController)
        {
            _weaponController = weaponController;
            _controlsController = controlsController;
            bus.Subscribe<PlayerDamaged>(PlayHurtSound);
        }

        private void Start() 
        {
            _camera = Camera.main;
            _layerMask = ~_layerMask;
        }

        private void Update() 
        {
            if (_weaponController.IsPlayerReloading)
                return;

            if (!_weaponController.CanShoot)
            {
                _weaponController.Reload();
                return;
            }

            if ((_controlsController.InputCondition() || Input.GetButtonDown("Fire1")) && _weaponController.HasAmmo)
            {
                RaycastHit hit;
                if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, Mathf.Infinity, _layerMask))  //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    EnemyHandler enemy = hit.transform.GetComponent<EnemyHandler>();
                    if (enemy != null)
                    {
                        _weaponsHandler.PlayGunShot();
                        ActivateMuzzleFlash();
                        _weaponController.Shoot();
                        enemy.TakeDamage();
                    }
                }
            }
        }

        private void ActivateMuzzleFlash()
        {
            foreach (var particleSystem in _muzzleFlash)
            {
                particleSystem.Play();
            }
        }

        private void PlayHurtSound()
        {
            _hurtSound.Play();
            _redHurtLight.SetActive(true);

            Invoke("DeactivateRedHurtLight", 1f);
        }

        private void DeactivateRedHurtLight()
        {
            _redHurtLight.SetActive(false);
        }
    }
}