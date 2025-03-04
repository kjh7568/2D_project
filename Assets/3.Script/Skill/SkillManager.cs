using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillNameSpace;
using System.IO; // 파일 입출력 사용
using LitJson;

namespace SkillNameSpace
{
    public interface SkillInfoInterface
    {
        string skillKey { get; set; }
        float skillCoolTime { get; set; }
        float skillMovementSpeed { get; set; }
        float skillDuration { get; set; }
        float skillCoefficient { get; set; }
        public void UseSkill(float cooltime);
    }
}

//게임 시작할 때 스킬 추가하도록 하기
public class SkillManager : MonoBehaviour
{
    [SerializeField] private Sprite[] gemImages;
    public Dictionary<string, Sprite> gemDic;

    [SerializeField] private List<GameObject> skillArray;
    protected Dictionary<string, SkillData> skillDataDict;
    private bool[] isSkillUse;
    public List<GameObject> usingSkill;

    private void Awake()
    {
        usingSkill = EquipmentManager.instance.usingSkill;

        SetGemDictionary();

        JsonMapper.RegisterExporter<float>((float value, JsonWriter writer) => writer.Write(value));
        JsonMapper.RegisterImporter<double, float>(input => (float)input);

        string skillJson = Resources.Load<TextAsset>("SkillData").text;

        skillDataDict = JsonMapper.ToObject<Dictionary<string, SkillData>>(skillJson);

        isSkillUse = new bool[skillArray.Count];

        for (int i = 0; i < skillArray.Count; i++)
        {
            isSkillUse[i] = false;
        }
    }
    private void Update()
    {
        for (int i = 0; i < skillArray.Count; i++)
        {
            if (!isSkillUse[i])
            {
                if (Vector2.Distance(GameManager.GM.playerController.transform.position, GetClosestEnemy()) < 10f)
                {
                    isSkillUse[i] = true;
                    skillArray[i].TryGetComponent(out SkillInfoInterface SI);

                    StartCoroutine(SkillCoolDown_Co(SI));
                }
            }
        }
    }


    public void SetGemDictionary()
    {
        if (gemImages == null || gemImages.Length == 0)
        {
            return;
        }

        gemDic = new Dictionary<string, Sprite>();

        gemDic.Add("FireBall", gemImages[0]);
    }
    public IEnumerator SkillCoolDown_Co(SkillInfoInterface usedSkill)
    {
        while (true)
        {
            usedSkill.UseSkill(skillDataDict[usedSkill.skillKey].skillCoolTime);

            yield return new WaitForSeconds(usedSkill.skillCoolTime);
        }
    }
    public Vector2 GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(GameManager.GM.playerController.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        if (closest == null)
        {
            Debug.LogError("가장 가까운 적을 찾을 수 없습니다!");
            return GameManager.GM.playerController.transform.position; // 플레이어 위치 반환 (임시 대체)
        }

        return closest.transform.position;
    }

    // 스킬 풀링 구현
    private void FireBallFlooring()
    {

    }
}
