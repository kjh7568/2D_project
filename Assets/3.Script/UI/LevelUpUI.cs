using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private Text level;

    // Start is called before the first frame update
    void Start()
    {
        level.text = "현재 레벨: 1";
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        level.text = $"현재 레벨: {GameManager.GM.playerState.level}";
    }

    public void ManaRestore()
    {
        PlayerState playerState = GameManager.GM.playerState;

        float returnAmount = playerState.maxExp * 0.4f;

        playerState.setExp(-playerState.maxExp);
        playerState.setExp(returnAmount);

        playerState.isLevelUp = false;

        Time.timeScale = 1f;
    }
}
