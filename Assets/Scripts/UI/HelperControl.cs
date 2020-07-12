using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// UI контрол Помощник.
/// </summary>
public class HelperControl : MonoBehaviour
{
    /// <summary>
    /// Изображение.
    /// </summary>
    private Image m_Image;

    /// <summary>
    /// Playable Director.
    /// </summary>
    private PlayableDirector m_PlayableDirector;

    /// <summary>
    /// Метод вызываемый при завершении проигрывания.
    /// </summary>
    private Action m_ActionOnStopped;

    /// <summary>
    /// Инициализация.
    /// </summary>
    public void Init()
    {
        m_PlayableDirector = GetComponent<PlayableDirector>();
        m_Image = GetComponent<Image>();
        m_Image.enabled = false;
    }

    /// <summary>
    /// Показать.
    /// </summary>
    /// <param name="timeline">Анимация</param>
    public void Show(TimelineAsset timeline, Action actionOnStopped = null)
    {
        m_PlayableDirector.playableAsset = timeline;
        m_PlayableDirector.Play();
        m_Image.enabled = true;
        m_PlayableDirector.stopped += PlayableDirector_OnStopped;
        m_ActionOnStopped = actionOnStopped;
    }

    /// <summary>
    /// Обработчик события Остановлен компонента PlayableDirector.
    /// </summary>
    /// <param name="director">PlayableDirector</param>
    private void PlayableDirector_OnStopped(PlayableDirector director)
    {
        m_PlayableDirector.stopped -= PlayableDirector_OnStopped;
        m_Image.enabled = false;
        m_ActionOnStopped?.Invoke();
    }
}
