using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector3 _size;
    [SerializeField] private float _heightOffset;

    public Vector3 SpawnPosition()
    {
        Vector3 pos = _center + 
        new Vector3(
            Random.Range(-_size.x * 0.5f, _size.x * 0.5f),
            _heightOffset,
            Random.Range(-_size.z * 0.5f, _size.z * 0.5f));

        return pos;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(_center, _size);
    }
#endif
}
