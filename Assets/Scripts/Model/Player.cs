namespace Base.Model
{
    public class Player
    {
        private const uint _maxHealth = 10;
        public uint CurrentHealth;
        public uint Speed {get; private set;} = 3;
    }
}