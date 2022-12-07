using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public static class Util
{
    public static double TotalSeconds(this DateTime dateTime) => (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds * 0.001;
    private static HashSet<string> bannedWordList = new HashSet<string>();
    //범위 반환.
    public static int Clamp(int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    /// <summary> 버튼 컬러 (Interactable)  </summary>
    public static void SetButtonColor(GameObject _object, bool _interactable)
    {
        Color color;
        float factor = _interactable ? 1f : 0.5f;
        var texts = _object.GetComponentsInChildren<Text>();
        var images = _object.GetComponentsInChildren<Image>();

        foreach (var text in texts)
        {
            color = text.color; // Color.white;
            color.a = text.color.a;
            color.r *= factor;
            color.g *= factor;
            color.b *= factor;
            text.color = color;
        }

        foreach (var image in images)
        {
            color = image.color; // Color.white;
            color.a = image.color.a;
            color.r *= factor;
            color.g *= factor;
            color.b *= factor;
            image.color = color;
        }
    }

    public static Vector2 TransWorldPositionToCanvasPostion(Vector2 _worldPostion, Vector2 _canvasSizeDelta)
    {
        Vector2 newPos = Camera.main.WorldToViewportPoint(_worldPostion);
        newPos.x *= _canvasSizeDelta.x;
        newPos.y *= _canvasSizeDelta.y;

        return newPos;
    }

    public static Vector2 TransWorldPositionToCanvasPostion(Vector2 _worldPostion, Vector2 _canvasSizeDelta, Vector2 _anchor)
    {
        Vector2 newPos = Camera.main.WorldToViewportPoint(_worldPostion);
        newPos.x *= _canvasSizeDelta.x;
        newPos.y *= _canvasSizeDelta.y;

        newPos.x -= _canvasSizeDelta.x * _anchor.x;
        newPos.y -= _canvasSizeDelta.y * _anchor.y;

        return newPos;
    }

    public static Vector3 GetBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;

        Vector3 result =
            (u2) * p0 +
            (2f * u * t) * p1 +
            (t2) * p2;
        return result;
    }
    public static Vector3 GetBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector3 result =
            (u3) * p0 +
            (3f * u2 * t) * p1 +
            (3f * u * t2) * p2 +
            (t3) * p3;
        return result;
    }

    public static void RegistBannedWord(string banWord)
    {
        if (bannedWordList.Contains(banWord))
            bannedWordList.Add(banWord);
    }

    public static bool IsOkText(string text)
    {
        var wordArray = text.Split(' ', ',', '.');
        for (int i = 0; i < wordArray.Length; i++)
        {
            string word = wordArray[i];
            for (int length = 1; length <= word.Length; length++)
            {
                for (int start = 0; start <= word.Length - length; start++)
                {
                    string sub = word.Substring(start, length);
                    if (bannedWordList.Contains(sub))
                        return false;
                }
            }
        }

        return true;
    }

    public static string ReplaceBannedWord(string text)
    {
        var wordArray = text.Split(' ', ',', '.');
        for (int i = 0; i < wordArray.Length; i++)
        {
            string word = wordArray[i];
            for (int length = 1; length <= word.Length; length++)
            {
                for (int start = 0; start <= word.Length - length; start++)
                {
                    string sub = word.Substring(start, length);
                    if (bannedWordList.Contains(sub))
                        text = text.Replace(sub, "*");
                }
            }
        }

        return text;
    }

    /// <summary>
    /// #색상 변환
    /// </summary>
    public static Color TryParseHtmlString(string htmlStr)
    {
        ColorUtility.TryParseHtmlString(htmlStr, out Color color);
        return color;
    }

    // Convert an SkillData to a byte array
    public static byte[] DataToByteArray<T>(T obj)
    {
        if (obj == null)
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);

        return ms.ToArray();
    }

    // Convert a byte array to an SkillData
    public static T ByteArrayToSkillData<T>(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        T obj = (T)binForm.Deserialize(memStream);

        return obj;
    }

    // Scheduler
    //    GameManager.Instance.globalTimeUtc.Subscribe(l =>
    //        {
    //            if (diceTowerPanels[0].activeSelf && !isExpire)
    //            {
    //                TimeSpan remainTime = Global.seasonEndTime.Subtract(GameManager.Instance.globalTimeUtc.Value);
    //    CheckExpireSeason(remainTime.TotalSeconds);
    //}
    //        }).AddTo(gameObject);

//    int pointerID;



//#if UNITY_EDITOR

//    pointerID = -1; //PC나 유니티 상에서는 -1

//#elif UNITY_IOS || UNITY_IPHONE

//        pointerID = 0;  // 휴대폰이나 이외에서 터치 상에서는 0 

//#endif

 

//if (EventSystem.current.IsPointerOverGameObject(pointerID) == false)

//{

//    //UI 상에서 클릭하지 않았을 때, 함수

//}

 
}
