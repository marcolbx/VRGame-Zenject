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
        [SerializeField] private Image _crosshair;
        [SerializeField] private AudioSource _changingWeaponSound;
        private WeaponController _weaponController;

        [Inject]
        public void Init(WeaponController weaponController)
        {
            _weaponController = weaponController;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                EquipHandgun();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EquipShotgun();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
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

            _weaponController.ChangeToMachinegun();
        }
    }
}