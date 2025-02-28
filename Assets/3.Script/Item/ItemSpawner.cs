using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace ItemNameSpace
//{
//    interface Iteminfo
//    {
//        IEnumerator ItemEvent_Co();
//    }
//}

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] item;

    void Start()
    {
        StartCoroutine(SpawnItem_Co());    
    }

    private IEnumerator SpawnItem_Co()
    {
        WaitForSeconds WFS = new WaitForSeconds(10f);
        
        while (true)
        {
            SpanwItem();

            yield return WFS;
        }
    }

    private void SpanwItem()
    {
        Vector2 playerPos = GameManager.GM.playerState.transform.position;

        float spawnPositionX = ((Random.value < 0.5f) ? Random.Range(-14, -7) : Random.Range(8, 15)) + playerPos.x;
        float spawnPositionY = ((Random.value < 0.5f) ? Random.Range(-14, -5) : Random.Range(6, 15)) + playerPos.y;
        
        Vector2 spawnPosition = new Vector2(spawnPositionX, spawnPositionY);

        //나중에 아이템 종류 추가되면 Random으로 바꿔야함
        GameObject spawnedItem = Instantiate(item[0]);

        spawnedItem.transform.position = spawnPosition;
    }
}
