using UnityEngine;

public class QuizCollisionHandler : MonoBehaviour
{
    public TimeManager timeManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Correct"))
        {
            Debug.Log("정답 선택지에 충돌!");
            timeManager.AddTime(10); // +10초
        }
        else if (other.CompareTag("Wrong"))
        {
            Debug.Log("오답 선택지에 충돌!");
            timeManager.AddTime(-5); // -5초
            Handheld.Vibrate(); // 모바일 진동
        }

        Destroy(other.gameObject); // 충돌 후 선택지 제거
    }
}
