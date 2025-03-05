using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillNameSpace;

public class FireBall : SkillManager, SkillInfoInterface
{
    [SerializeField] private GameObject effect;

    private Vector2 closestEnemyPos;

    private Vector3 skillSize;
    public string skillKey { get; set; } = "FireBall";
    public float skillCoolTime { get; set; }
    public float setSkillCoolTime;
    public float skillMovementSpeed { get; set; }
    public float setSkillMovementSpeed;
    public float skillDuration { get; set; }
    public float setSkillDuration;
    public float skillCoefficient { get; set; }

    public float setSkillCoefficient;

    public float initialSize;

    public string skillComment { get; set; }


    Vector2 direction;

    void Start()
    {
        Vector2 playerPos = GameManager.GM.playerController.transform.position;

        skillSize = transform.localScale;

        SetJsonData();
        SetValue();

        transform.position = playerPos;

        // 적이 존재하는 경우만 위치를 설정
        Vector2 enemyPos = GetClosestEnemy();
        if (enemyPos != (Vector2)playerPos)
        {
            closestEnemyPos = enemyPos;
        }
        else
        {
            Debug.Log("적을 찾을 수 없어서 FireBall을 생성할 수 없습니다.");
        }

        direction = (closestEnemyPos - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        StartCoroutine(SkillDuration(skillDuration));
    }

    void Update()
    {
        transform.Translate(direction * skillMovementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector2 curPos = transform.position;

            Instantiate(effect, curPos, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    public void UseSkill(float coolTime)
    {
        TryGetComponent(out FireBall fireBall);
        skillCoolTime = coolTime - (coolTime * GameManager.GM.playerState.cooldownReduction);

        Instantiate(gameObject);
    }

    private void SetValue()
    {
        PlayerState playerState = GameManager.GM.playerState;

        skillCoolTime = setSkillCoolTime - (setSkillCoolTime * playerState.cooldownReduction);
        skillMovementSpeed = setSkillMovementSpeed;
        skillDuration = setSkillDuration * playerState.durationIncrease;
        skillCoefficient = setSkillCoefficient;
        //transform.localScale = skillSize * playerState.skillExpansion;
    }

    private void SetJsonData()
    {
        GameObject.Find("SkillManager").TryGetComponent(out SkillManager skillManager);
         setSkillCoolTime = skillManager.skillDataDict[skillKey].skillCoolTime;
        setSkillMovementSpeed = skillManager.skillDataDict[skillKey].skillMovementSpeed;
        setSkillDuration = skillManager.skillDataDict[skillKey].skillDuration;
        setSkillCoefficient = skillManager.skillDataDict[skillKey].skillCoefficient;
    }

    private IEnumerator SkillDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
