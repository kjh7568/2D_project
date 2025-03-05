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
    public string passiveName { get; set; } = "ü��";
    public string passiveComment { get; set; } = "�ִ� ü���� ��ġ�� �����մϴ�.";
    public int passiveCount { get; set; } = 5;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.maxHpFlat += 20;

        playerState.UpdateMaxHp();
        playerState.setHp(20);

        passiveCount++;

        Debug.Log($"���� ü��: {playerState.hp} / {playerState.maxHp}");
        Debug.Log($"�ִ� ü�� �÷�: {playerState.maxHpFlat}");
        Debug.Log($"�ִ� ü�� ����: {playerState.maxHpPercent}");
    }
}
public class fortitude : PassiveInterface
{
    public string passiveName { get; set; } = "������";
    public string passiveComment { get; set; } = "�ִ� ü���� 10% �����մϴ�  .";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.maxHpPercent += 0.1f;

        playerState.UpdateMaxHp();
        playerState.setHp(playerState.maxHpFlat * 0.1f);

        passiveCount++;

        Debug.Log($"���� ü��: {playerState.hp} / {playerState.maxHp}");
        Debug.Log($"�ִ� ü�� �÷�: {playerState.maxHpFlat}");
        Debug.Log($"�ִ� ü�� ����: {playerState.maxHpPercent}");
    }
}
public class intelligence : PassiveInterface
{
    public string passiveName { get; set; } = "����";
    public string passiveComment { get; set; } = "�ֹ����� 5% �����մϴ�";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.spellPowerPercent += 0.05f;

        playerState.UpdateSpellPower();

        passiveCount++;

        Debug.Log($"���� �ֹ���: {playerState.spellPower}");
        Debug.Log($"�ֹ��� �÷�: {playerState.spellPowerFlat}");
        Debug.Log($"�ֹ��� ����: {playerState.spellPowerPercent}");
    }
}
public class ferocity : PassiveInterface
{
    public string passiveName { get; set; } = "������";
    public string passiveComment { get; set; } = "���ݷ��� 5% �����մϴ�";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.attackPowerPercent += 0.05f;

        playerState.UpdateAttackPower();

        passiveCount++;

        Debug.Log($"���� �ֹ���: {playerState.attackPower}");
        Debug.Log($"�ֹ��� �÷�: {playerState.attackPowerFlat}");
        Debug.Log($"�ֹ��� ����: {playerState.attackPowerPercent}");
    }
}
public class speed : PassiveInterface
{
    public string passiveName { get; set; } = "�ż�";
    public string passiveComment { get; set; } = "�̵� �ӵ��� 5% �����մϴ�.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.movementSpeedPercent += 0.05f;

        playerState.UpdateMovementSpeed();

        passiveCount++;

        Debug.Log($"���� �̵��ӵ�: {playerState.movementSpeed}");
        Debug.Log($"�̵��ӵ� �÷�: {playerState.movementSpeedFlat}");
        Debug.Log($"�̵��ӵ� ����: {playerState.movementSpeedPercent}");
    }
}
public class expansion : PassiveInterface
{
    public string passiveName { get; set; } = "��â";
    public string passiveComment { get; set; } = "���� �Ǵ� ������ ũ�Ⱑ 5% Ŀ���ϴ�.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        //playerState.skillExpansion += 0.05f;
        playerState.skillExpansion += 1f;

        Debug.Log($"��ų ������: {playerState.skillExpansion}");
    }
}
public class accuracy : PassiveInterface
{
    public string passiveName { get; set; } = "��Ȯ��";
    public string passiveComment { get; set; } = "ġ��Ÿ Ȯ���� 5% �����մϴ�.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.criticalChance += 5f;

        Debug.Log($"ġ��Ÿ Ȯ��: {playerState.criticalChance}");
    }
}
public class focus : PassiveInterface
{
    public string passiveName { get; set; } = "����";
    public string passiveComment { get; set; } = "��ų�� ���� �ð��� 5% �����մϴ�.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.durationIncrease += 0.05f;

        Debug.Log($"���ӽð� ������: {playerState.durationIncrease }");
    }
}
public class rapidCast : PassiveInterface
{
    public string passiveName { get; set; } = "��� ����";
    public string passiveComment { get; set; } = "���� �ӵ��� 5% �����մϴ�.";
    public int passiveCount { get; set; } = 0;
    public int passiveMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.cooldownReduction += 0.05f;

        Debug.Log($"��Ÿ�� ���ҷ�: {playerState.cooldownReduction }");
    }
}
