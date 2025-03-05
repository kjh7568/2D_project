using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillNameSpace;

public class FreezingPulse : SkillManager, SkillInfoInterface
{
    private Vector2 closestEnemyPos;

    private Vector3 skillSize;
    public string skillKey { get; set; } = "FreezingPulse";
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
        Vector3 playerPos = GameManager.GM.playerController.transform.position;

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
            PlayerState playerState = GameManager.GM.playerState;

            if (collision.TryGetComponent(out EnemyState ES))
            {
                int rate = Random.Range(0, 100);
                if (rate < playerState.criticalChance)
                {
                    Debug.Log($"기본 피해량 {-skillCoefficient * playerState.spellPower}");
                    Debug.Log($"치명타 발생! {(skillCoefficient * playerState.spellPower) * playerState.damageTakenAmount}");
                    ES.SetHp((-skillCoefficient * playerState.spellPower) * playerState.damageTakenAmount);
                }
                else
                {
                    ES.SetHp(-skillCoefficient * playerState.spellPower);
                }

                //StartCoroutine(ES.Freeze());
            }

        }
    }

    public void UseSkill(float coolTime)
    {
        TryGetComponent(out FreezingPulse freezingPulse);
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
