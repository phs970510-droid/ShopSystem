using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Shop Canvas Root")]
    [SerializeField] private GameObject shopCanvas;

    [Header("Optional Button")]
    [SerializeField] private Button shopOpenButton;

    private bool isOpen = false;

    void Start()
    {
        if (shopCanvas != null)
            shopCanvas.SetActive(false);

        if (shopOpenButton != null)
            shopOpenButton.onClick.AddListener(OnShopButtonPressed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    public void OnShopButtonPressed()
    {
        ToggleShop();
    }

    public void ToggleShop()
    {
        if (isOpen)
            CloseShop();
        else
            OpenShop();
    }

    public void OpenShop()
    {
        isOpen = true;

        if (shopCanvas != null)
            shopCanvas.SetActive(true);

        InputLockManager.Acquire("Shop");
    }

    public void CloseShop()
    {
        isOpen = false;

        if (shopCanvas != null)
            shopCanvas.SetActive(false);

        InputLockManager.Release("Shop");
    }
}
