using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillNameSpace;

public class FlameExplosionEffect : MonoBehaviour, SkillInfoInterface
{
    public string skillKey { get; set; }
    public float skillCoolTime { get; set; }
    public float setSkillCoolTime;
    public float skillMovementSpeed { get; set; }
    public float setSkillMovementSpeed;
    public float skillDuration { get; set; }
    public float setSkillDuration;
    public float skillCoefficient { get; set; }

    public float setSkillCoefficient;

    private Vector3 skillSize;

    private void Awake()
    {
        skillSize = transform.localScale;

        SetValue();
        StartCoroutine(SkillDuration(skillDuration));
    }

    public void UseSkill(float coolTime)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out EnemyState ES))
            {
                ES.SetHp(-skillCoefficient * GameManager.GM.playerState.spellPower);
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

    private IEnumerator SkillDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
