using Base.Model;
using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance { get { return _instance; } }
    private Player _player;

    [Inject]
    public void Init(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        if(ES3.KeyExists("playerxp"))
            _player.Experience = ES3.Load<uint>("playerxp");
    }

    private void OnApplicationQuit() {
        ES3.Save("playerxp", _player.Experience);
    }

    public void SavePlayerXP()
    {
        ES3.Save("playerxp", _player.Experience);
    }
}
