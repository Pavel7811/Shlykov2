using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

/// <summary>
/// Строка таблицы результатов с данными.
/// </summary>
public class RowItem : MonoBehaviour
{
    /// <summary>
    /// Количество колонок.
    /// </summary>
    public int CountColumns = 3;
    
    /// <summary>
    /// Список текстовых полей колонок.
    /// </summary>
    public List<TextMeshProUGUI> TxtCols;

    /// <summary>
    /// Фон.
    /// </summary>
    public Image ImgBg;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public void Init()
    {
        for (int i = 0; i < CountColumns; i++)
        {
            Transform col = transform.Find("Col_" + i.ToString());
            if (col != null)
            {
                TextMeshProUGUI txt = col.Find("TxtValue").GetComponent<TextMeshProUGUI>();
                if (txt != null)
                {
                    TxtCols.Add(txt);
                }
                else
                {
                    Debug.LogError("Не найден текстовый конпонент у объекта " + col.name);
                }
            }
            else
            {
                Debug.LogError("Не найден объект Col_" + i.ToString() + " у объекта " + gameObject.name);
            }
        }

        ImgBg = GetComponent<Image>();
    }

}

