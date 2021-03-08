using UnityEngine;

public class PositionHelper : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    private Quaternion _lastRotation;

    private void Update()
    {
        CalculatePosition();
    }

    private void CalculatePosition()
    {
        if (_camera.transform.eulerAngles.x > 20 && _camera.transform.eulerAngles.x < 25) //Creo el vector de la ultima rotacion antes de 50 grados
            _lastRotation = Quaternion.Euler(90f, _camera.transform.eulerAngles.y, _camera.transform.eulerAngles.z);
        else if (_camera.transform.eulerAngles.x < 20)
            this.transform.rotation = Quaternion.Euler(90f, _camera.transform.eulerAngles.y, _camera.transform.eulerAngles.z);
        else
            this.transform.rotation = _lastRotation;

        this.transform.position = new Vector3(_player.position.x, transform.position.y, _player.position.z);
    }
}
