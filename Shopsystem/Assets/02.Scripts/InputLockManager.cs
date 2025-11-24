using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLockManager : MonoBehaviour
{
    public static InputLockManager Instance { get; private set; }
    private readonly HashSet<string> _locks = new HashSet<string>();
    public static bool Blocked => Instance != null && Instance._locks.Count > 0;

    public static System.Action<bool> OnLockStateChanged;

    [Header("커서/락 기본 정책")]
    [Tooltip("잠금이 있을 때 커서를 표시할지 여부(일반적으로 true)")]
    [SerializeField] private bool showCursorWhenBlocked = true;

    [Tooltip("잠금이 없을 때 커서를 잠글지 여부(일반적으로 true)")]
    [SerializeField] private bool lockCursorWhenFree = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //중복 방지
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        ApplyCursorPolicy();
    }

    //잠금
    public static void Acquire(string key)
    {
        if (string.IsNullOrEmpty(key)) return;
        if (Instance == null) CreateBootstrap();

        bool before = Blocked;
        Instance._locks.Add(key);
        if (Blocked != before)
        {
            Instance.ApplyCursorPolicy();
            OnLockStateChanged?.Invoke(Blocked);
        }
    }

    public static void Release(string key)
    {
        if (Instance == null || string.IsNullOrEmpty(key)) return;
        bool before = Blocked;
        Instance._locks.Remove(key);
        if (Blocked != before)
        {
            Instance.ApplyCursorPolicy();
            OnLockStateChanged?.Invoke(Blocked);
        }
    }

    private static void CreateBootstrap()
    {
        var go = new GameObject("[InputLockManager]");
        go.AddComponent<InputLockManager>();
    }

    private void ApplyCursorPolicy()
    {
        if (Blocked)
        {
            Cursor.visible = showCursorWhenBlocked;
            Cursor.lockState = showCursorWhenBlocked ? CursorLockMode.None : CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = !lockCursorWhenFree ? true : false;
            Cursor.lockState = lockCursorWhenFree ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
