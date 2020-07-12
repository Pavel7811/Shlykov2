using System;

/// <summary>
/// Ответ пользователя на один вопрос теста.
/// </summary>
[Serializable]
public class AnswerData
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int ID;

    /// <summary>
    /// Количество попыток.
    /// </summary>
    public int CountAttemps;
}