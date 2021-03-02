using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    public Vector3 PlayerPosition => transform.position;

    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion    
}