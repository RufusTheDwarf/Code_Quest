namespace CodeQuest.Models;

public class Player
{
    public int MaxHp { get; set; } = 5;
    public int Hp { get; set; } = 5;
    public int Level { get; set; } = 1;
    public int Xp { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount { get; set; }

    public bool IsAlive => Hp > 0;

    public void Reset()
    {
        MaxHp = 5;
        Hp = 5;
        Level = 1;
        Xp = 0;
        CorrectCount = 0;
        WrongCount = 0;
    }

    public int XpForNextLevel => 40;
    public int XpInCurrentLevel => Xp % XpForNextLevel;
    public double XpProgress => (double)XpInCurrentLevel / XpForNextLevel;

    public bool AddXp(int amount)
    {
        Xp += amount;
        int newLevel = 1 + Xp / XpForNextLevel;
        if (newLevel > Level)
        {
            int oldMaxHp = MaxHp;
            Level = newLevel;
            MaxHp += 1;
            Hp = Math.Min(Hp + 1, MaxHp);
            return true;
        }
        return false;
    }
}