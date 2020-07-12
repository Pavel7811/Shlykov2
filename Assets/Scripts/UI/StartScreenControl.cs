using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// UI контрол Cтартовый экран.
/// Выбор персонажа, ввод имени.
/// </summary>
public class StartScreenControl : BaseUIControl
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action<Helper, string> Complete;

    /// <summary>
    /// Тип виртуального помощника.
    /// </summary>
    private Helper m_Helper;
    
    /// <summary>
    /// Кнопка Девочка.
    /// </summary>
    [SerializeField]
    private Button m_BtnGirl;
    
    /// <summary>
    /// Кнопка Мальчик.
    /// </summary>
    [SerializeField]
    private Button m_BtnBoy;
    
    /// <summary>
    /// Кнопка Вперед.
    /// </summary>
    [SerializeField]
    private Button m_BtnNext;

    /// <summary>
    /// Заголовок.
    /// </summary>
    [SerializeField]
    private GameObject m_TxtCaption;

    /// <summary>
    /// Текстовое поле Имя пользователя.
    /// </summary>
    [SerializeField]
    private TMP_InputField m_IfUserName;

    /// <summary>
    /// Текущее состояние контрола:
    /// 0 - выбор помощника;
    /// 1 - ввод имени пользователя.
    /// </summary>
    private int m_State = 0;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();
        
        m_BtnGirl.onClick.AddListener(BtnGirl_OnClick);
        m_BtnBoy.onClick.AddListener(BtnBoy_OnClick);
        m_BtnNext.onClick.AddListener(BtnNext_OnClick);
        m_IfUserName.onValueChanged.AddListener(IfUserName_OnValueChanged);
        m_IfUserName.text = "";
        m_BtnNext.interactable = false;

        UIState = UIState.StartScreen;
    }

    /// <summary>
    /// Установить состояние.
    /// </summary>
    /// <param name="state">Состояние</param>
    private void SetState(int state)
    {
        m_State = state;
        bool btnFemale = false;
        bool btnMale = false;
        bool btnNext = false;
        bool txtCaption = false;
        bool ifUserName = false;


        switch (m_State)
        {
            case 0:
                btnFemale = true;
                btnMale = true;
                txtCaption = true;
                break;
            case 1:
                btnNext = true;
                ifUserName = true;
                break;
        }

        m_BtnGirl.gameObject.SetActive(btnFemale);
        m_BtnBoy.gameObject.SetActive(btnMale);
        m_BtnNext.gameObject.SetActive(btnNext);
        m_TxtCaption.SetActive(txtCaption);
        m_IfUserName.gameObject.SetActive(ifUserName);
    }

    /// <summary>
    /// Показать.
    /// </summary>
    public override void Show()
    {
        m_IfUserName.text = "";
        SetState(0);
        base.Show();
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Девочка.
    /// </summary>
    private void BtnGirl_OnClick()
    {
        m_Helper = Helper.Girl;
        SetState(1);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Мальчик.
    /// </summary>
    private void BtnBoy_OnClick()
    {
        m_Helper = Helper.Boy;
        SetState(1);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Вперед.
    /// </summary>
    private void BtnNext_OnClick()
    {
        Complete?.Invoke(m_Helper, m_IfUserName.text);
        Hide();
    }

    /// <summary>
    /// Обработчик события Изменения значения поля Имя пользователя.
    /// </summary>
    /// <param name="value">Значение</param>
    private void IfUserName_OnValueChanged(string value)
    {
        m_BtnNext.interactable = value.Length > 0;
    }
}
