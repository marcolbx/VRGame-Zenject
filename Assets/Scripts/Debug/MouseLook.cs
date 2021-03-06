using UnityEngine;

public class MouseLook : MonoBehaviour
{
    
#if UNITY_EDITOR
    public float MouseSensitivity = 100f;
    public Transform PlayerBody;
    private float _xRotation = 0f;
    private float _yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        _yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
#endif
}
