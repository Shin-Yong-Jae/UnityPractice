using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DialogColorType
{
    Normal,
    Positive,
    Nagative,
}

public class PopupDialogTwoButton : BasePopup
{
    #region Variables
    [SerializeField]
    private TMP_Text titleText, descriptionText = null;

    [SerializeField]
    private Button cancelButton, confirmButton = null;

    [SerializeField]
    private TMP_Text cancelBtnText, confirmBtnText = null;

    [SerializeField]
    private Sprite normalBtnSprite, positiveBtnSprite, nagativeBtnSprite = null;

    private Action cancelAction, confirmAction = null;

    public override bool IsDontCloseBackButton => true;
    #endregion Variables

    #region Main Methods
    protected override void InitializePopup()
    {
        base.InitializePopup();

        cancelButton.onClick.AddListener(() =>
        {
            cancelAction?.Invoke();
        });
        confirmButton.onClick.AddListener(() =>
        {
            confirmAction?.Invoke();
        });
    }

    public void ShowMessage(string title, string description, string cancelStr, string confirmStr
        , DialogColorType descriptionColorType, DialogColorType buttonColorType, Action cancelAction, Action confirmAction)
    {
        cancelButton.gameObject.SetActive(cancelAction != null);
        confirmButton.gameObject.SetActive(confirmAction != null);

        this.cancelAction = cancelAction;
        this.confirmAction = confirmAction;

        titleText.text = title;
        descriptionText.text = description;

        cancelBtnText.text = cancelStr;
        confirmBtnText.text = confirmStr;

        switch (descriptionColorType)
        {
            case DialogColorType.Normal:
                descriptionText.color = Util.TryParseHtmlString(Constant.NormalDialogColorName);
                break;
            case DialogColorType.Positive:
                descriptionText.color = Util.TryParseHtmlString(Constant.PositiveDialogColorName);
                break;
            case DialogColorType.Nagative:
                descriptionText.color = Util.TryParseHtmlString(Constant.NagativeDialogColorName);
                break;
        }

        switch (buttonColorType)
        {
            case DialogColorType.Normal:
                confirmButton.GetComponent<Image>().sprite = normalBtnSprite;
                break;
            case DialogColorType.Positive:
                confirmButton.GetComponent<Image>().sprite = positiveBtnSprite;
                break;
            case DialogColorType.Nagative:
                confirmButton.GetComponent<Image>().sprite = nagativeBtnSprite;
                break;
        }
    }

    public override void Close()
    {
        confirmAction = null;
        cancelAction = null;

        base.Close();
    }

    #endregion Main Methods
}
