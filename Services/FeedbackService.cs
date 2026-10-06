using CodeQuest.Models;

namespace CodeQuest.Services;

public static class FeedbackService
{
    private static readonly Random _rng = new();

    public static string[] BuildCorrectFeedback(Question q)
    {
        var lines = new List<string>
        {
            "",
            "✦ BONNE RÉPONSE",
            "",
            q.Explanation
        };

        if (!string.IsNullOrEmpty(q.Hint))
        {
            lines.Add("");
            lines.Add("ASTUCE MÉMOIRE");
            lines.Add(q.Hint);
        }

        return lines.ToArray();
    }

    public static string[] BuildWrongFeedback(Question q, string selectedAnswer)
    {
        var lines = new List<string>
        {
            "",
            "✦ MAUVAISE RÉPONSE",
            "",
            $"Bonne réponse : {q.Answers[q.CorrectIndex]}",
            "",
            q.Explanation
        };

        if (!string.IsNullOrEmpty(q.Hint))
        {
            lines.Add("");
            lines.Add("POUR S'EN SOUVENIR");
            lines.Add(q.Hint);
        }

        return lines.ToArray();
    }

    public static string[] BuildDefeatLines(Enemy enemy, int xpGained)
    {
        return new[]
        {
            "",
            $"✦ {enemy.Name} VAINCU !",
            "",
            $"+{xpGained} XP",
            ""
        };
    }
}