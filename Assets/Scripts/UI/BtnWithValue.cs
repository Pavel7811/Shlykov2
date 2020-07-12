using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Кнопка c параметром.
/// </summary>
public class BtnWithValue<T> : MonoBehaviour
{
    /// <summary>
    /// Нажатие кнопки.
    /// </summary>
    public event Action<T> Click;

    /// <summary>
    /// Значение.
    /// </summary>
    public T Value;

    /// <summary>
    /// Интерактивность.
    /// </summary>
    public bool Interactable
    {
        get
        {
            return m_Button == null ? false : m_Button.interactable;
        }
        set
        {
            if (m_Button != null)
            {
                m_Button.interactable = value;
            }
        }
    }

    /// <summary>
    /// Кнопка.
    /// </summary>
    private Button m_Button;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public virtual void Init()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(Button_OnClick);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки.
    /// </summary>
    private void Button_OnClick()
    {
        Click?.Invoke(Value);
    }

    /// <summary>
    /// Показать.
    /// </summary>
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Скрыть.
    /// </summary>
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}

