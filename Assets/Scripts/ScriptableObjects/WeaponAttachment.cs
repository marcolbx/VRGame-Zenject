using Base.Model;
using UnityEngine;

namespace Base.Controller
{
    [CreateAssetMenu(fileName = "WeaponAttachment", menuName = "ScriptableObjects/WeaponAttachment", order = 1)]
    public class WeaponAttachment : ScriptableObject
    {
        public string Name;
        public float Cost;
        public GunType Gun;
        public Trait Trait;
    }
}