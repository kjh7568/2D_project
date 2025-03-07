using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemListUI : MonoBehaviour
{
    [SerializeField] private Button[] previousButtons;

    [SerializeField] private Text gemNameText;
    [SerializeField] private Text gemSpecText;
    [SerializeField] private Text createButtonText;
    [SerializeField] private Text FragmentText;

    [SerializeField] private Button createButton;

    [SerializeField] private GameObject panel;

    private int idx;
    private int selectNum;
    private int cost;

    private CreateManaStoneUI createManaStoneUI;
    private void Awake()
    {
        panel.TryGetComponent(out createManaStoneUI);
    }
    private void OnEnable()
    {
        idx = createManaStoneUI.number;

        for (int i = 0; i < previousButtons.Length; i++)
        {
            previousButtons[i].interactable = false;
        }
        createButton.interactable = false;

        FragmentText.text = $": {GameManager.GM.instansManaStoneFragment}";
    }
    private void OnDisable()
    {
        for (int i = 0; i < previousButtons.Length; i++)
        {
            previousButtons[i].interactable = true;
        }
        createButton.interactable = false;
    }

    public void ExitButton()
    {
        for (int i = 0; i < previousButtons.Length; i++)
        {
            previousButtons[i].interactable = true;
        }

        gameObject.SetActive(false);

        gemNameText.gameObject.SetActive(false);
        gemSpecText.gameObject.SetActive(false);
        createButton.interactable = false;
    }

    public void SelectGemButton(int num)
    {
        SkillManager skillManager;
        GameObject.Find("SkillManager").TryGetComponent(out skillManager);

        selectNum = num;

        string key = "";

        switch (num)
        {
            case 0:
                key = "FireBall";
                break;
            case 1:
                key = "FreezingPulse";
                break;
            case 2:
                key = "OrbOfStorms";
                break;
            default:
                break;
        }

        cost = skillManager.skillDataDict[key].makeCost;

        if (skillManager.usingSkill.Contains(EquipmentManager.instance.skillArray[selectNum]))
        {
            createButton.interactable = false;
            createButtonText.text = "이미 장착 중입니다.";
        }
        else
        {
            createButtonText.text = $"제작(필요 파편: {cost})";
            
            if (GameManager.GM.instansManaStoneFragment >= cost)
            {
                createButton.interactable = true;
            }
            else
            {
                createButton.interactable = false;
            }
        }

        if (!gemSpecText.gameObject.activeSelf)
        {
            gemSpecText.gameObject.SetActive(true);
            gemNameText.gameObject.SetActive(true);
        }

        gemNameText.text = skillManager.skillDataDict[key].skillName;
        gemSpecText.text = skillManager.skillDataDict[key].skillComment +
                              $"쿨타임: {skillManager.skillDataDict[key].skillCoolTime}초\n\n" +
                              $"지속시간: {skillManager.skillDataDict[key].skillDuration}초\n\n" +
                              $"데미지 계수: {(skillManager.skillDataDict[key].skillCoefficient) * 100}%";
    }
    public void CreateGemButton()
    {
        GameObject.Find("SkillManager").TryGetComponent(out SkillManager skillManager);

        GameManager.GM.instansManaStoneFragment -= cost;

        try
        {
            skillManager.usingSkill[idx] = EquipmentManager.instance.skillArray[selectNum];
        }
        catch
        {
            skillManager.usingSkill.Add(EquipmentManager.instance.skillArray[selectNum]);
        }

        createManaStoneUI.SetObject();
        skillManager.ResetCoroutine();

        FragmentText.text = $": {GameManager.GM.instansManaStoneFragment}";

        gameObject.SetActive(false);
    }
}
