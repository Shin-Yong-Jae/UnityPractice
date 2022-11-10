using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Util
{
    public static double TotalSeconds(this DateTime dateTime) => (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds * 0.001;

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
}
