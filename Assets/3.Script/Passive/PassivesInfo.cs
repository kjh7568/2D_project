using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PassiveNameSpace;

namespace PassiveNameSpace
{
    interface PassiveInterface
    {
        string passiveName { get; set; }
        string passiveComment { get; set; }
        int passiveCount { get; set; }
        int passiveMaxCount { get; set; }

        public void Action();
    }
}

public class health : PassiveInterface
{
    public string passiveName { get; set; } = "체력";
    public string passiveComment { get; set; } = "최대 체력의 수치가 증가합니다.";
    public int passiveCount { get; set; } = 5;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.maxHpFlat += 20;

        playerState.UpdateMaxHp();
        playerState.setHp(20);

        passiveCount++;

        Debug.Log($"현재 체력: {playerState.hp} / {playerState.maxHp}");
        Debug.Log($"최대 체력 플랫: {playerState.maxHpFlat}");
        Debug.Log($"최대 체력 증가: {playerState.maxHpPercent}");
    }
}
public class fortitude : PassiveInterface
{
    public string passiveName { get; set; } = "강인함";
    public string passiveComment { get; set; } = "최대 체력이 10% 증가합니다  .";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.maxHpPercent += 0.1f;

        playerState.UpdateMaxHp();
        playerState.setHp(playerState.maxHpFlat * 0.1f);

        passiveCount++;

        Debug.Log($"현재 체력: {playerState.hp} / {playerState.maxHp}");
        Debug.Log($"최대 체력 플랫: {playerState.maxHpFlat}");
        Debug.Log($"최대 체력 증가: {playerState.maxHpPercent}");
    }
}
public class intelligence : PassiveInterface
{
    public string passiveName { get; set; } = "지능";
    public string passiveComment { get; set; } = "주문력이 5% 증가합니다";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.spellPowerPercent += 0.05f;

        playerState.UpdateSpellPower();

        passiveCount++;

        Debug.Log($"현재 주문력: {playerState.spellPower}");
        Debug.Log($"주문력 플랫: {playerState.spellPowerFlat}");
        Debug.Log($"주문력 증가: {playerState.spellPowerPercent}");
    }
}
public class ferocity : PassiveInterface
{
    public string passiveName { get; set; } = "포악함";
    public string passiveComment { get; set; } = "공격력이 5% 증가합니다";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.attackPowerPercent += 0.05f;

        playerState.UpdateAttackPower();

        passiveCount++;

        Debug.Log($"현재 주문력: {playerState.attackPower}");
        Debug.Log($"주문력 플랫: {playerState.attackPowerFlat}");
        Debug.Log($"주문력 증가: {playerState.attackPowerPercent}");
    }
}
public class speed : PassiveInterface
{
    public string passiveName { get; set; } = "신속";
    public string passiveComment { get; set; } = "이동 속도가 5% 증가합니다.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.movementSpeedPercent += 0.05f;

        playerState.UpdateMovementSpeed();

        passiveCount++;

        Debug.Log($"현재 이동속도: {playerState.movementSpeed}");
        Debug.Log($"이동속도 플랫: {playerState.movementSpeedFlat}");
        Debug.Log($"이동속도 증가: {playerState.movementSpeedPercent}");
    }
}
public class expansion : PassiveInterface
{
    public string passiveName { get; set; } = "팽창";
    public string passiveComment { get; set; } = "공격 또는 마법의 크기가 5% 커집니다.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        //playerState.skillExpansion += 0.05f;
        playerState.skillExpansion += 1f;

        Debug.Log($"스킬 사이즈: {playerState.skillExpansion}");
    }
}
public class accuracy : PassiveInterface
{
    public string passiveName { get; set; } = "정확성";
    public string passiveComment { get; set; } = "치명타 확률이 5% 증가합니다.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.criticalChance += 5f;

        Debug.Log($"치명타 확률: {playerState.criticalChance}");
    }
}
public class focus : PassiveInterface
{
    public string passiveName { get; set; } = "집중";
    public string passiveComment { get; set; } = "스킬의 지속 시간이 5% 증가합니다.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.durationIncrease += 0.05f;

        Debug.Log($"지속시간 증가량: {playerState.durationIncrease }");
    }
}
public class rapidCast : PassiveInterface
{
    public string passiveName { get; set; } = "고속 시전";
    public string passiveComment { get; set; } = "시전 속도가 5% 증가합니다.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.cooldownReduction += 0.05f;

        Debug.Log($"쿨타임 감소량: {playerState.cooldownReduction }");
    }
}
