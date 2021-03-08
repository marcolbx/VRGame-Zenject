using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField ] private Rigidbody _rigidbody;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;

    public void Move(Vector3 _velocity)
    {
        this._velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
         this._rotation = _rotation;
    }

    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if(_velocity != Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {
        if(_rotation != Vector3.zero)
        {
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));
        }
    }
}
