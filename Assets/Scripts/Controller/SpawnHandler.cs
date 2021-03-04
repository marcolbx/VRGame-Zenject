using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] private List<SpawnArea> _enemyAreas;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private SpawnArea _ammoSpawnArea;
    [SerializeField] private List<GameObject> _ammoTypes;

    private uint _intervalEnemy = 350;
    private uint _intervalAmmo = 600;

    private void Update()
    {
        if (Time.frameCount % _intervalEnemy == 0)
        {
            SpawnEnemy();
        }
        if (Time.frameCount % _intervalAmmo == 0)
        {
            SpawnAmmo();
        }
    }

    private void SpawnEnemy()
    {
        _intervalEnemy = (uint) Random.Range(100, 800);
        int randomSpawnAreaIndex = Random.Range(0, _enemyAreas.Count);
        int randomEnemyIndex = Random.Range(0, _enemies.Count);

        Instantiate(_enemies[randomEnemyIndex], _enemyAreas[randomSpawnAreaIndex].SpawnPosition(), Quaternion.identity);
    }

    private void SpawnAmmo()
    {
        _intervalEnemy = (uint) Random.Range(200, 1600);
        int randomAmmoIndex = Random.Range(0, _ammoTypes.Count);

        Instantiate(_ammoTypes[randomAmmoIndex], _ammoSpawnArea.SpawnPosition(), Quaternion.identity);
    }
}
