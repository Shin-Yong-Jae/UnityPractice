using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopupManager : SingletonLoadResource<PopupManager>
{
    #region Variables
    protected override bool DontDestroyLoad => true;

    [SerializeField]
    private Transform parent = null;

    // 팝업 리스트
    private Dictionary<Type, BasePopup> popups = new Dictionary<Type, BasePopup>();
    #endregion Variables

    #region Main Methods
    public T GetPopup<T>() where T : BasePopup
    {
        Type popupType = typeof(T);
        if (!popups.ContainsKey(popupType))
        {
            CreatePopup<T>(popupType.ToString());

            popups[popupType].transform.SetAsLastSibling();
            popups[popupType].gameObject.SetActive(false);
            //Debug.LogError($"{popupType.Name} is not exist.");
            //return null;
        }

        return popups[popupType] as T;
    }

    /// <summary>
    /// 팝업을 보여주는 함수
    /// </summary>
    public T ShowPopup<T>() where T : BasePopup
    {
        Type popupType = typeof(T);
        if (!popups.ContainsKey(popupType))
        {
            CreatePopup<T>(popupType.ToString());
        }

        popups[popupType].transform.SetAsLastSibling();
        popups[popupType].gameObject.SetActive(true);

        return popups[popupType] as T;
    }

    /// <summary>
    /// 팝업을 닫는 함수
    /// </summary>
    public bool ClosePopup<T>()
    {
        Type popupType = typeof(T);
        return ClosePopup(popupType);
    }
    public bool ClosePopup(Type popupType)
    {
        if (popups.ContainsKey(popupType))
        {
            popups[popupType].Close();//.gameObject.SetActive(false);
            return true;
        }

        Debug.LogWarning("PopUp Close Failed.");
        return false;
    }

    /// <summary>
    /// 팝업을 생성하는 함수
    /// </summary>
    private GameObject CreatePopup<T>(string popupName) where T : BasePopup
    {
        GameObject popupGo = Resources.Load<GameObject>($"Popup/{popupName}");
        if (popupGo == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"팝업 경로가 잘못되었거나 없습니다. popupName: {popupName}");
#endif
            return null;
        }

        T popup = Instantiate(popupGo, parent).GetComponent<T>();
        if (popup == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"{popupName} 컴포넌트가 존재하지 않습니다.");
#endif
            return null;
        }

        Type popupType = typeof(T);
        popups.Add(popupType, popup);

        return popupGo;
    }

    /// <summary>
    /// 팝업을 모두 닫는 함수
    /// </summary>
    ///
    public void CloseAllPopup()
    {
        var enumerator = popups.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentPopup = enumerator.Current.Value;

            if (currentPopup.gameObject.activeSelf)
                currentPopup.Close();
        }
    }
    #endregion Main Methods
}
