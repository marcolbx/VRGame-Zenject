using UnityEngine;

public class PlayerMovementVR : MonoBehaviour
{
    [SerializeField] private PlayerMotor _playerMotor;
    [SerializeField] private Transform _camera;
    [SerializeField] private float speed = 3f;
    private float _movebool = 0;
    private bool _mobileInput => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    private bool _editorInput => Input.GetButtonDown("Fire1");

    private void Update()
    {
        if (InputCondition())
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3000))
            {
                if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Object"))
                {
                    _movebool = 0;
                }
                else
                {
                    if (_movebool == 0)
                        _movebool = 1;
                    else
                        _movebool = 0;
                }
            }
        }

        ApplyMovement();
    }

    private bool InputCondition()
    {
#if UNITY_EDITOR
            return  _editorInput;
#else
            return _mobileInput;
#endif
    }

    private void ApplyMovement()
    {
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 _velocity2 = forward * speed * _movebool;

        _playerMotor.Move(_velocity2);

        float _yRot = _camera.eulerAngles.x;
        Vector3 _rotation = new Vector3(_yRot, 0f, 0f);
    }
}
