using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;

    [SerializeField] private float spawnTime;

    private List<Queue<GameObject>> enemysQueueList;
    [SerializeField] private int enemyQueueInitialSize;

    private PlayerController playerController;

    private float elapsedTime;

    public int difficulty;


    void Start()
    {
        difficulty = 1;

        playerController = GameManager.GM.playerController;

        enemysQueueList = new List<Queue<GameObject>>();

        if (spawnTime <= 0)
        {
            spawnTime = 0.5f;
        }

        for (int i = 0; i < enemy.Length; i++)
        {
            enemysQueueList.Add(new Queue<GameObject>());

            for (int a = 0; a < enemyQueueInitialSize; a++)
            {
                GameObject temp = Instantiate(enemy[i]);
                temp.SetActive(false);

                enemysQueueList[i].Enqueue(temp);
            }
        }

        StartCoroutine(SpawnEnemy_Co());
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (Mathf.FloorToInt(elapsedTime / 60) == 1)
        {
            difficulty = 2;
        }
        //else if (Mathf.FloorToInt(elapsedTime / 60) == 2)
        //{
        //    difficulty = 3;
        //}
    }

    private IEnumerator SpawnEnemy_Co()
    {
        WaitForSeconds WFS = new WaitForSeconds(spawnTime);

        while (true)
        {
            //if (!GameManager.GM.playerState.isGetReward)
            //{
            //    //나중에 적 종류 추가되면 Random으로 바꿔야함
            //}
            SpawnEnemy();

            yield return WFS;
        }
    }

    public void SpawnEnemy()
    {
        GameObject spawnedEnemy;
        int species = 0;
        Vector2 spawnPosition = Vector2.zero;

        switch (difficulty)
        {
            case 1:
                species = 0;
                spawnPosition = SwamPos();
                break;
            case 2:
                int rate = Random.Range(0, 100);

                if (rate < 15)
                {
                    species = 1;
                    spawnPosition = ScarabPos();
                }
                else if (rate < 100)
                {
                    species = 0;
                    spawnPosition = SwamPos();
                }

                break;
            case 3:
                break;
            default:

                break;
        }

        if (enemysQueueList[species].Count <= 0)
        {
            enemysQueueList[species].Enqueue(Instantiate(enemy[species]));
        }

        spawnedEnemy = enemysQueueList[species].Dequeue();
        spawnedEnemy.SetActive(true);
        spawnedEnemy.transform.position = spawnPosition;
    }

    public Vector2 SwamPos()
    {
        float playerDirectionX = playerController.x;
        float playerDirectionY = playerController.y;

        Vector2 playerPos = playerController.transform.position;

        float spawnPositionX = 0;
        float spawnPositionY = 0;

        //이동 방향 쪽으로 가중치
        if (playerDirectionX < 0)
        {
            if (playerDirectionY < 0)
            {
                spawnPositionX = Random.Range(-14, -7) + playerPos.x;
                spawnPositionY = Random.Range(-14, -5) + playerPos.y;
            }
            else if (playerDirectionY > 0)
            {
                spawnPositionX = Random.Range(-14, -7) + playerPos.x;
                spawnPositionY = Random.Range(6, 15) + playerPos.y;
            }
            else
            {
                spawnPositionX = Random.Range(-14, -7) + playerPos.x;
                spawnPositionY = Random.Range(-14, 15) + playerPos.y;
            }
        }
        else if (playerDirectionX > 0)
        {
            if (playerDirectionY < 0)
            {
                spawnPositionX = Random.Range(8, 15) + playerPos.x;
                spawnPositionY = Random.Range(-14, -5) + playerPos.y;
            }
            else if (playerDirectionY > 0)
            {
                spawnPositionX = Random.Range(8, 15) + playerPos.x;
                spawnPositionY = Random.Range(6, 15) + playerPos.y;
            }
            else
            {
                spawnPositionX = Random.Range(8, 15) + playerPos.x;
                spawnPositionY = Random.Range(-14, 15) + playerPos.y;
            }
        }
        else
        {
            if (playerDirectionY < 0)
            {
                spawnPositionX = Random.Range(-14, 15) + playerPos.x;
                spawnPositionY = Random.Range(-14, -5) + playerPos.y;
            }
            else if (playerDirectionY > 0)
            {
                spawnPositionX = Random.Range(-14, 15) + playerPos.x;
                spawnPositionY = Random.Range(6, 15) + playerPos.y;
            }
            else
            {
                spawnPositionX = ((Random.value < 0.5f) ? Random.Range(-14, -7) : Random.Range(8, 15)) + playerPos.x;
                spawnPositionY = ((Random.value < 0.5f) ? Random.Range(-14, -5) : Random.Range(6, 15)) + playerPos.y;
            }
        }

        return new Vector2(spawnPositionX, spawnPositionY);
    }

    public Vector2 ScarabPos()
    {
        float spawnPositionX = 0;
        float spawnPositionY = 0;

        Vector2 playerPos = playerController.transform.position;

        if (Random.value < 0.5f)//x의 값만 바뀔 경우
        {
            spawnPositionX = Random.Range(-10, 11) + playerPos.x;
            spawnPositionY = ((Random.value < 0.5f) ? 6 : -6) + playerPos.y;
        }
        else
        {
            spawnPositionX = ((Random.value < 0.5f) ? 8 : -8) + playerPos.x;
            spawnPositionY = Random.Range(-8, 9) + playerPos.y;  
        }

        return new Vector2(spawnPositionX, spawnPositionY);
    }

    public void DequeueEnemy(GameObject enemy)
    {
        enemysQueueList[0].Enqueue(enemy);
        enemy.SetActive(false);
    }
}
