using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI контрол Список дел.
/// Описание личности, список дел.
/// </summary>
public class TasksControl : BasePagingUIControl
{
    /// <summary>
    /// Завершение. Событие возникает, когда пользователь 
    /// заканчивает взаимодействие с контролом.
    /// </summary>
    public event Action Complete;

    /// <summary>
    /// Имя личности.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TxtPersonName;

    /// <summary>
    /// Фото личности.
    /// </summary>
    [SerializeField]
    private Image m_ImgPhoto;

    /// <summary>
    /// Описание.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TxtInfo;

    /// <summary>
    /// Список дел.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TxtTasks;

    /// <summary>
    /// Кнопка В путь.
    /// </summary>
    [SerializeField]
    private Button m_BtnStart;

    /// <summary>
    /// Кнопка Режим AR.
    /// </summary>
    [SerializeField]
    private Button m_BtnAR;

    /// <summary>
    /// Компоненты прокрутки.
    /// </summary>
    [SerializeField]
    private List<ScrollRect> m_ScrollRects;
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

        m_BtnStart.onClick.AddListener(BtnStart_OnClick);
        m_BtnAR.onClick.AddListener(BtnAR_OnClick);
        UIState = UIState.Tasks;
        SetFirstPage();
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки В путь.
    /// </summary>
    private void BtnStart_OnClick()
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
    public void Show(PersonInfoSO personInfo, string url)
    {
        m_TxtPersonName.text = personInfo.Name;
        m_ImgPhoto.sprite = personInfo.Photo;
        m_TxtInfo.text = personInfo.Info;
        m_TxtTasks.text = personInfo.TasksText;
        m_URL = url;
        m_ScrollRects.ForEach(i => i.verticalNormalizedPosition = 1);
        base.Show();
        InvokeHelperShow(HelperAnimation.Person);
    }

}
