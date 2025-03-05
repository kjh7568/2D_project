using SkillNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOfStorms : SkillManager, SkillInfoInterface
{
    public string skillKey { get; set; } = "OrbOfStorms";
    public float skillCoolTime { get; set; }
    public float setSkillCoolTime;
    public float skillMovementSpeed { get; set; }
    public float setSkillMovementSpeed;
    public float skillDuration { get; set; }
    public float setSkillDuration;
    public float skillCoefficient { get; set; }
    public string skillComment { get; set; }

    public float setSkillCoefficient;

    private Vector3 skillSize;

    private void Awake()
    {
        Vector2 playerPos = GameManager.GM.playerController.transform.position;

        skillSize = transform.localScale;

        SetJsonData();
        SetValue();
        StartCoroutine(SkillDuration(skillDuration));

        float randomX = Random.Range(-7f, 7f); 
        float randomY = Random.Range(-5f, 5f);
        gameObject.transform.position = new Vector2(randomX + playerPos.x, randomY + playerPos.y);
    }

    public void UseSkill(float coolTime)
    {
        skillCoolTime = coolTime - (coolTime * GameManager.GM.playerState.cooldownReduction);

        Instantiate(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out EnemyState ES))
            {
                StartCoroutine(Dealing(ES));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out EnemyState ES))
            {
                StopCoroutine(Dealing(ES));
            }
        }
    }
    private void SetValue()
    {
        PlayerState playerState = GameManager.GM.playerState;

        skillCoolTime = setSkillCoolTime - (setSkillCoolTime * playerState.cooldownReduction);
        skillMovementSpeed = setSkillMovementSpeed;
        skillDuration = setSkillDuration * playerState.durationIncrease;
        skillCoefficient = setSkillCoefficient;
        transform.localScale = skillSize * playerState.skillExpansion;
    }
    private void SetJsonData()
    {
        GameObject.Find("SkillManager").TryGetComponent(out SkillManager skillManager);
        setSkillCoolTime = skillManager.skillDataDict[skillKey].skillCoolTime;
        setSkillMovementSpeed = skillManager.skillDataDict[skillKey].skillMovementSpeed;
        setSkillDuration = skillManager.skillDataDict[skillKey].skillDuration;
        setSkillCoefficient = skillManager.skillDataDict[skillKey].skillCoefficient;
    }
    private IEnumerator Dealing(EnemyState ES)
    {
        while (true)
        {
            PlayerState playerState = GameManager.GM.playerState;

            int rate = Random.Range(0, 100);
            if (rate < playerState.criticalChance)
            {
                Debug.Log($"기본 피해량 {-skillCoefficient * playerState.spellPower}");
                Debug.Log($"치명타 발생! {(skillCoefficient * playerState.spellPower) * playerState.damageTakenAmount}");
                ES.SetHp((-skillCoefficient * playerState.spellPower) * playerState.damageTakenAmount);
            }
            else
            {
                Debug.Log($"기본 피해량 {-skillCoefficient * playerState.spellPower}");
                ES.SetHp(-skillCoefficient * playerState.spellPower);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator SkillDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
