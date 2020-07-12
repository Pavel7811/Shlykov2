using System;
using System.Collections.Generic;

/// <summary>
/// Все результаты пользователя.
/// </summary>
[Serializable]
public class UserResults
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName;
    
    /// <summary>
    /// Результаты.
    /// </summary>
    public List<UserResult> Results = new List<UserResult>();
}