using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Notifications.iOS;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ShopSystem shopSystem;
    [SerializeField] ShopItemUi shopItemUiPrefabs;
    [SerializeField] RectTransform ShopList;
    [SerializeField] UIManager uIManager;
    [SerializeField] TextMeshProUGUI creditText;
   [SerializeField] Button BackBtn;
   [SerializeField] Button BuyBtn;
    [SerializeField] CreditComponent creditComp;
    List<ShopItemUi>shopItems=new List<ShopItemUi>();
    ShopItemUi selectedItem;

    private void Start() {
        InitShopItem();
        BackBtn.onClick.AddListener(uIManager.SwitchToGamePlayUI);
        BuyBtn.onClick.AddListener(TryPurchaseItem);
        creditComp.onCreditChange+=UpdateCredit;
        UpdateCredit(creditComp.Credit);
    }
    private void TryPurchaseItem(){
        if(!selectedItem || !shopSystem.TryPurChase(selectedItem.Get(),creditComp)){
            Debug.Log("! Item or Can't Buy");
            return ;
        }
        RemoveItem(selectedItem);
    }
    private void RemoveItem(ShopItemUi itemUiremove){
        shopItems.Remove(itemUiremove);
        Destroy(itemUiremove.gameObject);
    }
    private void UpdateCredit(int credit){
        creditText.SetText(credit.ToString());
        RefeshItems();
    }
    private void RefeshItems(){
        foreach(ShopItemUi shopItemUi in shopItems){
            shopItemUi.RefreshRate(creditComp.Credit);
        }
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
        newItemUi.onItemSelected+=ItemSelected;
        shopItems.Add(newItemUi);
    }
    private void ItemSelected(ShopItemUi Item){
        selectedItem=Item;
    }
}
