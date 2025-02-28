using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PassiveNameSpace;

public class SetPassives : MonoBehaviour
{
    [SerializeField] private Sprite[] passivesIcon = new Sprite[9];
    [SerializeField] private Image[] icon = new Image[3];
    [SerializeField] private Text[] pName = new Text[3];
    [SerializeField] private Text[] pComment = new Text[3];

    private List<int> randomNumber;
    private int[] key = new int[3];
    private Dictionary<int, PassiveInterface> passives;

    private void Awake()
    {
        randomNumber = new List<int>();
        passives = new Dictionary<int, PassiveInterface>();

        for (int i = 0; i < 9; i++)
        {
            randomNumber.Add(i);
        }

        passives.Add(0, new health());
        passives.Add(1, new fortitude());
        passives.Add(2, new intelligence());
        passives.Add(3, new ferocity());
        passives.Add(4, new speed());
        passives.Add(5, new expansion());
        passives.Add(6, new accuracy());
        passives.Add(7, new focus());
        passives.Add(8, new rapidCast());
    }

    private void OnEnable()
    {
        SelectKey();
        SetIcon();
        SetName();
        SetCommnet();
    }
    public void SelectKey()
    {
        for (int i = 0; i < 3; i++)
        {
            int idx = Random.Range(0, 9-i);
            key[i] = randomNumber[idx];

            if(passives[key[i]].passiveCount == passives[key[i]].passiveMaxCount)
            {
                i--;
                continue;
            }

            randomNumber.RemoveAt(idx);
        }

        for (int i = 0; i < 3; i++)
        {
            randomNumber.Add(key[i]);
        }
    }
    public void SetIcon()
    {
        for (int i = 0; i < 3; i++)
        {
            icon[i].sprite = passivesIcon[key[i]];
        }
    }
    public void SetName()
    {
        for (int i = 0; i < 3; i++)
        {
            pName[i].text = passives[key[i]].passiveName;
        }
    }
    public void SetCommnet()
    {
        for (int i = 0; i < 3; i++)
        {
            pComment[i].text = passives[key[i]].passiveComment;
        }
    }

    public void SelectAction(int num)
    {
        PlayerState playerState = GameManager.GM.playerState;

        passives[key[num]].Action();

        playerState.setExp(-playerState.maxExp);
        playerState.LevelUp();
        playerState.SetMaxExp(playerState.maxExp * 0.3f);

        playerState.isLevelUp = false;

        Time.timeScale = 1f;
    }
}
