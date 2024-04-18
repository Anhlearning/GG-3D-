using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageComponent : MonoBehaviour,ItemInterface
{
  [SerializeField] protected bool damageFriendly;
  [SerializeField] protected bool damageEnenmy;
  [SerializeField] protected bool damageNatural;

   ItemInterface teaminterface;
   public int GetTeamID(){
        if(teaminterface !=null){
            return teaminterface.GetTeamID();
        }
        return -1;
   }

   public void SetTeamInterFace(ItemInterface teamInterface){
        this.teaminterface=teamInterface;
   }

    public bool shouldDamage(GameObject other){
        if(teaminterface ==null){
            return false;
        }
        EteamRelation relation =teaminterface.GetRelationTowards(other);
        if(damageFriendly && relation==EteamRelation.Friendly){
            return true;
        }
        if(damageEnenmy && relation==EteamRelation.Enemy){
            return true;
        }
        if(damageNatural && relation ==EteamRelation.Neutral)
            return true;
        return false;
    }
}
