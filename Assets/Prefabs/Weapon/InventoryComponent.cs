using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
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

        nextWeapon();
    }
    public Weapon GetActiveWeapons(){
        return weapons[currentWeaponIdx];
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

}
