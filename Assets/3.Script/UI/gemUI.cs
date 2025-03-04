using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gemUI : MonoBehaviour
{
    [SerializeField] private GameObject previousUI;
    [SerializeField] private GameObject objectPanel;
    [SerializeField] private GameObject createManaStonePanel;

    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        createManaStonePanel.SetActive(false);
    }

    public void ExitButton()
    {
        Time.timeScale = 1f;
        GameManager.GM.isOpenManaStoneWindow = false;
        previousUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CreateManaStoneButton()
    {
        //GameManager.GM.isOpenManaStoneWindow = false;
        createManaStonePanel.SetActive(true);
        objectPanel.SetActive(false);
    }
}
