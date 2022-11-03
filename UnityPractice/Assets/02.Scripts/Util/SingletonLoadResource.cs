using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resource를 Load하여 싱글톤 생성
/// </summary>
public class SingletonLoadResource<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Variables
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = Resources.Load<GameObject>($"Singleton/{typeof(T).Name}");
                instance = Instantiate(go).GetComponent<T>();
            }

            return instance;
        }
    }
    #endregion Variables

    #region Property
    protected virtual bool DontDestroyLoad => false;
    #endregion Property

    #region Unity Methods
    private void Awake()
    {
        OnAwake();
    }
    #endregion Unity Methods

    #region Main Methods
    protected virtual void OnAwake()
    {
        if (DontDestroyLoad)
        {
            DontDestroyOnLoad(this);
        }
    }
    #endregion Main Methods
}
