using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private Text survivalTimeText;
    [SerializeField] private Text acquiredManaStoneFragmentsText;

    public float elapsedTime;
    
    private void Awake()
    {
        if (GameObject.Find("InGameCanvas").TryGetComponent(out MainUI m))
        {
            elapsedTime = m.elapsedTime;
        }
    }
    private void OnEnable()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        survivalTimeText.text = "생존시간: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        acquiredManaStoneFragmentsText.text = $"획득한 마석 조각: {GameManager.GM.instansManaStoneFragment}";
    }
    public void GotoMainButton()
    {
        SceneManager.LoadScene("Lobby");
    }
}
