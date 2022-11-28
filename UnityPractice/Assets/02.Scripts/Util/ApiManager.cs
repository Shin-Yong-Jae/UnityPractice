using Random = UnityEngine.Random;
using System.Collections.Generic;

public enum ErrorCode
{
    Success = 0,
    Fail,
}

public static class ApiManager
{
    public static bool IsSuccess(int errorCode)
    {
        //var waitPopup = PopupManager.Instance.GetPopup<PopupWait>();
        //if (waitPopup != null)
        //{
        //    waitPopup.WaitClose();
        //}

        return errorCode == (int)ErrorCode.Success;
    }

    public static string GetErrorCode(int errorCode)
    {
        return ((ErrorCode)errorCode).ToString();
    }
}