using UnityEngine;

namespace Base.Controller
{
    [CreateAssetMenu(fileName = "WeaponAttachments", menuName = "ScriptableObjects/WeaponAttachments", order = 1)]
    public class WeaponAttachments : ScriptableObject
    {
        public WeaponAttachment[] Attachments;
    }
}
