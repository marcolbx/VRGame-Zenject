using Base.Model;
using Base.Signal;
using Zenject;

namespace Base.Controller
{
    public class PlayerController : IDamageable
    {
        public Model.Player Player { get; private set; }
        private SignalBus _bus;

        public PlayerController(Player player, SignalBus bus)
        {
            Player = player;
            _bus = bus;
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
                _bus.Fire(new PlayerDied());
            }
        }

        public void ObtainExperience()
        {
            Player.Experience += 1;
        }
    }
}