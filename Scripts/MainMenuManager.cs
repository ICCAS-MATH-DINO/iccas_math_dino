using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;     // 메인 메뉴 패널
    public GameObject optionsPanel;      // 옵션 패널
    public GameObject mobileExitNoticePanel;  // 모바일 종료 안내창

    public void OnStartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void OnOptionsButton()
    {
        optionsPanel.SetActive(true);  // 옵션 패널 활성화
    }

    public void OnBackButton()
    {
        optionsPanel.SetActive(false);
    }

    public void OnExitButton()
    {
#if UNITY_ANDROID || UNITY_IOS
        mobileExitNoticePanel.SetActive(true);
#else
        Application.Quit();
#endif
    }

    public void CloseExitNotice()
    {
        mobileExitNoticePanel.SetActive(false);
    }
}
