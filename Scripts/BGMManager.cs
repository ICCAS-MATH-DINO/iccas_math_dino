using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource audioSource;

    public List<AudioClip> menuBGMList;
    public List<AudioClip> gameBGMList;

    private string currentScene = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;  // 한 곡 끝나면 다음 곡 재생하도록
            audioSource.playOnAwake = false;
            audioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1.0f); // 저장된 볼륨 불러오기
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayRandomBGM(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomBGM(currentScene);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayRandomBGM(scene.name);
    }

    void PlayRandomBGM(string sceneName)
    {
        currentScene = sceneName;

        if (sceneName == "GameScene")  // 게임 플레이 씬 이름
        {
            audioSource.clip = gameBGMList[Random.Range(0, gameBGMList.Count)];
        }
        else
        {
            audioSource.clip = menuBGMList[Random.Range(0, menuBGMList.Count)];
        }

        audioSource.Play();
    }

    // ✅ 슬라이더에서 호출할 함수
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume); // 사용자 설정 저장
    }
}
