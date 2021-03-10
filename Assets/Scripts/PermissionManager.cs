using UnityEngine;
using UnityEngine.Android;

public class PermissionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
            #endif
    }
}
