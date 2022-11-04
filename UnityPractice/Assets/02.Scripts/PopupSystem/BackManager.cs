using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackManager : SingletonLoadResource<BackManager>
{
    #region Variables
    protected override bool DontDestroyLoad => true;

    // 팝업 리스트
    private static List<Action> popupList = new List<Action>();
    #endregion Variables

    #region Unity Methods
    private void Update()
    {
        CheckBackButton();
    }
    #endregion Unity Methods

    #region Main Methods   
    /// <summary>
    /// 팝업 등록
    /// </summary>
    public void AddAction(Action action) => popupList.Add(action);
    /// <summary>
    /// 팝업 해제
    /// </summary>
    public void RemoveAction(Action action) => popupList.Remove(action);

    /// <summary>
    /// Back 버튼이 눌렸는지 검사하는 함수
    /// </summary>
    private void CheckBackButton()
    {
        // Todo: 팝업이 1회이상 나와야 인스턴스가 생성되며 CheckBackButton 가능한 문제 수정
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopupClose();
        }
    }

    public void PopupClose()
    {
        if (popupList.Count > 0)
        {
            int index = popupList.Count - 1;

            // 백버튼으로 꺼지는 팝업인 경우 팝업 close 로직 실행
            var popup = popupList[index].Target as BasePopup;
            if (!popup.IsDontCloseBackButton) popupList[index].Invoke();
        }
    }

    public void PopupAllCloseAndDestroyPopupManager()
    {
        for (int i = popupList.Count - 1; i >= 0; i--)
        {
            popupList[i].Invoke();
        }

        GameObject go = GameObject.FindObjectOfType<PopupManager>().gameObject;
        if (go != null)
        {
            Destroy(go);
        }
    }
    #endregion Main Methods
}
