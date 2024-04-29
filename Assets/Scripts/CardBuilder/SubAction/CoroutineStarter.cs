using System;
using UnityEngine;

public class CoroutineStarter : MonoBehaviour
{
    private static CoroutineStarter instance;

    public static CoroutineStarter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CoroutineStarter").AddComponent<CoroutineStarter>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
}