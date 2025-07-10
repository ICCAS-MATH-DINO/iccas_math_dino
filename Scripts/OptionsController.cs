using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class OptionsController : MonoBehaviour
{
    [Header("Audio Sliders")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("Panels")]
    public GameObject optionsPanel;

    [Header("Language Flags")]
    public GameObject buttonUS;
    public GameObject buttonUK;
    public GameObject buttonKR;
    public GameObject buttonDE;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    void Start()
    {
        void Start()
{
    if (bgmSlider != null)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);

    if (sfxSource != null)
        sfxSlider.value = sfxSource.volume;
}
    }

    public void OnBackButton()
    {
        // 옵션 패널 끄기
        optionsPanel.SetActive(false);
    }

    public void OnBgmVolumeChanged(float value)
    {
    if (BGMManager.Instance != null)
        BGMManager.Instance.SetVolume(value);
    }

    public void OnSfxVolumeChanged(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;
    }

   public void OnLanguageSelected(string langCode)
{
    PlayerPrefs.SetString("language", langCode);
    Debug.Log("Language set to: " + langCode);

    Locale selectedLocale = null;

    // 코드로 Locale을 찾아서 설정
    foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
    {
        if (locale.Identifier.Code == langCode)
        {
            selectedLocale = locale;
            break;
        }
    }

    if (selectedLocale != null)
    {
        LocalizationSettings.SelectedLocale = selectedLocale;
        Debug.Log("Changed locale to: " + selectedLocale.Identifier);
    }
    else
    {
        Debug.LogWarning("Locale not found for code: " + langCode);
    }
}

    public void OnAnalysisButton()
    {
        SceneManager.LoadScene("AnalysisScene");
    }
}
