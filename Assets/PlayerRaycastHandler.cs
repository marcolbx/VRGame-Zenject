using UnityEngine;

public class PlayerRaycastHandler : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    private void Update() 
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3000))
        {
            
        }
    }
}
