using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] private List<SpawnArea> _enemyAreas;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private Transform _parentForEnemies;
    [SerializeField] private SpawnArea _ammoSpawnArea;
    [SerializeField] private List<GameObject> _ammoTypes;

    private uint _intervalEnemy = 350;
    private uint _intervalAmmo = 600;
    private DiContainer Container;

    [Inject]
    public void Init(DiContainer container)
    {
        Container = container;
    }

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

        GameObject enemy = Container.InstantiatePrefab(_enemies[randomEnemyIndex], _parentForEnemies);

        enemy.transform.SetParent(null);
        enemy.transform.position = _enemyAreas[randomSpawnAreaIndex].SpawnPosition();
    }

    private void SpawnAmmo()
    {
        _intervalEnemy = (uint) Random.Range(200, 1600);
        int randomAmmoIndex = Random.Range(0, _ammoTypes.Count);

        GameObject gameObject = Container.InstantiatePrefab(_ammoTypes[randomAmmoIndex]);
        gameObject.transform.position = _ammoSpawnArea.SpawnPosition();
    }
}
