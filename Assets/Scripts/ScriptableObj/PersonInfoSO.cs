using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Информация о личности.
/// </summary>
[CreateAssetMenu(fileName = "PersonInfo", menuName = "ScriptableObjects/Person Info", order = 0)]
public class PersonInfoSO : ScriptableObject
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name;

    /// <summary>
    /// Личность.
    /// </summary>
    public Person Person;
    
    /// <summary>
    /// Фото.
    /// </summary>
    public Sprite Photo;

    /// <summary>
    /// Описание.
    /// </summary>
    [TextArea(15,30)]
    public string Info;

    /// <summary>
    /// Список дел.
    /// </summary>
    public List<Building> Tasks;

    /// <summary>
    /// Описание списка дел.
    /// </summary>
    [TextArea(15, 30)]
    public string TasksText;

    /// <summary>
    /// Сообщение конечного экрана.
    /// </summary>
    [TextArea(15,30)]
    public string EndMessage;
}
