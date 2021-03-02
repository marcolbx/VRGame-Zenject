
using Base.Model;
using Base.Signal;
using Zenject;

namespace Base.Controller
{
    public class PlayerController : IDamageable
    {
        private Model.Player _player;
        private SignalBus _bus;

        public PlayerController(Player player, SignalBus bus)
        {
            _player = player;
            _bus = bus;
        }

        public void TakeDamage()
        {
            if (_player.CurrentHealth > 0)
            {
                _player.CurrentHealth -= 1;
                _bus.Fire(new PlayerDamaged());
            }
            else
            {
                _player.CurrentHealth = 0;
                _bus.Fire(new PlayerDied());
            }
        }
    }
}