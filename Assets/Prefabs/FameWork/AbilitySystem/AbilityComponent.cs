using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityComponent : MonoBehaviour,IPurchaseListener
{
    // xử lý các khả năng 
    [SerializeField] Ability[] InitialAbilities;
    public delegate void OnNewAbilityAdded(Ability newAbility);
    public delegate void OnStaminaChange(float newAmount,float maxAmount);
    private List<Ability>abilities=new List<Ability>();

    public event OnNewAbilityAdded onNewAbilityAdded;
    public event OnStaminaChange onStaminaChange;

    [SerializeField] float stamina=200f;
    [SerializeField] float maxStamina=200f;

    public void BroadcastStaminaValueImeidately(){
        onStaminaChange?.Invoke(stamina,maxStamina);
    }
    void Start()
    {
        foreach(Ability ability in InitialAbilities){
            GiveAbility(ability);
        }
    }
    void GiveAbility (Ability ability){
        //sinh ra khả nang 
        Ability newAbility=Instantiate(ability);
        //khoi tao component cho kha nang
        newAbility.InitAbility(this);
        //them kha nang nay vao danh sach thuc te
        abilities.Add(newAbility);
        onNewAbilityAdded?.Invoke(newAbility);
    }
    public void ActivateAbility(Ability ActiveAbility){
        if(abilities.Contains(ActiveAbility)){
            ActiveAbility.ActivateAbility();
        }
    }
    float GetStamina(){
        return stamina;
    }
    public bool TryConSumeStamina(float staminaConsume){
        if(stamina <= staminaConsume){
            return false;
        }
        stamina-=staminaConsume;
        BroadcastStaminaValueImeidately();
        return true;
    }
    public bool HandlePurchase(ShopItem newPurchase){
        Ability itemAbility = newPurchase.item as Ability;
        Debug.Log($"{newPurchase}");
        if(itemAbility ==null){
            Debug.Log("NULL");
            return false;
        }
        GiveAbility(itemAbility);

        return true;
    }
}
