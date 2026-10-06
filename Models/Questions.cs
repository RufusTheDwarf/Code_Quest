namespace CodeQuest.Models;

public class Question
{
    public string Text { get; init; } = "";
    public string[] Answers { get; init; } = Array.Empty<string>();
    public int CorrectIndex { get; init; }
    public string Explanation { get; init; } = "";
    public string Hint { get; init; } = "";
    public Difficulty Difficulty { get; init; }
}