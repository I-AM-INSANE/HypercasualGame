using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject EnemySkeletonPrefab;
    [SerializeField] private GameObject EnemyPupmkinPrefab;
    private List<GameObject> enemies = new List<GameObject>();

    Timer spawnTimer;

    #endregion

    #region Methods

    private void Awake()
    {
        enemies.Add(EnemySkeletonPrefab);
        enemies.Add(EnemyPupmkinPrefab);

        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(2f, 4f);
        spawnTimer.Run();
    }

    private void Update()
    {
        if (spawnTimer.Finished)
            SpawnEnemy();
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
