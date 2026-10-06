using CodeQuest.Data;
using CodeQuest.Models;

namespace CodeQuest.Services;

public class GameSession
{
    public DifficultyConfig Config { get; }
    public Player Player { get; } = new();
    public List<Enemy> Enemies { get; }
    public int CurrentEnemyIndex { get; private set; }
    public List<Question> Questions { get; }
    private int _questionIndex;

    // ── Powerups ──────────────────────────────────────────
    public List<PowerUp> ActivePowerUps { get; } = new();

    // ── Flags de partie ────────────────────────────────────
    public bool BonusQuestionAsked { get; set; }
    public bool BossWarningShown { get; set; }

    public GameSession(Difficulty difficulty)
    {
        Config = DifficultyConfigs.Get(difficulty);
        Enemies = EnemyCatalog.BuildEnemies(Config);
        Questions = QuestionBank.Build()
            .Where(q => q.Difficulty <= difficulty)
            .OrderBy(_ => Guid.NewGuid())
            .ToList();
        _questionIndex = 0;
    }

    public Enemy? CurrentEnemy =>
        CurrentEnemyIndex < Enemies.Count ? Enemies[CurrentEnemyIndex] : null;

    public bool IsVictory => CurrentEnemyIndex >= Enemies.Count;
    public bool IsDefeat => !Player.IsAlive;

    public void AdvanceEnemy() => CurrentEnemyIndex++;

    public Question NextQuestion()
    {
        if (_questionIndex >= Questions.Count)
        {
            var reshuffled = Questions.OrderBy(_ => Guid.NewGuid()).ToList();
            Questions.Clear();
            Questions.AddRange(reshuffled);
            _questionIndex = 0;
        }
        return Questions[_questionIndex++];
    }

    public (string[] shuffled, int correctIndex) ShuffleAnswers(Question q)
    {
        var shuffled = (string[])q.Answers.Clone();
        var rng = new Random();
        int n = shuffled.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (shuffled[k], shuffled[n]) = (shuffled[n], shuffled[k]);
        }
        int newCorrect = Array.IndexOf(shuffled, q.Answers[q.CorrectIndex]);
        return (shuffled, newCorrect);
    }

    public int ScorePercent
    {
        get
        {
            int total = Player.CorrectCount + Player.WrongCount;
            return total == 0 ? 0 : (int)Math.Round(100.0 * Player.CorrectCount / total);
        }
    }

    // ── Powerups ──────────────────────────────────────────

    public void AddPowerUp(PowerUp p)
    {
        if (p.Type == PowerUpType.Heal)
        {
            Player.Hp = Math.Min(Player.MaxHp, Player.Hp + 2);
            return; // heal instantané, non stocké
        }
        // Empêche les doublons de même type
        if (ActivePowerUps.Any(x => x.Type == p.Type)) return;
        ActivePowerUps.Add(p);
    }

    public bool HasDamageBoost => ActivePowerUps.Any(p => p.Type == PowerUpType.DamageBoost);
    public bool HasVision => ActivePowerUps.Any(p => p.Type == PowerUpType.Vision);
    public bool HasShield => ActivePowerUps.Any(p => p.Type == PowerUpType.Shield);

    public int ComputeDamage() => HasDamageBoost ? 2 : 1;

    public bool ConsumeShield()
    {
        var shield = ActivePowerUps.FirstOrDefault(p => p.Type == PowerUpType.Shield);
        if (shield == null) return false;
        ActivePowerUps.Remove(shield);
        return true;
    }
}