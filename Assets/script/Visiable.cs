using UnityEngine;

public class Visiable
{
   public static void setVisible(CanvasGroup UiVisual)
    {
        UiVisual.alpha = 1;
        UiVisual.blocksRaycasts = true;
        UiVisual.interactable = true;
    }

    public static void setInvisible(CanvasGroup UiVisual)
    {
        UiVisual.alpha = 0;
        UiVisual.blocksRaycasts = false;
        UiVisual.interactable = false;
    }
}
