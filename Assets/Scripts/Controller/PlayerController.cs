using Base.Handler;
using Base.Model;
using Base.Signal;
using Zenject;

namespace Base.Controller
{
    public class PlayerController : IDamageable
    {
        public Model.Player Player { get; private set; }
        private Model.PlayerStats _playerStats;
        private SignalBus _bus;
        private SceneHandler _sceneHandler;

        public PlayerController(Player player, SignalBus bus, PlayerStats playerStats, SceneHandler sceneHandler)
        {
            Player = player;
            _bus = bus;
            _playerStats = playerStats;
            _sceneHandler = sceneHandler;
        }

        public void TakeDamage()
        {
            if (Player.CurrentHealth > 0)
            {
                Player.CurrentHealth -= 1;
                _bus.Fire(new PlayerDamaged());
            }
            else
            {
                Player.CurrentHealth = 0;
                _sceneHandler.LoadGameOver();
            }
        }

        public void ObtainExperience()
        {
            Player.Experience += 1;
            _playerStats.TotalEnemiesKilled += 1;
            SaveProgress();
        }

        public void ObtainMoney(float money)
        {
            Player.Money += money;
        }

        private void SaveProgress()
        {
            SaveManager.Instance.SaveProgress();
        }

        public void ResetHealth()
        {
            Player.CurrentHealth = 3;
        }
    }
}