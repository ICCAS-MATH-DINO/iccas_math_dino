// QuizData.cs
[System.Serializable]
public class QuizData
{
    public string question;
    public string option1;
    public string option2;
    public int correctOption; // 1 또는 2, 정답이 option1이면 1
}
