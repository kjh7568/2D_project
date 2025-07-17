using UnityEngine;

interface IAugmentation
{
    public string augmentationName { get; set; }
    public string augmentationComment { get; set; }
    public int augmentationCount { get; set; }
    public int augmentationMaxCount { get; set; }

    public void Action();
}

public class Health : IAugmentation
{
    public string augmentationName { get; set; } = "ü��";
    public string augmentationComment { get; set; } = "�ִ� ü���� ��ġ�� �����մϴ�.";
    public int augmentationCount { get; set; } = 5;
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.maxHpFlat += 20;

        playerState.UpdateMaxHp();
        playerState.setHp(20);

        augmentationCount++;

        Debug.Log($"���� ü��: {playerState.hp} / {playerState.maxHp}");
        Debug.Log($"�ִ� ü�� �÷�: {playerState.maxHpFlat}");
        Debug.Log($"�ִ� ü�� ����: {playerState.maxHpPercent}");
    }
}

public class Fortitude : IAugmentation
{
    public string augmentationName { get; set; } = "������";
    public string augmentationComment { get; set; } = "�ִ� ü���� 10% �����մϴ�  .";
    public int augmentationCount { get; set; }
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.maxHpPercent += 0.1f;

        playerState.UpdateMaxHp();
        playerState.setHp(playerState.maxHpFlat * 0.1f);

        augmentationCount++;

        Debug.Log($"���� ü��: {playerState.hp} / {playerState.maxHp}");
        Debug.Log($"�ִ� ü�� �÷�: {playerState.maxHpFlat}");
        Debug.Log($"�ִ� ü�� ����: {playerState.maxHpPercent}");
    }
}
public class Intelligence : IAugmentation
{
    public string augmentationName { get; set; } = "����";
    public string augmentationComment { get; set; } = "�ֹ����� 5% �����մϴ�";
    public int augmentationCount { get; set; }
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.spellPowerPercent += 0.05f;

        playerState.UpdateSpellPower();

        augmentationCount++;

        Debug.Log($"���� �ֹ���: {playerState.spellPower}");
        Debug.Log($"�ֹ��� �÷�: {playerState.spellPowerFlat}");
        Debug.Log($"�ֹ��� ����: {playerState.spellPowerPercent}");
    }
}
public class Ferocity : IAugmentation
{
    public string augmentationName { get; set; } = "������";
    public string augmentationComment { get; set; } = "���ݷ��� 5% �����մϴ�";
    public int augmentationCount { get; set; }
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.attackPowerPercent += 0.05f;

        playerState.UpdateAttackPower();

        augmentationCount++;

        Debug.Log($"���� �ֹ���: {playerState.attackPower}");
        Debug.Log($"�ֹ��� �÷�: {playerState.attackPowerFlat}");
        Debug.Log($"�ֹ��� ����: {playerState.attackPowerPercent}");
    }
}
public class Speed : IAugmentation
{
    public string augmentationName { get; set; } = "�ż�";
    public string augmentationComment { get; set; } = "�̵� �ӵ��� 5% �����մϴ�.";
    public int augmentationCount { get; set; }
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.movementSpeedPercent += 0.05f;

        playerState.UpdateMovementSpeed();

        augmentationCount++;

        Debug.Log($"���� �̵��ӵ�: {playerState.movementSpeed}");
        Debug.Log($"�̵��ӵ� �÷�: {playerState.movementSpeedFlat}");
        Debug.Log($"�̵��ӵ� ����: {playerState.movementSpeedPercent}");
    }
}
public class Expansion : IAugmentation
{
    public string augmentationName { get; set; } = "��â";
    public string augmentationComment { get; set; } = "���� �Ǵ� ������ ũ�Ⱑ 5% Ŀ���ϴ�.";
    public int augmentationCount { get; set; } = 0;
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.skillExpansion += 0.05f;

        Debug.Log($"��ų ������: {playerState.skillExpansion}");
    }
}
public class Accuracy : IAugmentation
{
    public string augmentationName { get; set; } = "��Ȯ��";
    public string augmentationComment { get; set; } = "ġ��Ÿ Ȯ���� 5% �����մϴ�.";
    public int augmentationCount { get; set; } = 0;
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.criticalChance += 5f;

        Debug.Log($"ġ��Ÿ Ȯ��: {playerState.criticalChance}");
    }
}
public class Focus : IAugmentation
{
    public string augmentationName { get; set; } = "����";
    public string augmentationComment { get; set; } = "��ų�� ���� �ð��� 5% �����մϴ�.";
    public int augmentationCount { get; set; } = 0;
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.durationIncrease += 0.05f;

        Debug.Log($"���ӽð� ������: {playerState.durationIncrease }");
    }
}
public class RapidCast : IAugmentation
{
    public string augmentationName { get; set; } = "��� ����";
    public string augmentationComment { get; set; } = "���� �ӵ��� 5% �����մϴ�.";
    public int augmentationCount { get; set; } = 0;
    public int augmentationMaxCount { get; set; } = 5;

    public void Action()
    {
        PlayerState playerState = GameManager.GM.playerState;

        playerState.cooldownReduction += 0.05f;

        Debug.Log($"��Ÿ�� ���ҷ�: {playerState.cooldownReduction }");
    }
}
