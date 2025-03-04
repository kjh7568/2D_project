using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillNameSpace;

public class CreateManaStoneUI : MonoBehaviour
{
    [SerializeField] private Image[] gemButtonImages;
    
    private SkillManager skillManager;

    private void Awake()
    {
        GameObject.Find("CreateManaStonePanel").TryGetComponent(out skillManager);
    }
    private void OnEnable()
    {
        for (int i = 0; i < skillManager.usingSkill.Count; i++)
        {
            //SkillInfoInterface cur = skillManager.usingSkill[i].TryGetComponent(curSkill);
            //gemButtonImages[i].sprite = skillManager.gemDic[/*여기에 스킬 명 있어야 하는데..*/]
        }
    }
}
