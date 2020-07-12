using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Timeline;

/// <summary>
/// Главный менеджер.
/// Предоставляет глобальный доступ.
/// </summary>
public class MainManager : MonoBehaviour
{
    public static MainManager Instance
    {
        get
        {
            return m_Instance ?? (m_Instance = GameObject.Find("_MainManager").GetComponent<MainManager>());
        }
    }

    /// <summary>
    /// Камера.
    /// </summary>
    [HideInInspector]
    public Camera MainCamera;

    /// <summary>
    /// Имя пользователя.
    /// </summary>

    public string UserName;

    /// <summary>
    /// Виртуальный помощник.
    /// </summary>

    public Helper Helper;

    /// <summary>
    /// Личность.
    /// </summary>
    [HideInInspector]
    public Person Person;

    /// <summary>
    /// Информация о личности.
    /// </summary>
    [HideInInspector]
    public PersonInfoSO PersonInfo
    {
        get
        {
            return Persons.Find(p => p.Person == Person);
        }
    }

    /// <summary>
    /// Текущее здание.
    /// </summary>
    public BuildingInfoSO CurrentBuildingInfo
    {
        get
        {
            return Buildings.Find(b => b.Building == PersonInfo.Tasks[CurrentTaskID]);
        }
    }

    /// <summary>
    /// Предыдущее здание.
    /// </summary>
    public BuildingInfoSO PrevBuildingInfo
    {
        get
        {
            if (CurrentTaskID == 0)
            {
                return null;
            }
            else
            {
                return Buildings.Find(b => b.Building == PersonInfo.Tasks[CurrentTaskID - 1]);
            }
        }
    }

    /// <summary>
    /// Номер текущего дела.
    /// </summary>
    public int CurrentTaskID { get; set; }

    /// <summary>
    /// Попытка ответа.
    /// </summary>
    public int AnswerAttempt;


    /// <summary>
    /// Список всех личностей.
    /// </summary>
    public List<PersonInfoSO> Persons;

    /// <summary>
    /// Список всех зданий.
    /// </summary>
    public List<BuildingInfoSO> Buildings;

    /// <summary>
    /// Результаты тестирования пользователей.
    /// </summary>
    public UsersResults UsersResults;

    /// <summary>
    /// Результаты пользователя с текущем именем.
    /// </summary>
    public UserResults UserResults
    {
        get
        {
            if (m_UserResults != null)
            {
                return m_UserResults;
            }
            else
            {
                UserResults userResults = UsersResults.Results.Find(r => r.UserName == UserName);
                if (userResults == null)
                {
                    userResults = new UserResults();
                    userResults.UserName = UserName;
                    UsersResults.Results.Add(userResults);
                    m_UserResults = userResults;
                }
                else
                {
                    m_UserResults = userResults;
                }
                return m_UserResults;
            }
        }
    }

    /// <summary>
    /// Анимации персонажей.
    /// </summary>
    [SerializeField]
    private List<HelperAnimationData> m_HelpersAnimations;

    
    /// <summary>
    /// Результаты пользователя с текущем именем.
    /// </summary>
    private UserResults m_UserResults;
    /// <summary>
    /// Количество найденных меток текущего знадния.
    /// </summary>
    [SerializeField]
    private int m_CountCurrentTargets;

    /// <summary>
    /// Количество всех найденных меток.
    /// </summary>
    [SerializeField]
    private int m_CountAllTargets;

    private static MainManager m_Instance;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public void Init()
    {
        MainCamera = Camera.main;

        VuforiaBehaviour.Instance.enabled = false;

        LoadPersonsAnimations();

        CheckBuildings();
        ChechPersons();

        LoadUsersResults();
    }

    /// <summary>
    /// Загрузка анимаций.
    /// </summary>
    private void LoadPersonsAnimations()
    {
        m_HelpersAnimations = new List<HelperAnimationData>();

        foreach (Helper helper in (Helper[])Enum.GetValues(typeof(Helper)))
        {
            foreach (HelperAnimation animation in (HelperAnimation[])Enum.GetValues(typeof(HelperAnimation)))
            {
                TimelineAsset timeline = Resources.Load("Timelines/" + helper.ToString() + "/" + animation.ToString()) as TimelineAsset;
                if (timeline != null)
                {
                    HelperAnimationData had = new HelperAnimationData()
                    {
                        Helper = helper,
                        Animation = animation,
                        Timeline = timeline
                    };
                    m_HelpersAnimations.Add(had);
                }
                else
                {
                    Debug.LogError("Не найдена анимация для " + helper.ToString() + " " + animation.ToString());
                }
            }
        }


    }

    /// <summary>
    /// Проверка наличия ссылок на требуемые здания.
    /// </summary>
    private void CheckBuildings()
    {
        HashSet<string> errorBuildings = new HashSet<string>();
        Persons.ForEach(person =>
        {
            person.Tasks.ForEach(task =>
            {
                if (Buildings.Find(b => b.Building == task) == null)
                {
                    errorBuildings.Add(task.ToString());
                }
            });
        });

        if (errorBuildings.Count > 0)
        {
            errorBuildings.ToList().ForEach(item => Debug.LogError("Здание присутствует в списках дел личностей, но о нём нет информации: " + item));
        }
    }

    /// <summary>
    /// Проверка личностей.
    /// </summary>
    public void ChechPersons()
    {
        HashSet<string> errorPersons = new HashSet<string>();

        Persons.ForEach(p =>
        {
            if (!UniversalCatalog.PersonRusNames.ContainsKey(p.Person))
            {
                errorPersons.Add(p.Person.ToString());
            }
        });

        if (errorPersons.Count > 0)
        {
            errorPersons.ToList().ForEach(item => Debug.LogError("Личность " + item + " отсутствует в справочнике русских названий."));
        }
    }

    /// <summary>
    /// Перезапуск приложения.
    /// </summary>
    public void Restart()
    {
        if (VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.enabled = false;
        }
        CurrentTaskID = 0;
        AnswerAttempt = 0;
        m_UserResults = null;
    }

    /// <summary>
    /// Загрузка результатов тестирования пользователей.
    /// </summary>
    private void LoadUsersResults()
    {
        UsersResults ur = SaveSystem.LoadResult();
        if (ur != null)
        {
            UsersResults = ur;
        }
        else
        {
            UsersResults = new UsersResults();
        }
    }

    /// <summary>
    /// Сохранение результатов тестирования пользователей.
    /// </summary>
    public void SaveUsersResults()
    {
        SaveSystem.SaveResult(UsersResults);
    }

    /// <summary>
    /// Закрыть приложение.
    /// </summary>
    public void QuitApp()
    {
        Application.Quit();
    }

    /// <summary>
    /// Найдена метка.
    /// </summary>
    /// <param name="building">Здание</param>
    public void TargetFound(Building building)
    {
        if (CurrentBuildingInfo.Building == building)
        {
            m_CountCurrentTargets++;
            UIManager.Instance.AR.SetState(1);
        }
        else
        {
            if (m_CountCurrentTargets == 0)
            {
                UIManager.Instance.AR.SetState(2);
            }
        }

        m_CountAllTargets++;
    }

    /// <summary>
    /// Потеряна метка.
    /// </summary>
    /// <param name="building">Здание</param>
    public void TargetLost(Building building)
    {
        if (CurrentBuildingInfo.Building == building)
        {
            if (m_CountCurrentTargets > 0)
            {
                m_CountCurrentTargets--;
            }
        }

        if (m_CountAllTargets > 0)
        {
            m_CountAllTargets--;
        }
        
        if (m_CountCurrentTargets == 0)
        {
            if (m_CountAllTargets == 0)
            {
                UIManager.Instance.AR?.SetState(0);
            }
            else
            {
                UIManager.Instance.AR?.SetState(2);
            }
        }
        
    }

    /// <summary>
    /// Анимация для текущего помощника.
    /// </summary>
    /// <param name="animation">Название анимации</param>
    /// <returns>Timeline</returns>
    public TimelineAsset GetHelperTimeline (HelperAnimation animation)
    {
        return m_HelpersAnimations.Find(a => a.Animation == animation && a.Helper == Helper).Timeline;
    }


    private void OnApplicationQuit()
    {
        SaveUsersResults();
    }
}
