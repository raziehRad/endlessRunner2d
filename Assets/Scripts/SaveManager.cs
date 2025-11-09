using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string savePath = Application.persistentDataPath + "/save.json";

    public static void SaveHighScore(int score)
    {
        SaveData data = new SaveData();
        data.highScore = score;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Saved: " + json);
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
            Debug.Log("No save file found. Returning 0.");
            return 0;
        }
    }
}