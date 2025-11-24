using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [Header("플레이어 데이터")]
    public PlayerDataSO playerData;

    [Header("아이템 데이터")]
    public List<ItemDataSO> shopItems;

    [Header("가격 설정")]
    public int hpUpgradeCost = 200;
    public int speedUpgradeCost = 150;

    //체력에서 아이템으로 로직 변경 예정
    //public void BuyStat_HP()
    //{
    //    if (DataManager.Instance.TrySpendMoney(hpUpgradeCost))
    //    {
    //        playerData.UpgradeHP(20f);
    //        Debug.Log("HP 업그레이드 완료!");
    //    }
    //    else Debug.Log("돈이 부족합니다!");
    //}
}
