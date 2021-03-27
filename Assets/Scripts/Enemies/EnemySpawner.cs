using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject EnemySkeletonPrefab;
    [SerializeField] private GameObject EnemyPupmkinPrefab;
    private List<GameObject> enemies = new List<GameObject>();

    private float minSpawnTime = 3f;
    private float maxSpawnTime = 5f;
    private Timer spawnTimer;

    private bool canBeIncreased = true; // Может ли быть увеличина скорость появления врагов

    #endregion

    #region Methods

    private void Awake()
    {
        enemies.Add(EnemySkeletonPrefab);
        enemies.Add(EnemyPupmkinPrefab);

        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(minSpawnTime, maxSpawnTime);
        spawnTimer.Run();
    }

    private void Update()
    {
        if (spawnTimer.Finished)
        {
            spawnTimer.Duration = Random.Range(minSpawnTime, maxSpawnTime);
            SpawnEnemy();
        }

        if (GameManager.Instance.Score % 5 != 0)
            canBeIncreased = true;
        if (canBeIncreased)
            IncreaseSpawnSpeed();
    }

    private void IncreaseSpawnSpeed() // Увеличить скорость появления врагов
    {
        if (GameManager.Instance.Score % 5 == 0 && GameManager.Instance.Score > 0)
        {
            if (minSpawnTime - GameManager.Instance.Score * 0.018f >= 1)
                minSpawnTime -= GameManager.Instance.Score * 0.018f;
            else
                minSpawnTime = 1f;

            if (maxSpawnTime - GameManager.Instance.Score * 0.018f >= 1.5f)
                maxSpawnTime -= GameManager.Instance.Score * 0.018f;
            else
                maxSpawnTime = 1.5f;

            canBeIncreased = false;
        }
    }

    private void SpawnEnemy()
    {

        GameObject enemyForSpawn = enemies[Random.Range(0, enemies.Count)];

        float halfWidthEnemy = enemyForSpawn.GetComponent<SpriteRenderer>().bounds.extents.x;
        float halfHeightEnemy = enemyForSpawn.GetComponent<SpriteRenderer>().bounds.extents.y;
        float gameBorderLeft = GameObject.Find("GameBorderLeft").transform.position.x;
        float gameBorderRight = GameObject.Find("GameBorderRight").transform.position.x;
        float halfWidthGameBorderLeft = GameObject.Find("GameBorderLeft").GetComponent<BoxCollider2D>().bounds.extents.x;
        float halfWidthGameBorderRight = GameObject.Find("GameBorderRight").GetComponent<BoxCollider2D>().bounds.extents.x;

        Vector2 spawnPosition = new Vector2(Random.Range(gameBorderLeft + halfWidthGameBorderLeft + halfWidthEnemy, 
            gameBorderRight - halfWidthGameBorderRight - halfWidthEnemy), ScreenUtils.ScreenTop + halfHeightEnemy * 2);

        Instantiate(enemyForSpawn, spawnPosition, Quaternion.identity);

        spawnTimer.Run();
    }

    #endregion

}
