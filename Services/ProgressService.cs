using System.Text.Json;
using CodeQuest.Models;

namespace CodeQuest.Services;

public static class ProgressService
{
    private static readonly string FilePath =
        Path.Combine(AppContext.BaseDirectory, "progress.json");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    private static GameProgress? _cached;

    public static GameProgress Load()
    {
        if (_cached != null) return _cached;
        try
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                _cached = JsonSerializer.Deserialize<GameProgress>(json, Options) ?? new GameProgress();
                if (_cached.Slots == null || _cached.Slots.Count != 3)
                    _cached.Slots = new List<SaveSlot?> { null, null, null };
                return _cached;
            }
        }
        catch { }
        _cached = new GameProgress();
        return _cached;
    }

    public static void Save()
    {
        if (_cached == null) return;
        try
        {
            var json = JsonSerializer.Serialize(_cached, Options);
            File.WriteAllText(FilePath, json);
        }
        catch { }
    }

    public static void Reset()
    {
        _cached = new GameProgress();
        Save();
    }

    public static void UnlockAll()
    {
        var p = Load();
        p.DevUnlocked = true;
        p.EasyCompleted = true;
        p.NormalCompleted = true;
        p.HardCompleted = true;
        p.ExtremeCompleted = true;
        p.Bonus1Unlocked = true;
        p.Bonus2Unlocked = true;
        Save();
    }
}