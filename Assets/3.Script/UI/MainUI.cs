using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    private PlayerState playerState;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider expSlider;
    [SerializeField] private Text timerText;

    private float elapsedTime;

    void Start()
    {
        playerState = GameManager.GM.playerState;

        healthSlider.maxValue = playerState.maxHp;
        healthSlider.value = playerState.hp;

        expSlider.maxValue = playerState.maxExp;
        expSlider.value = playerState.exp;
    }
    void Update()
    {
        healthSlider.maxValue = playerState.maxHp;
        healthSlider.value = playerState.hp;

        expSlider.maxValue = playerState.maxExp;
        expSlider.value = playerState.exp;

        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SetVidible()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);

            GameObject canvas = GameObject.Find("Player").transform.Find("RewardInterface")?.gameObject;
            canvas.SetActive(true);
        }
    }
}
