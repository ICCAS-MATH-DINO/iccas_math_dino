using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator playerAnimator;

    public void GameOver()
    {
        playerAnimator.SetTrigger("GameOver");
        Debug.Log("게임오버: 슬픈 애니메이션 실행");
    }
}
