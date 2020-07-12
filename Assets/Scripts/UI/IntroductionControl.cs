using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI контрол Вступление.
/// Приветствие, описание, выбор персонажа.
/// </summary>
public class IntroductionControl : BasePagingUIControl
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action<Person> Complete;
    
    /// <summary>
    /// Список кнопок выбора Личности.
    /// </summary>
    [SerializeField]
    private List<BtnSelectPerson> m_BtnPersons;

    /// <summary>
    /// Компонент прокрутки.
    /// </summary>
    [SerializeField]
    private ScrollRect m_ScrollRect;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public void Init(List<Person> persons)
    {
        base.Init();

        m_BtnPersons.ForEach(btn =>
        {

            btn.Init();
            if (persons.Contains(btn.Value))
            {
                btn.Interactable = true;
                btn.Click += BtnPerson_OnClick;
            }
            else
            {
                btn.Interactable = false;
            }
        

        });

        UIState = UIState.Introduction;
    }

    /// <summary>
    ///  Обработчик события Нажатия кнопки Личности.
    /// </summary>
    /// <param name="person">Личность</param>
    private void BtnPerson_OnClick(Person person)
    {
        Complete?.Invoke(person);
    }

    /// <summary>
    /// Показать.
    /// </summary>
    public override void Show()
    {
        m_ScrollRect.verticalNormalizedPosition = 1;
        base.Show();
        ChangePage(0);
    }

    /// <summary>
    /// Изменить страницу.
    /// </summary>
    /// <param name="page">Номер страницы</param>
    protected override void ChangePage(int page)
    {
        base.ChangePage(page); 
        switch (page)
        {
            case 0:
                InvokeHelperShow(HelperAnimation.IntroductionHello);
                break;
            case 1:
                InvokeHelperShow(HelperAnimation.IntroductionAboutCity);
                break;
            case 2:
                InvokeHelperShow(HelperAnimation.IntroductionChoosePerson);
                break;
        }
    }
}
