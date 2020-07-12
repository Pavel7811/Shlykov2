using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI контрол Правильный ответ.
/// </summary>
public class RightAnswerControl : BaseUIControl
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action Complete;

    /// <summary>
    /// Описание здания.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TxtText;

    /// <summary>
    /// Кнопка Следующий пункт маршрута.
    /// </summary>
    [SerializeField]
    private Button m_BtnNext;

    /// <summary>
    /// Кнопка Режим AR.
    /// </summary>
    [SerializeField]
    private Button m_BtnAR;

    /// <summary>
    /// Компонент прокрутки.
    /// </summary>
    [SerializeField]
    private ScrollRect m_ScrollRect;

    /// <summary>
    /// Ссылка на карту.
    /// </summary>
    private string m_URL;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();

        m_BtnNext.onClick.AddListener(BtnNext_OnClick);
        m_BtnAR.onClick.AddListener(BtnAR_OnClick);

        UIState = UIState.RightAnswer;
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Следующий пункт маршрута.
    /// </summary>
    private void BtnNext_OnClick()
    {
        Application.OpenURL(m_URL);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Режим AR.
    /// </summary>
    private void BtnAR_OnClick()
    {
        Complete?.Invoke();
    }

    /// <summary>
    /// Показать.
    /// </summary>
    /// <param name="prevBuildingMessage">Описние здания</param>
    /// <param name="url">Ссылка на следующее здание</param>
    public void Show(string buildingMessage, string url)
    {
        m_TxtText.text = buildingMessage;
        m_URL = url;
        m_ScrollRect.verticalNormalizedPosition = 1;
        base.Show();
        InvokeHelperShow(HelperAnimation.RightAnswer);
    }

}
