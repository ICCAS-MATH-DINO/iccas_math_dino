using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Localization.Settings;



public class QuizLoader : MonoBehaviour
{
    public string fileNamePrefix = "quiz"; // 기본 파일 이름 접두사
    public string language = "English"; // Inspector에서는 사용하지 않음
    public List<QuizData> quizzes = new List<QuizData>();

    void Awake()
    {
        LoadQuizByLocale();
    }

   void LoadQuizByLocale()
{
    string localeCode = LocalizationSettings.SelectedLocale?.Identifier.Code;
    string fileName = fileNamePrefix;

    if (localeCode == "en-US" || localeCode == "en-UK")
        fileName += "_en";
    else if (localeCode == "ko-KR")
        fileName += "_ko";
    else if (localeCode == "de-DE")
        fileName += "_de";
    else
        fileName += "_en";  // fallback

    string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".txt");

    if (!File.Exists(filePath))
    {
        Debug.LogError("퀴즈 파일이 존재하지 않습니다: " + filePath);
        return;
    }

    quizzes.Clear();
    string[] lines = File.ReadAllLines(filePath);

    foreach (var line in lines)
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

    Debug.Log($"[{fileName}.txt] 퀴즈 {quizzes.Count}개 로드 완료");
}


    public QuizData GetRandomQuiz()
    {
        if (quizzes.Count == 0) return null;
        return quizzes[Random.Range(0, quizzes.Count)];
    }
}
