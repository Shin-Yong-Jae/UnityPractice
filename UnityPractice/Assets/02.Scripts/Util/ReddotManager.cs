using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RedDotType
{
    ShopProduct,
    //Add Type
}

public class RedDotManager : MonoBehaviour
{
    #region Variables
    private Dictionary<RedDotType, RedDotObject> dictRedDotList = new Dictionary<RedDotType, RedDotObject>();
    #endregion Variables

    #region Main Methods
    public void ActivatedRedDot(RedDotType redDotType, bool active, int count = 0)
    {
        if (active)
        {
            SetRedDot(redDotType, redDotType switch
            {
                // Cashing Reddot Object.

                //RedDotType.ShopProduct => lobbyBottomPanel.goReddotShop,
                _ => throw new Exception($"Not correct red dot type : {redDotType}"),
            }, count);
        }
        else
        {
            RemoveRedDot(redDotType);
        }
    }

    private void SetRedDot(RedDotType redDotType, RedDotObject redDotGo, int count = 0)
    {
        if (dictRedDotList.ContainsKey(redDotType))
        {
            redDotGo.Show(count);
            return;
        }

        dictRedDotList.Add(redDotType, redDotGo);
        redDotGo.Show(count);
    }

    private void RemoveRedDot(RedDotType redDotType)
    {
        if (!dictRedDotList.ContainsKey(redDotType))
            return;

        dictRedDotList[redDotType].Hide();
        dictRedDotList.Remove(redDotType);
    }
    #endregion Main Methods
}
