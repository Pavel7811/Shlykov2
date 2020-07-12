using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI контрол Подтверждение выхода.
/// </summary>
public class QuitConfirm : MonoBehaviour
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action Complete;

    /// <summary>
    /// Отображение помощника. Событые возникает, когда контрол
    /// вызывает отображение помощника.
    /// </summary>
    public event Action<HelperAnimation> HelperShow;

    /// <summary>
    /// Кнопка Да.
    /// </summary>
    [SerializeField]
    private Button m_BtnYes;

    /// <summary>
    /// Кнопка Нет.
    /// </summary>
    [SerializeField]
    private Button m_BtnNo;

    
    /// <summary>
    /// Инициализация.
    /// </summary>
    public void Init()
    {
        m_BtnYes.onClick.AddListener(BtnYes_OnClick);
        m_BtnNo.onClick.AddListener(BtnNo_OnClick);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Да.
    /// </summary>
    private void BtnYes_OnClick()
    {
        Complete?.Invoke();
    }


    /// <summary>
    /// Обработчик события Нажатия кнопки Нет.
    /// </summary>
    private void BtnNo_OnClick()
    {
        Hide();
    }

    /// <summary>
    /// Показать.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        HelperShow?.Invoke(HelperAnimation.QuitAppConfirm);
    }

    /// <summary>
    /// Скрыть.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
