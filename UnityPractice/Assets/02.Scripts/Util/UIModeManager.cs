using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class UIModeManager : MonoBehaviour
{
    #region Variables
    [System.Serializable]
    public class UIModeInstance
    {
        [Header("Text Color", order = 0), Tooltip("텍스트의 색상을 변경")]
        public Color changeTextColor = Color.white;
        public List<Text> textList = new List<Text>();
        [Header("Text Outline Color", order = 1), Tooltip("텍스트의 아웃라인 색상을 변경")]
        public Color changeTextOutlineColor = Color.white;
        public List<Outline> textOutlineList = new List<Outline>();
        [Header("Text Shadow Color", order = 2), Tooltip("텍스트의 그림자 색상을 변경")]
        public Color changeTextShadowColor = Color.white;
        public List<Shadow> textShadowList = new List<Shadow>();

        [Header("TMP Text Color", order = 3), Tooltip("TMP 텍스트의 색상을 변경")]
        public Color changeTmpTextColor = Color.white;
        public List<TMP_Text> tmpTextList = new List<TMP_Text>();

        [Header("TMP Text Material", order = 4), Tooltip("TMP 텍스트의 머테리얼을 변경")]
        public Material changeTmpMaterial = null;
        public List<TMP_Text> tmpMaterialList = new List<TMP_Text>();
        [Header("Image Color", order = 5), Tooltip("이미지 색상을 변경")]
        public Color changeImageColor = Color.white;
        public List<Image> imageColorList = new List<Image>();

        [Header("Image Sprite", order = 6), Tooltip("이미지의 스프라이트를 변경")]
        public Sprite changeSpriteImage = null;
        public List<Image> imageSpriteList = new List<Image>();

        public void OnChangeMode()
        {
            // 텍스트 색상 변환
            for (int i = 0; i < textList.Count; i++)
            {
                if (textList[i] == null)
                    continue;
                textList[i].color = changeTextColor;
            }

            // 텍스트 아웃라인 색상 변환
            for (int i = 0; i < textOutlineList.Count; i++)
            {
                if (textOutlineList[i] == null)
                    continue;
                textOutlineList[i].effectColor = changeTextOutlineColor;
            }

            // 텍스트 그림자 색상 변환
            for (int i = 0; i < textShadowList.Count; i++)
            {
                if (textShadowList[i] == null)
                    continue;
                textShadowList[i].effectColor = changeTextShadowColor;
            }

            // TMP 텍스트 색상 변환
            for (int i = 0; i < tmpTextList.Count; i++)
            {
                if (tmpTextList[i] == null)
                    continue;
                tmpTextList[i].color = changeTmpTextColor;
            }

            // TMP 텍스트 메테리얼 변경
            if (changeTmpMaterial != null)
            {
                for (int i = 0; i < tmpMaterialList.Count; i++)
                {
                    if (tmpMaterialList[i] == null)
                        continue;
                }
            }

            // 이미지 색상 변환
            for (int i = 0; i < imageColorList.Count; i++)
            {
                if (imageColorList[i] == null)
                    continue;
                imageColorList[i].color = changeImageColor;
            }

            // 이미지 스프라이트 변경
            if (changeSpriteImage != null)
            {
                for (int i = 0; i < imageSpriteList.Count; i++)
                {
                    if (imageSpriteList[i] == null)
                        continue;
                    imageSpriteList[i].sprite = changeSpriteImage;
                }
            }
        }
    }

    [System.Serializable]
    public class UIMode
    {
        public UIModeCategory category;
        public UIModeInstance list = new UIModeInstance();
    }

    // UIModeManager.cs 에서 사용하는 모드 구분 타입
    public enum UIModeCategory
    {
        Positive,
        Nagative,
        Active,
        Deactive,
    }

    [Header("Test Type")]
    public UIModeCategory testModeType = UIModeCategory.Positive;

    [Header("Mode Object List")]
    public List<UIMode> uimodeList = new List<UIMode>();
    #endregion Variables

    #region Main Methods
    public void ProcessChangeMode(UIModeCategory type)
    {
        List<UIMode> findList = uimodeList.FindAll(x => x.category == type);
        for (int i = 0; i < findList.Count; i++)
        {
            findList[i].list.OnChangeMode();
        }
    }

    [ContextMenu("ProcessChangeMode")]
    public void ProcessChangeMode()
    {
        List<UIMode> findList = uimodeList.FindAll(x => x.category == testModeType);
        for (int i = 0; i < findList.Count; i++)
        {
            findList[i].list.OnChangeMode();
        }
    }
    #endregion Main Methods
}
