using UnityEngine;

namespace Base.Controller
{
    [CreateAssetMenu(fileName = "Ranks", menuName = "ScriptableObjects/Rank", order = 1)]
    public class Ranks : ScriptableObject
    {
        public string[] Rank;
        public Sprite[] RankIcon;
    }
}