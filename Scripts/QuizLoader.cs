using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuizLoader : MonoBehaviour
{
    public string fileNamePrefix = "quiz";
    public SystemLanguage language = SystemLanguage.Korean;
    public List<QuizData> quizzes = new List<QuizData>();

    void Awake()
    {
        LoadQuizFile();
    }

    void LoadQuizFile()
    {
        string langCode = language switch
        {
            SystemLanguage.Korean => "ko",
            SystemLanguage.English => "en",
            SystemLanguage.German => "de",
            _ => "en"
        };

        string path = Path.Combine(Application.streamingAssetsPath, $"{fileNamePrefix}_{langCode}.txt");

        if (!File.Exists(path))
        {
            Debug.LogError("퀴즈 파일 없음: " + path);
            return;
        }

        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');

            if (parts.Length != 4)
            {
                Debug.LogWarning($"잘못된 형식의 줄: {line}");
                continue;
            }

            if (!int.TryParse(parts[3].Trim(), out int correctOption))
            {
                Debug.LogWarning($"정답 번호 파싱 실패: {parts[3]}");
                continue;
            }

            quizzes.Add(new QuizData
            {
                question = parts[0].Trim(),
                option1 = parts[1].Trim(),
                option2 = parts[2].Trim(),
                correctOption = correctOption
            });
        }

        Debug.Log($"퀴즈 {quizzes.Count}개 로드 완료");
    }

    public QuizData GetRandomQuiz()
    {
        if (quizzes.Count == 0)
        {
            Debug.LogError("퀴즈가 비어 있음");
            return null;
        }

        return quizzes[Random.Range(0, quizzes.Count)];
    }
}
