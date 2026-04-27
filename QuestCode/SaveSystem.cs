using System;
using System.IO;
using System.Text.Json;

public static class SaveSystem
{
    private static string filePath = "savegame.json";

    // This fixes the 'Save' definition error
    public static void Save(GameState state)
    {
        try
        {
            string json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            Console.WriteLine("Game saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save Error: {ex.Message}");
        }
    }

    // This fixes the 'Load' definition error
    public static GameState? Load()
    {
        if (!File.Exists(filePath)) return null;
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<GameState>(json);
        }
        catch
        {
            return null;
        }
    }
}