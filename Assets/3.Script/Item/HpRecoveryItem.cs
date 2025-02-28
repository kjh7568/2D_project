using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ItemNameSpace;

public class HpRecoveryItem : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(ItemDestroy_Co());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.GM.playerState.setHp(20);
            Destroy(gameObject);
        }
    }

    public IEnumerator ItemDestroy_Co()
    {
        yield return new WaitForSeconds(60f);

        Destroy(gameObject);
    }
}
