using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public QuizLoader quizLoader;
    public TextMeshProUGUI questionText;
    public TextMeshPro option1Text;
    public TextMeshPro option2Text;
    public Slider countdownSlider;

    public Transform option1Object;
    public Transform option2Object;
    public Transform playerTransform;

    public GameObject quizCanvas;
    public float quizDuration = 10f;

    private QuizData currentQuiz;
    private bool isQuizActive = false;
    private float quizTimer = 0f;

    private bool waitingForNextQuiz = false;
    private float waitTimer = 0f;
    private float waitDuration = 3f;

    private float playerSpeed = 5f;

    void Start()
    {
        quizCanvas.SetActive(false);
        TriggerQuiz();
    }

    void Update()
    {
        if (isQuizActive)
        {
            quizTimer += Time.deltaTime;
            countdownSlider.value = Mathf.Clamp01((quizDuration - quizTimer) / quizDuration);

            if (quizTimer >= quizDuration)
            {
                OnOptionCollision(false); // 시간 초과 = 오답 처리
            }
        }
        else if (waitingForNextQuiz)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitDuration)
            {
                waitingForNextQuiz = false;
                TriggerQuiz();
            }
        }
    }

    public void TriggerQuiz()
    {
        currentQuiz = quizLoader.GetRandomQuiz();
        if (currentQuiz == null)
        {
            Debug.LogError("퀴즈 데이터가 없습니다.");
            return;
        }

        questionText.text = currentQuiz.question;
        option1Text.text = currentQuiz.option1;
        option2Text.text = currentQuiz.option2;

        // 옵션 오브젝트 다시 활성화
        option1Object.gameObject.SetActive(true);
        option2Object.gameObject.SetActive(true);

        // 위치 초기화
        Vector3 basePos = playerTransform.position + playerTransform.forward * (playerSpeed * quizDuration);
        option1Object.position = basePos + new Vector3(-1.5f, 0, 0);
        option2Object.position = basePos + new Vector3(1.5f, 0, 0);

        quizCanvas.SetActive(true);
        isQuizActive = true;
        quizTimer = 0f;
    }

    public void OnOptionCollision(bool isCorrect)
    {
        if (!isQuizActive) return;

        if (isCorrect)
        {
            Debug.Log("정답!");
            TimeManager.Instance.AddTime(10f);
        }
        else
        {
            Debug.Log("오답!");
            TimeManager.Instance.SubtractTime(5f);
            Handheld.Vibrate();
        }

        // 옵션 오브젝트 비활성화
        option1Object.gameObject.SetActive(false);
        option2Object.gameObject.SetActive(false);

        EndQuiz();
    }

    void EndQuiz()
    {
        isQuizActive = false;
        quizCanvas.SetActive(false);

        waitingForNextQuiz = true;
        waitTimer = 0f;
    }
}
