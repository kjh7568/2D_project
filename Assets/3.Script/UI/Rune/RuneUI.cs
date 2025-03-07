using SkillNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneUI : MonoBehaviour
{
    [SerializeField] private GameObject previousPanel;
    [SerializeField] private GameObject background;

    [SerializeField] private Button[] gemButtons;
    [SerializeField] private Button[] runeButtons;
    [SerializeField] private Button exitButton;

    [SerializeField] private Image[] runeImages;

    [SerializeField] private Text[] runeTexts;
    [SerializeField] private Text[] runeNameTexts;
    [SerializeField] private Text gemName;

    private SkillManager skillManager;
    private void Awake()
    {
        GameObject.Find("SkillManager").TryGetComponent(out skillManager);
    }
    private void OnEnable()
    {
        gemName.gameObject.SetActive(false);

        for (int i = 0; i < runeImages.Length; i++)
        {
            runeImages[i].gameObject.SetActive(false);
            runeNameTexts[i].gameObject.SetActive(false);
            runeButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < skillManager.usingSkill.Count; i++)
        {
            SkillInfoInterface cur = skillManager.usingSkill[i].GetComponent<SkillInfoInterface>();

            if(gemButtons[i].gameObject.TryGetComponent(out Image image))
            {
                image.sprite = skillManager.gemDic[cur.skillKey];
            }
        }
    }

    public void ExitButton()
    {
        previousPanel.SetActive(true);

        gameObject.SetActive(false);
    }
}
