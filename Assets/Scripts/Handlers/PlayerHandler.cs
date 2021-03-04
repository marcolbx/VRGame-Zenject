using Base.Controller;
using UnityEngine;
using Zenject;

namespace Base.Handler
{
    public class PlayerHandler : MonoBehaviour
    {
        [SerializeField] private WeaponsHandler _weaponsHandler;
        private WeaponController _weaponController;
        private Camera _camera;
        private int _layerMask;

        [Inject]
        public void Init(WeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        private void Start() {
            _camera = Camera.main;
        }

        private void Update() {
            if (_weaponController.IsPlayerReloading)
                return;

            if (!_weaponController.CanShoot())
            {
                _weaponController.Reload();
                return;
            }

            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, Mathf.Infinity))  //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    EnemyHandler enemy = hit.transform.GetComponent<EnemyHandler>();
                    if (enemy != null)
                    {
                        _weaponsHandler.PlayGunShot();
                        _weaponController.Shoot();
                        enemy.TakeDamage();
                    }
                }
            }
        }
    }
}