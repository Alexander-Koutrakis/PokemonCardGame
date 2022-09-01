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
                return 1;
            case Rarity.Uncommon:
                return 2;
            case Rarity.Rare:
                return 3;
            case Rarity.RareHolo:
                return 4;
            case Rarity.RareRainbow:
                return 5;
            case Rarity.RareHoloVMAX:
                return 6;
            default://Rare Types
                return 7;
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
            case "Rare Holo":
                return Rarity.RareHolo;
            case "Rare Rainbow":
                return Rarity.RareRainbow;
            case "Rare Holo VMAX":
                return Rarity.RareHoloVMAX;
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
