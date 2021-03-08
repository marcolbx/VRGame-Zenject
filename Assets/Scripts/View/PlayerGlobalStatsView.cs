using Base.Controller;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerGlobalStatsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalEnemiesKilled;
    [SerializeField] private TextMeshProUGUI _totalBulletsFired;
    [SerializeField] private TextMeshProUGUI _totalCriticalHits;
    [SerializeField] private TextMeshProUGUI _criticalRate;
    [SerializeField] private TextMeshProUGUI _survivalHighestKillCount;
    [SerializeField] private TextMeshProUGUI _survivalHighestTimeSurvived;

    private PlayerStatsController _playerStatsController;

    [Inject]
    public void Init(PlayerStatsController playerStatsController)
    {
        _playerStatsController = playerStatsController;
    }

    private void Start() {
        ShowStats();
    }

    private void ShowStats()
    {
        if (_totalEnemiesKilled != null)
            _totalEnemiesKilled.text = _playerStatsController.TotalEnemiesKilled.ToString();

        if (_totalBulletsFired != null)
            _totalBulletsFired.text = _playerStatsController.TotalBulletsFired.ToString();

        if (_totalCriticalHits != null)
            _totalCriticalHits.text = _playerStatsController.TotalCriticals.ToString();

        if (_criticalRate != null)
            _criticalRate.text = _playerStatsController.CriticalShotRate.ToString() + "%";

        if (_survivalHighestKillCount != null)
            _survivalHighestKillCount.text = _playerStatsController.SurvivalHighestKillCount.ToString();
    }
}
