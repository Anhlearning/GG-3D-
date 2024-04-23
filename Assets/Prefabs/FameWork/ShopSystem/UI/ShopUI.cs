using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ShopSystem shopSystem;
    [SerializeField] ShopItemUi shopItemUiPrefabs;
    [SerializeField] RectTransform ShopList;
    [SerializeField] CreditComponent creditComp;
    List<ShopItemUi>shopItemUis=new List<ShopItemUi>();

    private void Start() {
        InitShopItem();
    }
    private void InitShopItem(){
        ShopItem[] shopItems =shopSystem.GetShopItems();
        foreach(ShopItem item in shopItems){
            AddShopItem(item);
        }
    }
    private void AddShopItem(ShopItem item){
        ShopItemUi newItemUi=Instantiate(shopItemUiPrefabs,ShopList);
        newItemUi.Init(item,creditComp.Credit);
        shopItemUis.Add(newItemUi);
    }
}
