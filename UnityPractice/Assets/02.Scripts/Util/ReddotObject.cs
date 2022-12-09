using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RedDotObject : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private TMP_Text textCount;
    [SerializeField] private float size = 54f;
    private Vector2 sizeCountImg = new Vector2(80f, 61f);

    public void Show(int _count = -1)
    {
        if (_count <= 0)
        {
            textCount.enabled = false;
            img.rectTransform.sizeDelta = Vector2.one * size;
        }
        else
        {
            img.rectTransform.sizeDelta = sizeCountImg;
            textCount.text = _count <= 0 ? "" : _count.ToString();
            textCount.enabled = true;
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
