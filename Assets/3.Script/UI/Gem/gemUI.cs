using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gemUI : MonoBehaviour
{
    [SerializeField] private GameObject previousUI;
    [SerializeField] private GameObject objectPanel;
    [SerializeField] private GameObject createManaStonePanel;
    [SerializeField] private GameObject engravingCreationPanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            GameManager.GM.isOpenManaStoneWindow = false;
            previousUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void CreateManaStoneButton()
    {
        createManaStonePanel.SetActive(true);
        objectPanel.SetActive(false);
    }
    public void EngraveGemButton()
    {
        engravingCreationPanel.SetActive(true);
        objectPanel.SetActive(false);
    }
}
