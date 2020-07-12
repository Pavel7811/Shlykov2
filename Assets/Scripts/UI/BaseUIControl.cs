using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Базовый UI контрол.
/// </summary>
public class BaseUIControl : MonoBehaviour
{
    /// <summary>
    /// Нажатие кнопки Меню. Событие вознимает, когда пользователь
    /// называет кнопку Меню.
    /// </summary>
    public event Action MenuClick;

    /// <summary>
    /// Отображение помощника. Событые возникает, когда контрол
    /// вызывает отображение помощника.
    /// </summary>
    public event Action <HelperAnimation> HelperShow;
    /// <summary>
    /// Состояние UI.
    /// </summary>
    public UIState UIState;

    /// <summary>
    /// Кнопка Меню.
    /// </summary>
    private Button m_BtnMenu;
    
    /// <summary>
    /// Инициализация.
    /// </summary>
    public virtual void Init()
    {
        m_BtnMenu = transform.Find("BtnMenu")?.GetComponent<Button>();
        if (m_BtnMenu != null)
        {
            m_BtnMenu.onClick.AddListener(BtnMenu_OnClick);
        }
    }

    /// <summary>
    /// Показать.
    /// </summary>
    public virtual void Show ()
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

    /// <summary>
    /// Обработчик события Нажатия кнопки Меню.
    /// </summary>
    protected virtual void BtnMenu_OnClick()
    {
        MenuClick?.Invoke();
    }

    /// <summary>
    /// Вызов события Отображение помощника.
    /// </summary>
    /// <param name="animation">Анимация</param>
    protected void InvokeHelperShow(HelperAnimation animation)
    {
        HelperShow?.Invoke(animation);
    }


}
