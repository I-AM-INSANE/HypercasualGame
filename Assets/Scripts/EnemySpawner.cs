using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject prefabEnemy;
    [SerializeField] private GameObject prefabMultiEnemy;

    Timer spawnTimer;

    #endregion

    #region Methods

    private void Awake()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(3f, 5f);
        spawnTimer.Run();
    }

    private void Update()
    {
        if (spawnTimer.Finished)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject enemyForSpawn = default;
        int rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                enemyForSpawn = prefabEnemy;
                break;
            case 1:
                enemyForSpawn = prefabMultiEnemy;
                break;
        }
        float halfWidthEnemyCollider = enemyForSpawn.GetComponent<Collider2D>().bounds.extents.x;
        float halfHeightEnemyCollider = enemyForSpawn.GetComponent<Collider2D>().bounds.extents.y;
        Vector2 spawnPosition = new Vector2(Random.Range(GameBordersManager.Instance.BorderLeft + halfWidthEnemyCollider,
            GameBordersManager.Instance.BorderRight - halfWidthEnemyCollider), 
            GameBordersManager.Instance.BorderTop + halfHeightEnemyCollider * 2);
        Instantiate(enemyForSpawn, spawnPosition, Quaternion.identity);

        spawnTimer.Run();
    }

    #endregion

}
