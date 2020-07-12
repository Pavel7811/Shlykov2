using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Сохранение и загрузка.
/// </summary>
public static class SaveSystem 
{
    /// <summary>
    /// Имя файла Результатов.
    /// </summary>
    private const string FileNameResults = "UsersResults.txt";

    /// <summary>
    /// Имя файла Отзыва.
    /// </summary>
    private const string FileNameReview = "Review";

    /// <summary>
    /// Сохранение результатов.
    /// </summary>
    /// <param name="usersResults">Данные</param>
    /// <returns>Результат</returns>
    public static bool SaveResult (UsersResults usersResults)
    {
        bool result = true;
        string json = JsonUtility.ToJson(usersResults);
        
        
        string path = Path.Combine(Application.persistentDataPath,FileNameResults);

        try
        {
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            result = false;
        }

        return result;
    }

    /// <summary>
    /// Загрузка результатов.
    /// </summary>
    /// <returns>Данные</returns>
    public static UsersResults LoadResult()
    {
        string path = Path.Combine(Application.persistentDataPath, FileNameResults);

        if (File.Exists(path))
        {
            UsersResults data = new UsersResults();
            try
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, data);
                return data;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return null;
            }
        }
        else
        {
            Debug.Log("Not found");
            return null;
        }
    }

    /// <summary>
    /// Сохранение отзыва.
    /// </summary>
    /// <param name="review">Отзыв</param>
    /// <param name="user">Пользователь</param>
    /// <returns></returns>
    public static bool SaveReview(string review, string user)
    {
        bool result = true;
        try
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Application.persistentDataPath, FileNameReview + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt")))
            {
                outputFile.WriteLine(user);
                outputFile.WriteLine(review);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            result = false;
        }
        return result;
    }
}
