using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Базовый страничный UI контрол.
/// </summary>
public class BasePagingUIControl : BaseUIControl
{
    /// <summary>
    /// Страницы.
    /// </summary>
    public List<GameObject> Pages;

    /// <summary>
    /// Текущая страница.
    /// </summary>
    protected int m_CurrentPage = 0;
    
    /// <summary>
    /// Кнопка Вперед.
    /// </summary>
    private Button m_BtnNext;
    /// <summary>
    /// Кнопка Назад.
    /// </summary>
    private Button m_BtnBack;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public override void Init()
    {
        base.Init();

        m_BtnNext = transform.Find("BtnNext").GetComponent<Button>();
        m_BtnNext.onClick.AddListener(BtnNext_OnClick);

        m_BtnBack = transform.Find("BtnBack").GetComponent<Button>();
        m_BtnBack.onClick.AddListener(BtnBack_OnClick);

        for (int i = 1; i < Pages.Count; i++)
        {
            Pages[i].gameObject.SetActive(false);
        }

        UpdateChangePageBtnsState();
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Вперед.
    /// </summary>
    private void BtnNext_OnClick()
    {
        if (m_CurrentPage < Pages.Count)
        {
            ChangePage(m_CurrentPage + 1);
        }
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Назад.
    /// </summary>
    private void BtnBack_OnClick()
    {
        if (m_CurrentPage > 0)
        {
            ChangePage(m_CurrentPage - 1);
        }
    }


    /// <summary>
    /// Обновление состояния кнопок.
    /// </summary>
    private void UpdateChangePageBtnsState()
    {
        m_BtnBack.gameObject.SetActive(m_CurrentPage != 0);
        m_BtnNext.gameObject.SetActive(m_CurrentPage != Pages.Count - 1);
    }

    /// <summary>
    /// Изменить страницу.
    /// </summary>
    /// <param name="page">Номер страницы</param>
    protected virtual void ChangePage(int page)
    {
        m_CurrentPage = page;
        Pages[m_CurrentPage].gameObject.SetActive(true);

        for (int i = 0; i < Pages.Count; i++)
        {
            if (i != m_CurrentPage)
            {
                Pages[i].gameObject.SetActive(false);
            }
        }

        UpdateChangePageBtnsState();
    }

    /// <summary>
    /// Изменить страницу на первую.
    /// </summary>
    public void SetFirstPage()
    {
        ChangePage(0);
    }
}
