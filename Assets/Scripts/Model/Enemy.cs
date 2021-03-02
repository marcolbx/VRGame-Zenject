using UnityEngine;

namespace Base.Model
{
    public enum EnemyType
    {
        Blue,
        Red,
        Green
    }

    public class Enemy : MonoBehaviour
    {
        public EnemyType ColorType { get; private set; }
        private const uint MaxHealth = 5;
        public uint CurrentHealth;
        public float MovementSpeed { get; private set; }
    }
}