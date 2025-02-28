using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabController : MonoBehaviour
{
    private PlayerController player;

    private EnemySpawner enemySpawner;
    private EnemyState enemyState;

    private bool isAttack;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.GM.playerController;
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        TryGetComponent(out enemyState);

        direction = (player.transform.position - transform.position).normalized;

        isAttack = false;

    }

    // Update is called once per frame  
    void Update()
    {
        //if (GameManager.GM.playerState.isGetReward)
        //{
        //    return;
        //}

        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = transform.position;

        if (Vector2.Distance(playerPos, enemyPos) > 40f)
        {
            enemySpawner.DequeueEnemy(gameObject);
        }

        transform.Translate(direction * enemyState.movementSpeed * Time.deltaTime, Space.World);

        if (enemyState.hp <= 0)
        {
            DieEnemy();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerState playerState = GameManager.GM.playerState;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isAttack)
            {
                playerState.setHp(-enemyState.damage);
                isAttack = true;
                StartCoroutine(AttackCooldown_Co());
            }
        }
    }
    public IEnumerator AttackCooldown_Co()
    {
        yield return new WaitForSeconds(0.5f);

        isAttack = false;
    }

    public void DieEnemy()
    {
        GameManager.GM.playerState.setExp(enemyState.acquiredExp);
        GameManager.GM.instansManaStoneFragment += 2;
        enemySpawner.DequeueEnemy(gameObject);
    }
}
