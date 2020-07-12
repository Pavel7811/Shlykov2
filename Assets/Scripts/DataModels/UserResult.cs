using System;
using System.Collections.Generic;

/// <summary>
/// Результат пользователя за один тест.
/// </summary>
[Serializable]
public class UserResult
{
    /// <summary>
    /// Дата.
    /// </summary>
    public string Date;

    /// <summary>
    /// Личность.
    /// </summary>
    public string Person;

    /// <summary>
    /// Результат в процентах.
    /// </summary>
    public string ResultValue = "-";

    /// <summary>
    /// Количество вопросов.
    /// </summary>
    public int CountQuestions;

    /// <summary>
    /// Ответы.
    /// </summary>
    public List<AnswerData> Answers = new List<AnswerData>();

}