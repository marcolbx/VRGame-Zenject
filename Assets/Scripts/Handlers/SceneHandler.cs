using System.Threading.Tasks;
using Base.Controller;
using UnityEngine.SceneManagement;
using Zenject;

namespace Base.Handler
{
    public class SceneHandler
    {
        private PlayerController _playerController;
        private PlayerStatsController _playerStatsController;
        private WeaponController _weaponController;

        [Inject]
        public void Init(PlayerController playerController, PlayerStatsController playerStatsController, WeaponController weaponController)
        {
            _playerController = playerController;
            _playerStatsController = playerStatsController;
            _weaponController = weaponController;
        }

        public void ReturnToMain()
        {
            SceneManager.LoadScene(0);
        }

        public async void LoadSurvival()
        {
            _playerController.ResetHealth();
            _playerStatsController.ResetEphemeral();
            _weaponController.InitialAmmo();

            await Task.Delay(4000);
            SceneManager.LoadScene(1);
        }

        public void LoadGameOver()
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
