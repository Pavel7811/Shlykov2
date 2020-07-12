using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// UI контрол AR.
/// </summary>
public class ARControl : BaseUIControl
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action Complete;

    /// <summary>
    /// Нажатие кнопки Назад. Событие вознимает, когда пользователь
    /// называет кнопку Назад.
    /// </summary>
    public event Action BackClick;

    /// <summary>
    /// Кнопка Назад.
    /// </summary>
    [SerializeField]
    private Button m_BtnBack;

    /// <summary>
    /// Кнопка Тестирование.
    /// </summary>
    [SerializeField]
    private Button m_BtnTest;

    /// <summary>
    /// Сообщение Другое здание.
    /// </summary>
    [SerializeField]
    private GameObject m_TxtError;

    /// <summary>
    /// Текущее состояние контрола:
    /// 0 - пусто;
    /// 1 - кнопка тестирование.
    /// 2 - сообщение Другое здание.
    /// </summary>
    private int m_State = 0;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();

        m_BtnBack.onClick.AddListener(BtnBack_OnClick);
        m_BtnTest.onClick.AddListener(BtnTest_OnClick);

        UIState = UIState.AR;
    }

    /// <summary>
    /// Установить состояние.
    /// </summary>
    /// <param name="state">Состояние</param>
    public void SetState(int state)
    {
        m_State = state;
        bool btnTest = false;
        bool txtError = false;
        switch (m_State)
        {
            case 1:
                btnTest = true;
                break;
            case 2:
                txtError = true;
                break;
        }
        m_BtnTest.gameObject.SetActive(btnTest);
        m_TxtError.gameObject.SetActive(txtError);
    }

    /// <summary>
    /// Показать.
    /// </summary>
    public override void Show()
    {
        SetState(0);
        base.Show();
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Назад.
    /// </summary>
    private void BtnBack_OnClick()
    {
        BackClick?.Invoke();
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Тестирование.
    /// </summary>
    private void BtnTest_OnClick()
    {
        Complete?.Invoke();
    }
}
