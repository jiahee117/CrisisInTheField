using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                }
                instance.Init();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this as T;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("a");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Init()
    {

    }
}
