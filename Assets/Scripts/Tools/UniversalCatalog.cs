using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Универвсальный сроавочник.
/// </summary>
public static class UniversalCatalog 
{
    /// <summary>
    /// Справочние имён личностей на русском языке.
    /// </summary>
    public static Dictionary<Person, string> PersonRusNames = new Dictionary<Person, string>()
    {
        { Person.Lapshin, "Лапшин"},
        { Person.Mishin, "Мишин"},
        { Person.Shlykov, "Шлыков"}
    };

    public static List<string> MenuRusCaptions = new List<string>()
    {
        "Меню",
        "Выбор помощника",
        "Результаты"
    };
}
