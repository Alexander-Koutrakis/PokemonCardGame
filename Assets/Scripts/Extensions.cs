using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class Extensions 
{
    //Present Rarity as Int
   public static int INTRarity(this Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 4;
            case Rarity.Uncommon:
                return 3;
            case Rarity.Legend:
                return 1;
            default://Rare Types
                return 2;
        }
    }

    public static Rarity StringToRarity(this string rarity)
    {
        switch (rarity)
        {
            case "Common":
                return Rarity.Common;
            case "Uncommon":
                return Rarity.Uncommon;
            case "LEGEND":
                return Rarity.Legend;
            default:
                return Rarity.Rare;
        }
    }

    //Show how many cards can fit in HorizontalLayout group
    // depending on the width of the screen and the withd of the card
    public static int CardLineSize(this RectTransform rectTransform,RectTransform cardRectTransform,HorizontalLayoutGroup horizontalLayoutGroup)
    {
        float offsetAndSpacing = horizontalLayoutGroup.padding.left + horizontalLayoutGroup.padding.right;
        float width = rectTransform.rect.width;
        float cardWidth = cardRectTransform.rect.width + horizontalLayoutGroup.spacing;
        float freeSpace = width - offsetAndSpacing;
        int size = (int)(freeSpace / cardWidth);
        return size;
    }
}
