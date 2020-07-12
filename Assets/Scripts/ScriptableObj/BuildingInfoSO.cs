using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Информация о здании.
/// </summary>
[CreateAssetMenu(fileName = "BuildingInfo", menuName = "ScriptableObjects/Building Info", order = 1)]
public class BuildingInfoSO : ScriptableObject
{
    /// <summary>
    /// Здание.
    /// </summary>
    public Building Building;

    /// <summary>
    /// Ответы тестирования.
    /// </summary>
    public List<string> Answers;
    
    /// <summary>
    /// ID правильного ответа.
    /// </summary>
    public int RightAnswerID;

    /// <summary>
    /// Сообщение.
    /// </summary>
    [TextArea(15, 30)]
    public string RightAnswerMessage;
    
    /// <summary>
    /// Ссылка на карту.
    /// </summary>
    [TextArea(15, 30)]
    public string URL;
}
