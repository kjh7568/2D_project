//Hello World!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM { get; private set; }
    public PlayerController playerController;
    public PlayerState playerState;

    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject levelUpUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gemUI;

    public int instansManaStoneFragment;

    public bool isOpenManaStoneWindow;
    private void Awake()
    {
        instansManaStoneFragment = 0;
        if (GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }

        isOpenManaStoneWindow = false;
    }

    private void Update()
    {
        ConvertUI();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOpenManaStoneWindow = true;
        }
    }

    private void OnDestroy()
    {
        EquipmentManager.instance.manaStoneFragment += instansManaStoneFragment;
    }

    private void ConvertUI()
    {
        GameObject canvas;

        try
        {
            canvas = GameObject.Find("Player").transform.Find("RewardInterface")?.gameObject;
        }
        catch
        {
            return;
        }

        if (playerState.isLevelUp)
        {
            mainUI.SetActive(false);
            canvas.SetActive(true);
            levelUpUI.SetActive(true);

            Time.timeScale = 0f;
        }
        else if(playerState.hp <= 0)
        {
            gameOverUI.SetActive(true);
            mainUI.SetActive(false);

            Time.timeScale = 0f;
        }
        else if(isOpenManaStoneWindow)
        {
            gemUI.SetActive(true);
            mainUI.SetActive(false);

            Time.timeScale = 0f;
        }
        else
        {
            if (canvas.activeSelf)
            {
                canvas.SetActive(false);
            }

            if (!mainUI.activeSelf)
            {
                mainUI.SetActive(true);
            }

            if (levelUpUI.activeSelf)
            {
                levelUpUI.SetActive(false);
            }
        }
    }
}
