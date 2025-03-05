using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillNameSpace;

public class CreateManaStoneUI : MonoBehaviour
{
    [SerializeField] private GameObject previousPanel;
    [SerializeField] private GameObject gemListPanel;

    [SerializeField] private Image[] gemButtonImages;

    [SerializeField] private Text GemName;
    [SerializeField] private Text GemSpec;

    [SerializeField] private Button ReplaceGemButton;
    [SerializeField] private Button CreateGemButton;


    private SkillManager skillManager;

    public int number;

    private void Awake()
    {
        GameObject.Find("SkillManager").TryGetComponent(out skillManager);
    }
    private void OnEnable()
    {
        SetObject();
    }
    public void Exitbutton()
    {
        previousPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    public void GemButton(int num)
    {
        number = num;

        SkillInfoInterface cur = null;

        GemName.gameObject.SetActive(true);
        GemSpec.gameObject.SetActive(true);

        try
        {
            cur = skillManager.usingSkill[num].GetComponent<SkillInfoInterface>();
            ReplaceGemButton.gameObject.SetActive(true);
            CreateGemButton.gameObject.SetActive(false);

            GemName.text = skillManager.skillDataDict[cur.skillKey].skillName;
            GemSpec.text = skillManager.skillDataDict[cur.skillKey].skillComment+
                                  $"쿨타임: {skillManager.skillDataDict[cur.skillKey].skillCoolTime}초\n\n" +
                                  $"지속시간: {skillManager.skillDataDict[cur.skillKey].skillDuration}초\n\n" +
                                  $"데미지 계수: {(skillManager.skillDataDict[cur.skillKey].skillCoefficient) * 100}%";

        }
        catch
        {
            GemName.gameObject.SetActive(false);
            GemSpec.gameObject.SetActive(false);

            ReplaceGemButton.gameObject.SetActive(false);
            CreateGemButton.gameObject.SetActive(true);
        }



    }
    public void CreateButton()
    {
        gemListPanel.SetActive(true);
    }

    public void SetObject()
    {
        for (int i = 0; i < skillManager.usingSkill.Count; i++)
        {
            SkillInfoInterface cur = skillManager.usingSkill[i].GetComponent<SkillInfoInterface>();
            gemButtonImages[i].sprite = skillManager.gemDic[cur.skillKey];
        }

        GemName.gameObject.SetActive(false);
        GemSpec.gameObject.SetActive(false);
        ReplaceGemButton.gameObject.SetActive(false);
        CreateGemButton.gameObject.SetActive(false);
    }
}
