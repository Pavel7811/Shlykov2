using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    void Start()
    {
        MainManager.Instance.Init();
        UIManager.Instance.Init();
    }
}
