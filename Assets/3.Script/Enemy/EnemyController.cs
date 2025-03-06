using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PlayerController player;

    private EnemySpawner enemySpawner;
    private EnemyState enemyState;

    private bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.GM.playerController;
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        TryGetComponent(out enemyState);

        isAttack = false;

    }

    void Update()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = transform.position;

        if (Vector2.Distance(playerPos, enemyPos) > 40f)
        {
            enemySpawner.DequeueEnemy(gameObject);
        }

        transform.position = Vector2.MoveTowards(enemyPos, playerPos, enemyState.movementSpeed * Time.deltaTime);

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
                Debug.Log($"몬스터에게 피격! {enemyState.damage - GameManager.GM.playerState.defense}");
                playerState.setHp(-(enemyState.damage - GameManager.GM.playerState.defense));
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
        GameManager.GM.instansManaStoneFragment += 1;
        enemySpawner.DequeueEnemy(gameObject);
    }
}
