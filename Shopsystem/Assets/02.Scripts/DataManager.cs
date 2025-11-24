using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [Header("경제/자원")]
    public int Money;   //코인

    [Header("데이터 참조")]
    public PlayerDataSO playerData;
    public List<ItemDataSO> allItemData = new();

    public int CurrentSlot { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        Money = Mathf.Max(0, Money + amount);
        UIManager.Instance?.UpdateMoney(Money);
        Save();
    }

    public bool TrySpendMoney(int amount)
    {
        if (Money < amount) return false;
        Money -= amount;
        UIManager.Instance?.UpdateMoney(Money);
        Save();
        return true;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.Save();

        MirrorQuickSaveToCurrentSlotIfAny();
    }

    public void Load()
    {
        Money = PlayerPrefs.GetInt("Money", 0);
        UIManager.Instance?.UpdateMoney(Money);
    }
    public void SaveAllData(int slotIndex)
    {
        string prefix = $"Save{slotIndex}_";
        PlayerPrefs.SetInt(prefix + "Money", Money);

 
        //아이템으로 수정버전으로 올리기
        //foreach (var weapon in allItemData)
        //{
        //    if (weapon == null) continue;
        //    PlayerPrefs.SetInt(prefix + $"{weapon.weaponName}_Unlocked", weapon.isUnlocked ? 1 : 0);
        //}

        PlayerPrefs.Save();
        Debug.Log($"[DataManager] 슬롯 {slotIndex} 저장 완료");
    }
    public void LoadAllData(int slotIndex)
    {
        string prefix = $"Save{slotIndex}_";
        Money = PlayerPrefs.GetInt(prefix + "Money", 0);
        
        //아이템 수정버전으로 교체하기
        //if (playerData != null)
        //{
        //    playerData.maxHP = PlayerPrefs.GetFloat(prefix + "Player_MaxHP", playerData.maxHP);
        //    playerData.moveSpeed = PlayerPrefs.GetFloat(prefix + "Player_MoveSpeed", playerData.moveSpeed);
        //}
        //foreach (var weapon in allWeaponData)
        //{
        //    if (weapon == null) continue;
        //    weapon.isUnlocked = PlayerPrefs.GetInt(prefix + $"{weapon.weaponName}_Unlocked", 0) == 1;
        //}

        UIManager.Instance?.UpdateMoney(Money);

        Debug.Log($"[DataManager] 슬롯 {slotIndex} 불러오기 완료");
    }
    public bool HasSaveSlot(int slotIndex)
    {
        string prefix = $"Save{slotIndex}_";
        return PlayerPrefs.HasKey(prefix + "Money") || PlayerPrefs.HasKey(prefix + "Player_MaxHP");
    }

    public void SetCurrentSlot(int slotIndex)
    {
        CurrentSlot = slotIndex;
        PlayerPrefs.SetInt("LastSaveSlot", slotIndex);
        PlayerPrefs.Save();
    }

    private void MirrorQuickSaveToCurrentSlotIfAny()
    {
        if (CurrentSlot <= 0) return;
        string prefix = $"Save{CurrentSlot}_";
        PlayerPrefs.SetInt(prefix + "Money", Money);
        PlayerPrefs.Save();
    }
}
