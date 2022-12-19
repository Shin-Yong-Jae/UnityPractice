using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToastCanvas : BasePanel
{
    #region Variables
    [SerializeField]
    private GameObject toastMessage = null;

    [SerializeField]
    private Text messageText = null;
    private UIModeManager uiModeManager = null;
    #endregion Variables

    #region Main Methods
    protected override void InitializePanel()
    {
        base.InitializePanel();

        uiModeManager = GetComponent<UIModeManager>();
        toastMessage.SetActive(false);
    }

    private void PlayTween()
    {
        toastMessage.SetActive(false);

        toastMessage.SetActive(true);
        DOTween.Restart(toastMessage);
    }

    public void ToastPositive(string msg)
    {
        PlayTween();

        uiModeManager?.ProcessChangeMode(UIModeManager.UIModeCategory.Positive);
        messageText.text = msg;
    }

    public void ToastNagative(string msg)
    {
        PlayTween();

        uiModeManager?.ProcessChangeMode(UIModeManager.UIModeCategory.Nagative);
        messageText.text = msg;
    }
    #endregion Main Methods
}
