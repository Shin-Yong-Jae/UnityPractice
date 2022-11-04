using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using DG.Tweening;

public class BasePanel : MonoBehaviour
{
    #region Variables
    private CanvasGroup canvasGroup = null;

    [SerializeField]
    private float fadeDurationTime = 0.5f;
    #endregion Variables

    #region Unity Methods
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        PanelManager.RegistPanel(GetType(), this);
    }
    private void Start() => InitializePanel();
    private void Update() => UpdatePanel();
    private void OnDestroy() => DestroyPanel();
    #endregion Unity Methods

    #region Main Methods
    protected virtual void InitializePanel() { }

    protected virtual void UpdatePanel() { }
    protected virtual void DestroyPanel() => PanelManager.UnRegistPanel(GetType());

    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Close() => gameObject.SetActive(false);

    //protected void FadeIn()
    //{
    //    if (canvasGroup != null)
    //    {
    //        canvasGroup.DOFade(1, fadeDurationTime);
    //    }
    //}

    //protected void FadeOut(Action action)
    //{
    //    if (canvasGroup != null)
    //    {
    //        canvasGroup.DOFade(0, fadeDurationTime).OnComplete(() => { action?.Invoke(); });
    //    }
    //    else
    //    {
    //        action?.Invoke();
    //    }
    //}
    #endregion Main Methods
}
