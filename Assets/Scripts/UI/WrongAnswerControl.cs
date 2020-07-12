using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// UI контрол Неправильный ответ.
/// </summary>
public class WrongAnswerControl : BaseUIControl
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action Complete;

    /// <summary>
    /// Кнопка Попробовать снова.
    /// </summary>
    [SerializeField]
    private Button m_BtnAgain;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();

        m_BtnAgain.onClick.AddListener(BtnAgain_OnClick);

        UIState = UIState.WrongAnswer;
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Назад.
    /// </summary>
    private void BtnAgain_OnClick()
    {
        Complete?.Invoke();
    }

}
