using UnityEngine;
using Zenject;
using Base.Controller;

public class PlayerMovementVR : MonoBehaviour
{
    [SerializeField] private PlayerMotor _playerMotor;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _speed = 3f;
    private float _movebool = 1;
    private ControlsController _controlsController;
    private Vector3 _currentDirection;

    [Inject]
    public void Init(ControlsController controlsController)
    {
        _controlsController = controlsController;
    }

    private void Update()
    {
        if (_controlsController.InputCondition())
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            _currentDirection = Camera.main.transform.forward;
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
            ApplyMovement();
        }

    }

    private void ApplyMovement()
    {
        Vector3 _velocity2 = _currentDirection * _speed * _movebool;

        _playerMotor.Move(_velocity2);

        float _yRot = _camera.eulerAngles.x;
        Vector3 _rotation = new Vector3(_yRot, 0f, 0f);
    }
}
