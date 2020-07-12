using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using Vuforia;

/// <summary>
/// UI менеджер.
/// Предоставляет глобальный доступ к UI контролам.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get
        {
            return m_Instance ?? (m_Instance = GameObject.Find("_UIManager").GetComponent<UIManager>());
        }
    }

    /// <summary>
    /// Cтартовый экран.
    /// </summary>
    [HideInInspector]
    public StartScreenControl StartScreen;

    /// <summary>
    /// Вступление.
    /// </summary>
    [HideInInspector]
    public IntroductionControl Introduction;

    /// <summary>
    /// Список дел.
    /// </summary>
    [HideInInspector]
    public TasksControl Tasks;

    /// <summary>
    /// AR.
    /// </summary>
    [HideInInspector]
    public ARControl AR;

    /// <summary>
    /// Тестирование.
    /// </summary>
    [HideInInspector]
    public TestControl Test;

    /// <summary>
    /// Неправильный ответ.
    /// </summary>
    [HideInInspector]
    public WrongAnswerControl WrongAnswer;

    /// <summary>
    /// Правильный ответ.
    /// </summary>
    [HideInInspector]
    public RightAnswerControl RightAnswer;

    /// <summary>
    /// Конечный экран.
    /// </summary>
    [HideInInspector]
    public EndScreenControl EndScreen;

    /// <summary>
    /// Меню.
    /// </summary>
    [HideInInspector]
    public MenuControl Menu;

    /// <summary>
    /// Помощник.
    /// </summary>
    [HideInInspector]
    public HelperControl Helper;


    /// <summary>
    /// Подтверждение выхода.
    /// </summary>
    [HideInInspector]
    public QuitConfirm QuitConfirm;

    /// <summary>
    /// Текущее состояние UI.
    /// </summary>
    [SerializeField]
    private UIState m_CurrentState;

    /// <summary>
    /// Предыдущее состояние UI.
    /// </summary>
    [SerializeField]
    private UIState m_PrevState;

    /// <summary>
    /// Список всех контролов.
    /// </summary>
    private List<BaseUIControl> AllControls;
    private static UIManager m_Instance;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public void Init()
    {
        StartScreen = GameObject.Find("PnlStartScreen").GetComponent<StartScreenControl>();
        StartScreen.Init();
        StartScreen.Complete += StartScreen_OnComplete;

        Introduction = GameObject.Find("PnlIntroduction").GetComponent<IntroductionControl>();
        Introduction.Init(MainManager.Instance.Persons.Select(p => p.Person).ToList());
        Introduction.Complete += Introduction_OnComplete;
        Introduction.MenuClick += Control_OnMenuClick;
        Introduction.HelperShow += Control_OnHelperShow;

        Tasks = GameObject.Find("PnlTasks").GetComponent<TasksControl>();
        Tasks.Init();
        Tasks.Complete += Tasks_OnComplete;
        Tasks.MenuClick += Control_OnMenuClick;
        Tasks.HelperShow += Control_OnHelperShow;

        AR = GameObject.Find("PnlAR").GetComponent<ARControl>();
        AR.Init();
        AR.BackClick += AR_OnBackClick;
        AR.Complete += AR_OnComplete;
        AR.MenuClick += Control_OnMenuClick;

        Test = GameObject.Find("PnlTest").GetComponent<TestControl>();
        Test.Init();
        Test.BackClick += Test_OnBackClick;
        Test.Complete += Test_OnComplete;
        Test.MenuClick += Control_OnMenuClick;

        WrongAnswer = GameObject.Find("PnlWrongAnswer").GetComponent<WrongAnswerControl>();
        WrongAnswer.Init();
        WrongAnswer.Complete += WrongAnswer_OnComplete;
        WrongAnswer.MenuClick += Control_OnMenuClick;

        RightAnswer = GameObject.Find("PnlRightAnswer").GetComponent<RightAnswerControl>();
        RightAnswer.Init();
        RightAnswer.Complete += RightAnswer_OnComplete;
        RightAnswer.MenuClick += Control_OnMenuClick;
        RightAnswer.HelperShow += Control_OnHelperShow;

        EndScreen = GameObject.Find("PnlEndScreen").GetComponent<EndScreenControl>();
        EndScreen.Init();
        EndScreen.Complete += EndScreen_OnComplete;
        EndScreen.MenuClick += Control_OnMenuClick;
        EndScreen.HelperShow += Control_OnHelperShow;
        EndScreen.ExitClick += Control_OnExitClick;

        Menu = GameObject.Find("PnlMenu").GetComponent<MenuControl>();
        Menu.Init();
        Menu.BackClick += Menu_OnBackClick;
        Menu.Complete += Menu_OnComplete;
        Menu.HelperChange += Menu_OnHelperChange;
        Menu.ExitClick += Control_OnExitClick;

        Helper = GameObject.Find("PnlHelper").GetComponent<HelperControl>();
        Helper.Init();

        QuitConfirm = GameObject.Find("PnlQuitConfirm").GetComponent<QuitConfirm>();
        QuitConfirm.Init();
        QuitConfirm.Hide();
        QuitConfirm.HelperShow += Control_OnHelperShow;
        QuitConfirm.Complete += QuitConfirm_OnComplete;

        AllControls = new List<BaseUIControl>();
        AllControls.Add(StartScreen);
        AllControls.Add(Introduction);
        AllControls.Add(Tasks);
        AllControls.Add(AR);
        AllControls.Add(Test);
        AllControls.Add(WrongAnswer);
        AllControls.Add(RightAnswer);
        AllControls.Add(EndScreen);
        AllControls.Add(Menu);

        SetState(UIState.StartScreen);
    }

   
    private void SetState(UIState state)
    {
        if (state == UIState.Menu)
        {
            m_PrevState = m_CurrentState;
        }

        m_CurrentState = state;

        if (m_CurrentState != UIState.Menu)
        {
            AllControls.Where(c => c.UIState != m_CurrentState).ToList().ForEach(c => c.Hide());
        }
        switch (m_CurrentState)
        {
            case UIState.StartScreen:
                StartScreen.Show();
                break;
            case UIState.Introduction:
                Introduction.Show();
                break;
            case UIState.Tasks:
                Tasks.Show(MainManager.Instance.PersonInfo, MainManager.Instance.CurrentBuildingInfo.URL);
                break;
            case UIState.AR:
                AR.Show();
                VuforiaBehaviour.Instance.enabled = true;
                break;
            case UIState.Test:
                Test.Show(MainManager.Instance.CurrentBuildingInfo);
                break;
            case UIState.RightAnswer:
                RightAnswer.Show(MainManager.Instance.PrevBuildingInfo.RightAnswerMessage, MainManager.Instance.CurrentBuildingInfo.URL);
                break;
            case UIState.WrongAnswer:
                WrongAnswer.Show();
                break;
            case UIState.EndScreen:
                EndScreen.Show(MainManager.Instance.PersonInfo.EndMessage);
                break;
            case UIState.Menu:
                Menu.Show(MainManager.Instance.Helper);
                break;

        }
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Меню контрола.
    /// </summary>
    private void Control_OnMenuClick()
    {
        SetState(UIState.Menu);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки Выход контрола.
    /// </summary>
    public void Control_OnExitClick()
    {
        QuitConfirm.Show();
    }

    /// <summary>
    /// Обработчик события Завершения контрола Cтартовый экран.
    /// </summary>
    /// <param name="helper">Помощник</param>
    /// <param name="userName">Имя пользователя</param>
    private void StartScreen_OnComplete(Helper helper, string userName)
    {
        MainManager.Instance.Helper = helper;
        MainManager.Instance.UserName = userName;
        SetState(UIState.Introduction);
    }
    /// <summary>
    /// Обработчик события Завершения контрола Вступление.
    /// </summary>
    /// <param name="person"></param>
    private void Introduction_OnComplete(Person person)
    {
        MainManager.Instance.Person = person;
        MainManager.Instance.CurrentTaskID = 0;
        MainManager.Instance.AnswerAttempt = 0;

        UserResult ur = new UserResult()
        {
            Date = DateTime.Now.ToString("dd.MM.yyyy HH:mm"),
            Person = UniversalCatalog.PersonRusNames[MainManager.Instance.Person],
            CountQuestions = MainManager.Instance.PersonInfo.Tasks.Count
        };

        MainManager.Instance.UserResults.Results.Add(ur);

        SetState(UIState.Tasks);
    }



    /// <summary>
    /// Обработчик события Завершения контрола Список дел.
    /// </summary>
    private void Tasks_OnComplete()
    {
        SetState(UIState.AR);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки назад контрола AR.
    /// </summary>
    private void AR_OnBackClick()
    {
        if (MainManager.Instance.CurrentTaskID == 0)
        {
            SetState(UIState.Tasks);
        }
        else
        {
            SetState(UIState.RightAnswer);
        }
    }

    /// <summary>
    /// Обработчик события Завершения контрола AR.
    /// </summary>
    private void AR_OnComplete()
    {
        VuforiaBehaviour.Instance.enabled = false;
        SetState(UIState.Test);
    }


    /// <summary>
    /// Обработчик события Нажатия кнопки назад контрола Тестирование.
    /// </summary>
    private void Test_OnBackClick()
    {
        SetState(UIState.AR);
    }

    /// <summary>
    /// Обработчик события Завершения контрола Тестирование.
    /// </summary>
    private void Test_OnComplete(bool result)
    {
        if (result)
        {
            AnswerData ad = new AnswerData()
            {
                ID = MainManager.Instance.CurrentTaskID,
                CountAttemps = MainManager.Instance.AnswerAttempt
            };

            MainManager.Instance.UserResults.Results.Last().Answers.Add(ad);


            if (MainManager.Instance.CurrentTaskID < MainManager.Instance.PersonInfo.Tasks.Count - 1) // в списке ещё есть задания
            {

                MainManager.Instance.CurrentTaskID++;
                MainManager.Instance.AnswerAttempt = 0;

                SetState(UIState.RightAnswer);
            }
            else // в списке больше нет заданий
            {
                SetState(UIState.EndScreen);

                EvaluatingSystem.CalculateResult(MainManager.Instance.UserResults.Results.Last());
            }
        }
        else
        {
            SetState(UIState.WrongAnswer);
            MainManager.Instance.AnswerAttempt++;
            if (MainManager.Instance.AnswerAttempt == 1)
            {
                Control_OnHelperShow(HelperAnimation.WrongAnswerAttemp_1);
            }
            else if (MainManager.Instance.AnswerAttempt == 2)
            {
                Control_OnHelperShow(HelperAnimation.WrongAnswerAttemp_2);
            }
            else if (MainManager.Instance.AnswerAttempt > 2)
            {
                Control_OnHelperShow(HelperAnimation.WrongAnswerAttemp_3);
            }
        }
    }

    /// <summary>
    /// Обработчик события Завершения контрола Неправильный ответ.
    /// </summary>
    private void WrongAnswer_OnComplete()
    {
        SetState(UIState.Test);
    }

    /// <summary>
    /// Обработчик события Завершения контрола Правильный ответ.
    /// </summary>
    private void RightAnswer_OnComplete()
    {
        SetState(UIState.AR);
    }

    /// <summary>
    /// Обработчик события Завершения контрола Конечный экран.
    /// </summary>
    private void EndScreen_OnComplete(string review)
    {
        SaveSystem.SaveReview(review, MainManager.Instance.UserName);
        Restart();
        SetState(UIState.Menu);
        Menu.Show(MainManager.Instance.Helper,2);
    }

    /// <summary>
    /// Обработчик события Нажатия кнопки назад контрола Меню.
    /// </summary>
    private void Menu_OnBackClick()
    {
        m_CurrentState = m_PrevState;
        m_PrevState = UIState.Menu;
        Menu.Hide();
    }

    /// <summary>
    /// Обработчик события Завершения контрола Меню.
    /// </summary>
    private void Menu_OnComplete()
    {
        Restart();
    }

    /// <summary>
    /// Обработник события Изменение помощника контрола Меню.
    /// </summary>
    /// <param name="helper">Helper</param>
    private void Menu_OnHelperChange(Helper helper)
    {
        MainManager.Instance.Helper = helper;
    }

    /// <summary>
    /// Обработник события Отображения помощника контрола.
    /// </summary>
    /// <param name="animation">Анимация</param>
    private void Control_OnHelperShow(HelperAnimation animation)
    {
        Helper.Show(MainManager.Instance.GetHelperTimeline(animation));
    }

    /// <summary>
    /// Обработчик события Завершения контрола Подтверждение выхода.
    /// </summary>
    private void QuitConfirm_OnComplete()
    {
        Helper.Show(MainManager.Instance.GetHelperTimeline(HelperAnimation.QuitApp), MainManager.Instance.QuitApp);
    }

    /// <summary>
    /// Перезапуск приложения.
    /// </summary>
    private void Restart()
    {
        MainManager.Instance.Restart();
        SetState(UIState.StartScreen);
    }

}
