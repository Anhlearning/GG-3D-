using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Image =UnityEngine.UI.Image;
public class ShopItemUi : MonoBehaviour
{
   [SerializeField] Image Icon;
   [SerializeField] TextMeshProUGUI TitleText;
   [SerializeField] TextMeshProUGUI PriceText;
   [SerializeField] TextMeshProUGUI Descrpetion;

   [SerializeField] Button button;
   [SerializeField] Image GrayOutCover;

   ShopItem item;

   [SerializeField] Color InEfficientCreditColor;
   [SerializeField] Color SurffiicentCreditColor;

   public void Init(ShopItem item,int Avaliablecredits){
        this.item=item;
        Icon.sprite=item.Itemicon;
        TitleText.text=item.title;
        PriceText.text="$"+item.price.ToString();
        Descrpetion.text=item.description;

        RefreshRate(Avaliablecredits);
   }
   private void RefreshRate(int tmp){
        if(tmp<item.price){
            GrayOutCover.enabled=true;
            PriceText.color=InEfficientCreditColor;
        }
        else {
            GrayOutCover.enabled=false;
            PriceText.color=SurffiicentCreditColor;
        }
   }
}
