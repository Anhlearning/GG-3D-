using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    List<ShopItemUi>shopItemUis=new List<ShopItemUi>();

    private void Start() {
        InitShopItem();
        BackBtn.onClick.AddListener(uIManager.SwitchToGamePlayUI);
        creditComp.onCreditChange+=UpdateCredit;
        UpdateCredit(creditComp.Credit);
    }
    private void UpdateCredit(int credit){
    creditText.SetText(credit.ToString());
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
