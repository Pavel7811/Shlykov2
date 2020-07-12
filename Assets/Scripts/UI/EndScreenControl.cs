using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI контрол Конечный экран.
/// </summary>
public class EndScreenControl : BaseUIControl
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action <string> Complete;

    /// <summary>
    /// Нажатие кнопки Выход. Событие вознимает, когда пользователь
    /// называет кнопку Выход.
    /// </summary>
    public event Action ExitClick;

    /// <summary>
    /// Описание здания.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TxtText;

    /// <summary>
    /// Кнопка Следующий пункт маршрута.
    /// </summary>
    [SerializeField]
    private Button m_BtnExit;

    /// <summary>
    /// Кнопка Режим AR.
    /// </summary>
    [SerializeField]
    private Button m_BtnSave;

    /// <summary>
    /// Текстовое поле Отзыв.
    /// </summary>
    [SerializeField]
    private TMP_InputField m_IfReview;

    /// <summary>
    /// Компонент прокрутки.
    /// </summary>
    [SerializeField]
    private ScrollRect m_ScrollRect;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();

        m_BtnExit.onClick.AddListener(BtnExit_OnClick);
        m_BtnSave.onClick.AddListener(BtnSave_OnClick);

        UIState = UIState.EndScreen;
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Выход.
    /// </summary>
    private void BtnExit_OnClick()
    {
        ExitClick?.Invoke();
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Сохранить.
    /// </summary>
    private void BtnSave_OnClick()
    {
        Complete?.Invoke(m_IfReview.text);
    }

    /// <summary>
    /// Показать.
    /// </summary>
    /// <param name="endMessage">Сообщение</param>
    public void Show(string endMessage)
    {
        m_IfReview.text = "";
        m_TxtText.text = endMessage;
        m_ScrollRect.verticalNormalizedPosition = 1;
        base.Show();
        InvokeHelperShow(HelperAnimation.End);
    }

}
