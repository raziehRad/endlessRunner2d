using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string savePath = Application.persistentDataPath + "/save.json";

    public static void SaveHighScore(int score)
    {
        SaveData data = new SaveData();
        data.highScore = score;
        Save(data);
    }

    private static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }
    public static int LoadHighScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data.highScore;
        }
        else
        {
            return 0;
        }
    }

    public static void SaveCharacter(int currentIndex)
    {
        SaveData data = new SaveData();
        data.characterIndex = currentIndex;

        Save(data);
    }
    public static int LoadCharacter()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data.characterIndex;
        }
        else
        {
            return 0;
        }
    }
}