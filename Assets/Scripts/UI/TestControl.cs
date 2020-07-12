using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// UI контрол Тестирование.
/// </summary>
public class TestControl : BaseUIControl
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action<bool> Complete;

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
    /// Кнопки ответов.
    /// </summary>
    [SerializeField]
    private List<BtnTestAnswer> m_BtnAnswers;


    /// <summary>
    /// Информация о здании.
    /// </summary>
    private BuildingInfoSO m_BuildingInfo;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();

        m_BtnBack.onClick.AddListener(BtnBack_OnClick);

        m_BtnAnswers.ForEach(btn =>
        {
            btn.Init();
            btn.Click += BtnAnswer_Click;
        });


        UIState = UIState.Test;
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Ответ.
    /// </summary>
    private void BtnAnswer_Click(int value)
    {
        Complete?.Invoke(value == m_BuildingInfo.RightAnswerID);
    }


    /// <summary>
    /// Показать.
    /// </summary>
    public void Show(BuildingInfoSO buildingInfo)
    {
        base.Show();
        m_BuildingInfo = buildingInfo;

        for (int i = 0; i < m_BtnAnswers.Count; i++)
        {
            if (i < m_BuildingInfo.Answers.Count)
            {
                m_BtnAnswers[i].Caption = m_BuildingInfo.Answers[i];
                m_BtnAnswers[i].Show();
            }
            else
            {
                m_BtnAnswers[i].Hide();
            }
        }
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Назад.
    /// </summary>
    private void BtnBack_OnClick()
    {
        BackClick?.Invoke();
    }

}
