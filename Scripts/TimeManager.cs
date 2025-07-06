using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; } // ✅ 싱글톤

    [Header("UI Elements")]
    public Slider timeSlider;
    public TextMeshProUGUI timeText;
    public LocalizedString estimateTimeLocalized;

    [Header("Time Settings")]
    public float maxTime = 60f;
    private float currentTime;
    private bool isRunning = true;

    private bool localizationReady = false;

    void Awake()
    {
        // ✅ 싱글톤 인스턴스 설정
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentTime = maxTime;
        StartCoroutine(InitializeLocalizationAndStart());
    }

    IEnumerator InitializeLocalizationAndStart()
    {
        yield return LocalizationSettings.InitializationOperation;
        localizationReady = true;
        UpdateUI();
    }

    void Update()
    {
        if (!isRunning || !localizationReady) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;
            GameOver();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (!localizationReady) return;

        timeSlider.value = currentTime / maxTime;
        int displayTime = Mathf.CeilToInt(currentTime);

        estimateTimeLocalized.Arguments = new object[] { displayTime };

        var operation = estimateTimeLocalized.GetLocalizedStringAsync();
        operation.Completed += handle =>
        {
            timeText.text = handle.Result;
        };
    }

    public void AddTime(float amount)
    {
        currentTime += amount;
        if (currentTime > maxTime) currentTime = maxTime;
        UpdateUI();
    }

    public void SubtractTime(float amount)
    {
        currentTime -= amount;
        if (currentTime < 0) currentTime = 0;
        UpdateUI();
    }

    public void PauseTime()
    {
        isRunning = false;
    }

    public void ResumeTime()
    {
        isRunning = true;
    }

    void GameOver()
    {
        Debug.Log("게임 오버!");
        // 게임 오버 시 동작 추가
    }
}
