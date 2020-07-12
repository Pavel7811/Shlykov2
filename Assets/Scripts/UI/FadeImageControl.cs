using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImageControl : MonoBehaviour
{
    public float FadeTime = 2f;
    private Image m_Image;
    private float m_ElapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();
        StartCoroutine(CoroutineFade());
    }

    IEnumerator CoroutineFade()
    {
        m_ElapsedTime = 0;
        
        while (m_ElapsedTime < FadeTime)
        {
            float rate = 1f - m_ElapsedTime / FadeTime;
            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, rate);

            m_ElapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        yield return null;
    }
}
