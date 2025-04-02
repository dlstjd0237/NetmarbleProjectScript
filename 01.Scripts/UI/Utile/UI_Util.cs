using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public static class UI_Util
{
    public static List<PopUpUI> PopupUIList = new List<PopUpUI>();

    public static void Subscription(PopUpUI ui)
    {
        PopupUIList.Add(ui);
    }

    public static void AllHidePopupUI(PopUpUI ui)
    {
        for (int i = 0; i < PopupUIList.Count; ++i)
        {
            if (PopupUIList[i] == ui)
                continue;

            PopupUIList[i].ShowWindow(false);
        }
    }
    public static void WindowActive(CanvasGroup cG, bool value, float duration)
    {
        int endValue = value ? 1 : 0;
        cG.interactable = value;
        cG.blocksRaycasts = value;
        cG.DOFade(endValue, duration);
    }
}
