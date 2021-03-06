using Base.Model;
using Base.Signal;
using UnityEngine;
using Zenject;

namespace Base.Controller
{
    public class PlayerStatsController
    {
        private Model.PlayerStats _playerStats;
        public uint TotalEnemiesKilled => _playerStats.TotalEnemiesKilled;
        public uint TotalBulletsFired => _playerStats.TotalShots;
        public uint TotalCriticals => _playerStats.TotalCriticals;
        public float CriticalShotRate => _playerStats.CriticalShotRate;
        public uint SurvivalMaxTimeSurvived => _playerStats.SurvivalHighestTimeSurvived;
        public uint SurvivalHighestKillCount => _playerStats.SurvivalHighestKillCount;
        private uint HighestKillEphemeralCounter => _playerStats.HighestKillEphemeralCounter;

        public PlayerStatsController(PlayerStats playerStats)
        {
            _playerStats = playerStats;
            _playerStats.HighestKillEphemeralCounter = 0;
        }

        public void AddBulletShot()
        {
            _playerStats.TotalShots++;
            
            
            CalculateRate();
        }

        public void AddCritical()
        {
            _playerStats.TotalCriticals++;

            CalculateRate();
        }

        public void CalculateRate()
        {
            float rate = (float) _playerStats.TotalCriticals / (float) _playerStats.TotalShots;
            rate *= 100;
            _playerStats.CriticalShotRate = rate;
        }

        public void AddKillSurvivalRound()
        {
            _playerStats.HighestKillEphemeralCounter++;

            if (_playerStats.HighestKillEphemeralCounter >= _playerStats.SurvivalHighestKillCount)
                _playerStats.SurvivalHighestKillCount = _playerStats.HighestKillEphemeralCounter;
        }

        public void ResetEphemeral()
        {
            _playerStats.HighestKillEphemeralCounter = 0;
        }
    }
}