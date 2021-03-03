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
        public EnemyType ColorType;
        private const uint MaxHealth = 5;
        public float CurrentHealth;
        public float VisionArea { get; private set; } = 20f;
        public float AudibleArea { get; private set; } = 17f; //7
        public float AttackDistance { get; private set; } = 5f;
        public float AngleRadius { get; private set; } = 30f;
    }
}