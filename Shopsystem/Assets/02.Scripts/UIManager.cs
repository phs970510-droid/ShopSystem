using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("UI 참조")]
    [SerializeField] private TMPro.TMP_Text moneyText;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (DataManager.Instance != null)
        {
            UpdateMoney(DataManager.Instance.Money);
        }
    }

    public void UpdateMoney(int value)
    {
        if (moneyText != null) moneyText.text = $"$ {value}";
    }
}