using UnityEngine;

public class QuizOptionCollider : MonoBehaviour
{
    public bool isCorrect; // Inspector에서 정답 여부 설정

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuizManager quizManager = FindObjectOfType<QuizManager>();
            if (quizManager != null)
            {
                quizManager.OnOptionCollision(isCorrect);
            }
        }
    }
}
