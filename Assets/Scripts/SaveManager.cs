using Base.Model;
using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance { get { return _instance; } }
    private Player _player;
    private PlayerStats _playerStats;
    private Handgun _handgun;
    private Shotgun _shotgun;
    private Machinegun _machinegun;

    [Inject]
    public void Init(Player player, PlayerStats playerStats, Handgun handgun, Shotgun shotgun, Machinegun machinegun)
    {
        _player = player;
        _playerStats = playerStats;
        _handgun = handgun;
        _shotgun = shotgun;
        _machinegun = machinegun;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else 
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() 
    {
        if(ES3.KeyExists("playerxp"))
            _player.Experience = ES3.Load<uint>("playerxp");

        if(ES3.KeyExists("enemiesKilled"))
            _playerStats.TotalEnemiesKilled = ES3.Load<uint>("enemiesKilled");

        if(ES3.KeyExists("shots"))
            _playerStats.TotalShots = ES3.Load<uint>("shots");

        if(ES3.KeyExists("critical"))
            _playerStats.TotalCriticals = ES3.Load<uint>("critical");

        if(ES3.KeyExists("criticalRate"))
            _playerStats.CriticalShotRate = ES3.Load<float>("criticalRate");

        if(ES3.KeyExists("money"))
            _player.Money = ES3.Load<float>("money");

        LoadPurchases();
    }

    private void OnApplicationQuit() 
    {
        SaveProgress();
    }

    public void SaveProgress()
    {
        ES3.Save("playerxp", _player.Experience);
        ES3.Save("enemiesKilled", _playerStats.TotalEnemiesKilled);
        ES3.Save("shots", _playerStats.TotalShots);
        ES3.Save("critical", _playerStats.TotalCriticals);
        ES3.Save("criticalRate", _playerStats.CriticalShotRate);
        ES3.Save("money", _player.Money);
        Debug.Log("Saving players money with: " + _player.Money);
        ES3.Save("highestKillCount", _playerStats.SurvivalHighestKillCount);
    }

    public void SaveHandgunRedDotPurchase(string nameOfItem)
    {
        ES3.Save("handgunRedDot", nameOfItem);
    }

    public void SaveShotgunScopePurchase(string nameOfItem)
    {
        ES3.Save("shotgunScope", nameOfItem);
    }

    public void SaveMachinegunScopePurchase(string nameOfItem)
    {
        ES3.Save("machinegunScope", nameOfItem);
    }

    public void LoadPurchases()
    {
        if(ES3.KeyExists("handgunRedDot"))
            _handgun.Attachments.Add(new Attachment(){Name = ES3.Load<string>("handgunRedDot")});
        if(ES3.KeyExists("shotgunScope"))
            _shotgun.Attachments.Add(new Attachment(){Name = ES3.Load<string>("shotgunScope")});
        if(ES3.KeyExists("machinegunScope"))
            _machinegun.Attachments.Add(new Attachment(){Name = ES3.Load<string>("machinegunScope")});

        Debug.Log("Handgun Attachments: " + _handgun?.Attachments?.Count);
        Debug.Log("Shotgun Attachments: " + _shotgun?.Attachments?.Count);
        Debug.Log("Machine Attachments: " + _machinegun?.Attachments?.Count);

    }
}
