using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Shop/ShopSystem")]
public class ShopSystem : ScriptableObject{
    [SerializeField] ShopItem[] shopItems;

    public ShopItem[] GetShopItems(){
        return shopItems;
    }

    public bool TryPurChase(ShopItem selectedItem,CreditComponent purchase){
         return purchase.Purchase(selectedItem.price,selectedItem);
    }
}
