using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPurchaseListener{
    public bool HandlePurchase(Object newPurchase);
}



public class CreditComponent : MonoBehaviour
{
    [SerializeField] int creadits;
    [SerializeField] Component[] PurchaseListeners;

    List<IPurchaseListener>purchaseListenerInterface=new List<IPurchaseListener>();

    public delegate void OnCreditChange(int newCredit);
    public event OnCreditChange onCreditChange;

    private void Start() {
        CollectPurchaselListeners();
    }

    private void CollectPurchaselListeners(){
        foreach(Component listener in PurchaseListeners){
            IPurchaseListener listenerInterface =listener as IPurchaseListener;
            if(listenerInterface !=null){
                purchaseListenerInterface.Add(listenerInterface);
            }
        }
    }

    private void BroadcastPurchase(Object item){
        foreach(IPurchaseListener purchaseListener in purchaseListenerInterface){
           if( purchaseListener.HandlePurchase(item)){
                return ;
           }
        }
    }

    public int Credit{
        get{return creadits;}
    }



    public bool Purchase(int price ,Object item){
        if(creadits < price) return false;

        creadits-=price;
        onCreditChange?.Invoke(creadits);
        BroadcastPurchase(item);
        return true;
    }

}
