using System.Collections.Generic;
using Base.Controller;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Base.Handler
{
    public class WeaponsHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _weapons = new List<GameObject>(3);
        [SerializeField] private List<Sprite> _crosshairs = new List<Sprite>(3);
        [SerializeField] private List<AudioClip> _gunShots = new List<AudioClip>(3);
        [SerializeField] private Image _crosshair;
        [SerializeField] private AudioSource _changingWeaponSound;
        [SerializeField] private AudioSource _shootingSound;
        private WeaponController _weaponController;

        [Inject]
        public void Init(WeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        private void Start() {
            _shootingSound.clip = _gunShots[0];
        }

        private void Update() {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                if (_weapons[0].activeSelf)
                    return;

                _changingWeaponSound.Play();
                EquipHandgun();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                if (_weapons[1].activeSelf)
                    return;

                _changingWeaponSound.Play();
                EquipShotgun();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                if (_weapons[2].activeSelf)
                    return;

                _changingWeaponSound.Play();
                EquipMachinegun();
            }
        }

        public void EquipHandgun()
        {
            for (int i = 0; i < _weapons.Count; i ++)
            {
                if (i == 0)
                {
                    _weapons[i].SetActive(true);
                    _crosshair.sprite = _crosshairs[i];
                }
                else
                    _weapons[i].SetActive(false);
            }

            _shootingSound.clip = _gunShots[0];
            _weaponController.ChangeToHandgun();
        }
        public void EquipShotgun()
        {
            for (int i = 0; i < _weapons.Count; i ++)
            {
                if (i == 1)
                {
                    _weapons[i].SetActive(true);
                    _crosshair.sprite = _crosshairs[i];
                }
                else
                    _weapons[i].SetActive(false);
            }

            _shootingSound.clip = _gunShots[1];
            _weaponController.ChangeToShotgun();
        }

        public void EquipMachinegun()
        {
            for (int i = 0; i < _weapons.Count; i ++)
            {
                if (i == 2)
                {
                    _weapons[i].SetActive(true);
                    _crosshair.sprite = _crosshairs[i];
                }
                else
                    _weapons[i].SetActive(false);
            }

            _shootingSound.clip = _gunShots[2];
            _weaponController.ChangeToMachinegun();
        }

        public void PlayGunShot()
        {
            _shootingSound.Play();
        }
    }
}