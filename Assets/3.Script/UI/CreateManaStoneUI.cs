using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillNameSpace;

public class CreateManaStoneUI : MonoBehaviour
{
    [SerializeField] private GameObject previousPanel;

    [SerializeField] private Image[] gemButtonImages;

    [SerializeField] private Text GemName;
    [SerializeField] private Text GemSpec;

    [SerializeField] private Button ReplaceGemButton;
    [SerializeField] private Button CreateGemButton;


    private SkillManager skillManager;

    private void Awake()
    {
        GameObject.Find("SkillManager").TryGetComponent(out skillManager);
    }
    private void OnEnable()
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
    public void Exitbutton()
    {
        previousPanel.SetActive(true);
        gameObject.SetActive(false);    
    }
}
