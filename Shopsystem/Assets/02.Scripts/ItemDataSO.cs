using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Game/Item Data")]
public class ItemDataSO : MonoBehaviour
{
    [Header("기본")]
    public string ItemName = "Pistol";
    [Tooltip("발당 데미지")]
    public float damage = 10.0f;
    [Tooltip("연사 간격(초)")]
    public float fireRate = 1.0f;
    [Tooltip("재장전 시간(초)")]
    public float reloadTime = 1.0f;
    [Tooltip("한 발씩 장전되는 무기인가?")]
    public bool isSingleLoad = false;

    [Header("탄약 설정")]
    [Tooltip("탄창당 탄 수")]
    public int maxAmmo = 12;
    [Tooltip("최대 탄창 수")]
    public int maxMag = 5;

    [Header("해금 여부")]
    public bool isUnlocked = true;

    [Header("관통 설정")]
    [Tooltip("탄환이 관통 가능한 적 수 (0 = 관통 없음)")]
    public int penetration = 0;

    [Header("사운드 설정")]
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip pumpSound;
}
