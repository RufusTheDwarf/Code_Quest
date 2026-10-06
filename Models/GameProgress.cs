namespace CodeQuest.Models;

public class GameProgress
{
    public bool EasyCompleted { get; set; }
    public bool NormalCompleted { get; set; }
    public bool HardCompleted { get; set; }
    public bool ExtremeCompleted { get; set; }
    public bool Bonus1Unlocked { get; set; }
    public bool Bonus2Unlocked { get; set; }
    public bool DevUnlocked { get; set; }
    public List<SaveSlot?> Slots { get; set; } = new() { null, null, null };

    public bool IsDifficultyUnlocked(Difficulty d) => d switch
    {
        Difficulty.Easy => true,
        Difficulty.Normal => EasyCompleted || DevUnlocked,
        Difficulty.Hard => NormalCompleted || DevUnlocked,
        Difficulty.Extreme => HardCompleted || DevUnlocked,
        _ => false
    };

    public bool AreBonusLevelsVisible => ExtremeCompleted || DevUnlocked;

    public void MarkCompleted(Difficulty d)
    {
        switch (d)
        {
            case Difficulty.Easy: EasyCompleted = true; break;
            case Difficulty.Normal: NormalCompleted = true; break;
            case Difficulty.Hard: HardCompleted = true; break;
            case Difficulty.Extreme: ExtremeCompleted = true; break;
        }
    }
}

public class SaveSlot
{
    public string Difficulty { get; set; } = "Easy";
    public int EnemyIndex { get; set; }
    public int PlayerHp { get; set; } = 5;
    public int PlayerMaxHp { get; set; } = 5;
    public int PlayerLevel { get; set; } = 1;
    public int PlayerXp { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.Now;
}