using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI контрол Меню.
/// </summary>
public class MenuControl : BaseUIControl
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
    /// Нажатие кнопки Выход. Событие вознимает, когда пользователь
    /// называет кнопку Выход.
    /// </summary>
    public event Action ExitClick;

    /// <summary>
    /// Изменение помощника. Событие вознимает, когда пользователь
    /// называет выбирает другого Помощника.
    /// </summary>
    public event Action <Helper> HelperChange;


    /// <summary>
    /// Кнопка Заново.
    /// </summary>
    [SerializeField]
    private Button m_BtnRestart;

    /// <summary>
    /// Кнопка Настройки.
    /// </summary>
    [SerializeField]
    private Button m_BtnSettings;

    /// <summary>
    /// Кнопка Результаты.
    /// </summary>
    [SerializeField]
    private Button m_BtnResults;

    /// <summary>
    /// Кнопка Выход.
    /// </summary>
    [SerializeField]
    private Button m_BtnExit;

    /// <summary>
    /// Кнопка Назад.
    /// </summary>
    [SerializeField]
    private Button m_BtnBack;

    /// <summary>
    /// Заголовок Меню.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TxtCaption;

    /// <summary>
    /// Панель с кнопками.
    /// </summary>
    [SerializeField]
    private GameObject m_PnlBtns;

    /// <summary>
    /// Панель с настройками.
    /// </summary>
    [SerializeField]
    private ToggleGroupEx m_PnlSettings;

    /// <summary>
    /// Панель с результатами.
    /// </summary>
    [SerializeField]
    private ResultTableControl m_PnlResults;

    /// <summary>
    /// Текущее состояние контрола:
    /// 0 - меню;
    /// 1 - настройки;
    /// 2 - результаты.
    /// </summary>
    private int m_State = 0;

    /// <summary>
    /// Помощник.
    /// </summary>
    private Helper m_Helper;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();

        m_BtnRestart.onClick.AddListener(BtnRestart_OnClick);
        m_BtnSettings.onClick.AddListener(BtnSettings_OnClick);
        m_BtnResults.onClick.AddListener(BtnResults_OnClick);
        m_BtnExit.onClick.AddListener(BtnExit_OnClick);
        m_BtnBack.onClick.AddListener(BtnBack_OnClick);

        m_PnlResults.Init();

        UIState = UIState.Menu;
    }

  

    /// <summary>
    /// Установить состояние.
    /// </summary>
    /// <param name="state">Состояние</param>
    private void SetState(int state)
    {
        m_State = state;
        bool pnlBtns = false;
        bool pnlSettings = false;
        bool pnlResults = false;
        bool btnExit = false;


        switch (m_State)
        {
            case 0:
                pnlBtns = true;
                btnExit = true;
                break;
            case 1:
                pnlSettings = true;
                InvokeHelperShow(HelperAnimation.Results);
                break;
            case 2:
                pnlResults = true;
                break;
        }

        m_PnlBtns.gameObject.SetActive(pnlBtns);
        m_PnlSettings.gameObject.SetActive(pnlSettings);

        if (pnlResults)
        {
            m_PnlResults.Show(MainManager.Instance.UsersResults,MainManager.Instance.UserName);
        }
        else
        {
            m_PnlResults.Hide();
        }

        m_BtnExit.gameObject.SetActive(btnExit);
        m_TxtCaption.text = UniversalCatalog.MenuRusCaptions[m_State];
        if (m_State == 1)
        {
            m_PnlSettings.Toggles.Find(t => t.ID == (int)m_Helper).isOn = true;
        }
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Заново.
    /// </summary>
    private void BtnRestart_OnClick()
    {
        Complete?.Invoke();
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Настройки.
    /// </summary>
    private void BtnSettings_OnClick()
    {
        SetState(1);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Результаты.
    /// </summary>
    private void BtnResults_OnClick()
    {
        SetState(2);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Выход.
    /// </summary>
    private void BtnExit_OnClick()
    {
        ExitClick?.Invoke();
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Назад.
    /// </summary>
    private void BtnBack_OnClick()
    {
        switch (m_State)
        {
            case 0:
                BackClick?.Invoke();
                break;
            case 1:
                HelperChange?.Invoke((Helper)m_PnlSettings.ActiveToggles().FirstOrDefault().ID);
                SetState(0);
                break;
            case 2:
                SetState(0);
                break;
        }
    }

    /// <summary>
    /// Показать
    /// </summary>
    /// <param name="helper">Помощник</param>
    /// <param name="state">0-меню, 1-настройки, 2-результаты</param>
    public void Show(Helper helper,int state = 0)
    {
        m_Helper = helper;
        SetState(state);
        base.Show();
    }

}
