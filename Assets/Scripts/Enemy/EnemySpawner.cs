using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject prefabEnemy;
    [SerializeField] private GameObject prefabMultiEnemy;
    private List<Color> colors = new List<Color>();

    Timer spawnTimer;

    #endregion

    #region Methods

    private void Awake()
    {
        colors.Add(new Color(219/255f, 0 / 255f, 255 / 255f, 255 / 255f));
        colors.Add(new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f));

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
        int rnd = Random.Range(0, 4);
        //switch (rnd)
        //{
        //    case 0:
        //        enemyForSpawn = prefabEnemy;
        //        enemyForSpawn.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Count)];
        //        break;
        //    case 1:
        //        enemyForSpawn = prefabMultiEnemy;
        //        break;
        //}
        if (rnd == 3)
            enemyForSpawn = prefabMultiEnemy;
        else
        {
            enemyForSpawn = prefabEnemy;
            enemyForSpawn.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Count)];
        }

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
