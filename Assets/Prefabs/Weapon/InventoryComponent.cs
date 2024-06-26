using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour,IPurchaseListener
{
   [SerializeField] Weapon[] initWeaponsPrefabs;
   [SerializeField] Transform defaultWeaponSlot;
   [SerializeField] Transform[] weaponSlots;

    List<Weapon>weapons;
    int currentWeaponIdx=-1;
    private void Start() {
        InitializeWeapons();
     }
    
    private void InitializeWeapons(){
        weapons=new List<Weapon>();
        foreach(Weapon weapon in initWeaponsPrefabs){
            GiveNewWeapon(weapon);
        }

        nextWeapon();
    }
    public Weapon GetActiveWeapons(){
        if(hasWeapon()){
            return weapons[currentWeaponIdx];
        }
        return null;
    }
    private void GiveNewWeapon(Weapon weapon){
          Transform weaponSlot=defaultWeaponSlot;
            foreach(Transform slot in weaponSlots){
                if(slot.transform.tag==weapon.GetAttackSlotTag()){
                    weaponSlot =slot;
                }
            }
            Weapon newWeapon=Instantiate(weapon,weaponSlot);
            newWeapon.Init(gameObject);
            weapons.Add(newWeapon);
    }

    public void nextWeapon(){
        int nextWeaponIdx=currentWeaponIdx+1;

        if(nextWeaponIdx>=weapons.Count){
            nextWeaponIdx=0;
        }

        EquipWeapon(nextWeaponIdx);
    }

    private void EquipWeapon(int weaponIndex){
        if(weaponIndex<0||weaponIndex>=weapons.Count){
            return ;
        }

        if(currentWeaponIdx>=0 && currentWeaponIdx<weapons.Count){
            weapons[currentWeaponIdx].Unequip();
        }
        weapons[weaponIndex].Equip();

        currentWeaponIdx=weaponIndex;
    }
    public bool HandlePurchase(ShopItem newPurchase){
        GameObject itemAsGameObject = newPurchase.item as GameObject;

        if(itemAsGameObject ==null){
            return false;
        }
        Weapon itemAsWeapon=itemAsGameObject.GetComponent<Weapon>();
        if(itemAsWeapon ==null) return false;
        bool hasWeapon=true;
        if(weapons.Count==0){
            hasWeapon=false;
        }
        GiveNewWeapon(itemAsWeapon);
        if(!hasWeapon){
            EquipWeapon(0);
            hasWeapon=true;
        }
        return true;
    }
    public bool hasWeapon(){
        return weapons.Count!=0;
    }
}
