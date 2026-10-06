namespace CodeQuest.Models;

public class BonusLevel
{
    public string Name { get; init; } = "";
    public string Subtitle { get; init; } = "";
    public string Description { get; init; } = "";
    public string PrimaryColor { get; init; } = "White";
    public string SecondaryColor { get; init; } = "White";
    public int QuestionCount { get; init; }
    public bool NoMistakes { get; init; }
    public string RewardTitle { get; init; } = "";
}