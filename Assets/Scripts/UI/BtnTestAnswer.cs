using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Кнопка ответа в тестировании.
/// </summary>
public class BtnTestAnswer : BtnWithValue<int>
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Caption
    {
        get
        {
            
            return m_TxtCaption == null ? "" : m_TxtCaption.text;
        }
        set
        {
            if (m_TxtCaption != null)
            {
                m_TxtCaption.text = value;
            }
        }
    }

    /// <summary>
    /// Компонент Название.
    /// </summary>
    private TextMeshProUGUI m_TxtCaption;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();
        m_TxtCaption = transform.Find("TxtCaption").GetComponent<TextMeshProUGUI>();
    }
}

