using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SetPassives : MonoBehaviour
{
    [FormerlySerializedAs("passivesIcon")] [SerializeField] private Sprite[] augmentationIcon = new Sprite[9];
    [SerializeField] private Image[] icon = new Image[3];
    [FormerlySerializedAs("pName")] [SerializeField] private Text[] aName = new Text[3];
    [FormerlySerializedAs("pComment")] [SerializeField] private Text[] aComment = new Text[3];

    private List<int> randomNumber;
    
    private readonly int[] key = new int[3];
    private readonly List<IAugmentation> augmentations = new();

    private void Awake()
    {
        randomNumber = new List<int>();

        for (int i = 0; i < 9; i++)
        {
            randomNumber.Add(i);
        }

        augmentations.Add(new Health());
        augmentations.Add(new Fortitude());
        augmentations.Add(new Intelligence());
        augmentations.Add(new Ferocity());
        augmentations.Add(new Speed());
        augmentations.Add(new Expansion());
        augmentations.Add(new Accuracy());
        augmentations.Add(new Focus());
        augmentations.Add(new RapidCast());
    }

    private void OnEnable()
    {
        SelectKey();
        SetUI();
    }

    private void SelectKey()
    {
        for (int i = 0; i < 3; i++)
        {
            int idx = Random.Range(0, 9-i);
            key[i] = randomNumber[idx];

            if(augmentations[key[i]].augmentationCount == augmentations[key[i]].augmentationMaxCount)
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
    
    private void SetUI()
    {
        for (int i = 0; i < 3; i++)
        {
            icon[i].sprite = augmentationIcon[key[i]];
            aName[i].text = augmentations[key[i]].augmentationName;
            aComment[i].text = augmentations[key[i]].augmentationComment;
        }
    }

    public void SelectAction(int num)
    {
        PlayerState playerState = GameManager.GM.playerState;

        augmentations[key[num]].Action();

        playerState.setExp(-playerState.maxExp);
        playerState.LevelUp();
        playerState.SetMaxExp(playerState.maxExp * 0.3f);

        playerState.isLevelUp = false;

        Time.timeScale = 1f;
    }
}
