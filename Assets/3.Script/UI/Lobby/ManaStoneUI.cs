using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaStoneUI : MonoBehaviour
{
    [SerializeField] private GameObject previousPanel;

    [SerializeField] private Button createButton;
    [SerializeField] private Button[] gemButtons;

    [SerializeField] private Image getImage;

    [SerializeField] private Sprite[] gemImageList;

    [SerializeField] private Text gemNameText;
    [SerializeField] private Text gemSpecText;
    [SerializeField] private Text fragmentText;
    [SerializeField] private Text createButtonText;

    private bool[] isCreated;

    private List<string> keyList;

    private int cost;
    private int selectIndex;

    public void Awake()
    {
        isCreated = new bool[3];
        keyList = new List<string>();
    }

    private void OnEnable()
    {
        getImage.gameObject.SetActive(false);
        gemNameText.gameObject.SetActive(false);
        gemSpecText.gameObject.SetActive(false);

        createButton.interactable = false;

        fragmentText.text = $": {EquipmentManager.instance.manaStoneFragment}";
        isCreated = EquipmentManager.instance.isCreated;
    }
    private void OnDisable()
    {
        EquipmentManager.instance.isCreated = isCreated;
    }
    public void Start()
    {
        keyList.Add("FireBall");
        keyList.Add("FreezingPulse");
        keyList.Add("OrbOfStorms");
    }

    public void ExitButton()
    {
        previousPanel.SetActive(true);

        gameObject.SetActive(false);
    }
    public void GemButton(int idx)
    {
        Dictionary<string, SkillData> skillDataDict = EquipmentManager.instance.skillDataDict;

        selectIndex = idx;

        getImage.gameObject.SetActive(true);
        gemNameText.gameObject.SetActive(true);
        gemSpecText.gameObject.SetActive(true);

        getImage.sprite = gemImageList[selectIndex];
        gemNameText.text = skillDataDict[keyList[selectIndex]].skillName;
        gemSpecText.text = skillDataDict[keyList[selectIndex]].skillComment +
                                  $"쿨타임: {skillDataDict[keyList[selectIndex]].skillCoolTime}초\n\n" +
                                  $"지속시간: {skillDataDict[keyList[selectIndex]].skillDuration}초\n\n" +
                                  $"데미지 계수: {(skillDataDict[keyList[selectIndex]].skillCoefficient) * 100}%";


        if (!isCreated[selectIndex])
        {
            cost = skillDataDict[keyList[selectIndex]].makeCost;
            createButtonText.text = $"마석 만들기(필요 파편: {cost})";

            if (EquipmentManager.instance.manaStoneFragment >= cost)
            {
                createButton.interactable = true;
            }
            else
            {
                createButton.interactable = false;
            }
        }
        else
        {
            createButtonText.text = $"이미 만들어져 있습니다";
            createButton.interactable = false;

            EquipmentManager temp = EquipmentManager.instance;
            try
            {
                temp.usingSkill[0] = temp.skillArray[selectIndex];
            }
            catch
            {
                temp.usingSkill.Add(temp.skillArray[selectIndex]);
            }
        }
    }
    public void CreateButton()
    {
        EquipmentManager temp = EquipmentManager.instance;

        Dictionary<string, SkillData> skillDataDict = temp.skillDataDict;

        isCreated[selectIndex] = true;

        temp.manaStoneFragment -= cost;

        fragmentText.text = $": {temp.manaStoneFragment}";

        if (!isCreated[selectIndex])
        {
            cost = skillDataDict[keyList[selectIndex]].makeCost;
            createButtonText.text = $"마석 만들기(필요 파편: {cost})";

            if (EquipmentManager.instance.manaStoneFragment >= cost)
            {
                createButton.interactable = true;
            }
            else
            {
                createButton.interactable = false;
            }
        }
        else
        {
            createButtonText.text = $"이미 만들어져 있습니다";
            createButton.interactable = false;

            try
            {
                temp.usingSkill[0] = temp.skillArray[selectIndex];
            }
            catch
            {
                temp.usingSkill.Add(temp.skillArray[selectIndex]);
            }
        }
    }
}
