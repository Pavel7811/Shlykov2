
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTableControl : MonoBehaviour
{
    /// <summary>
    /// Префаб строки с именем пользователя.
    /// </summary>
    public GameObject RowUserNamePrefab;

    /// <summary>
    /// Префаб строки с результатов.
    /// </summary>
    public GameObject RowResultPrefab;


    /// <summary>
    /// Контейнер для строк таблицы.
    /// </summary>
    [SerializeField]
    private Transform m_PnlVerticalGroup;

    /// <summary>
    /// Список строк.
    /// </summary>
    private List<RowItem> m_Rows;


    /// <summary>
    /// Инициализация.
    /// </summary>
    public void Init()
    {
        m_PnlVerticalGroup = transform.Find("Viewport").Find("Content").Find("PnlVerticalGroup");
        if (m_PnlVerticalGroup == null)
        {
            Debug.LogError("Не найден контейнер таблицы результатов");
        }
    }

    /// <summary>
    /// Показать.
    /// </summary>
    public void Show(UsersResults usersResults, string topUser)
    {
        if (m_Rows == null)
        {
            m_Rows = new List<RowItem>();
        }
        else
        {
            m_Rows.ForEach(item => Destroy(item.gameObject));
            m_Rows.Clear();
        }

        gameObject.SetActive(true);

        UserResults ur = usersResults.Results.Find(r => r.UserName == topUser);
        if (ur != null)
        {
            CreateRows(ur);
        }

        usersResults.Results.FindAll(r => r.UserName != topUser).ForEach(userResults =>
        {
            CreateRows(userResults);
        });
    }

    /// <summary>
    /// Скрыть.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }


    private void CreateRows(UserResults userResults)
    {
        RowItem rowUserName = Instantiate(RowUserNamePrefab.gameObject).GetComponent<RowItem>();
        rowUserName.Init();
        rowUserName.TxtCols[0].text = userResults.UserName;

        m_Rows.Add(rowUserName);
        rowUserName.transform.SetParent(m_PnlVerticalGroup);
        rowUserName.transform.localScale = Vector3.one;

        int i = 0;
        userResults.Results.ForEach(result =>
        {
            RowItem rowResult = Instantiate(RowResultPrefab.gameObject).GetComponent<RowItem>();
            rowResult.Init();
            rowResult.TxtCols[0].text = result.Date;
            rowResult.TxtCols[1].text = result.Person;
            rowResult.TxtCols[2].text = result.ResultValue;
            rowResult.ImgBg.enabled = (i % 2) == 0;
            m_Rows.Add(rowResult);
            rowResult.transform.SetParent(m_PnlVerticalGroup);
            rowResult.transform.localScale = Vector3.one;
            i++;
        });
    }
}
