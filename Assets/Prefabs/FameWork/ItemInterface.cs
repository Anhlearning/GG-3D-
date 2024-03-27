using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EteamRelation{
    Friendly,
    Enemy,
    Neutral
}


public interface ItemInterface 
{
   public int GetTeamID(){return -1;}
   public EteamRelation GetRelationTowards(GameObject other){
    ItemInterface otherTeamInterface=other.GetComponent<ItemInterface>();
    if(otherTeamInterface==null){
        return EteamRelation.Neutral;
    }
    if(otherTeamInterface.GetTeamID()==GetTeamID()){
        return EteamRelation.Friendly;
    }
    else if(otherTeamInterface.GetTeamID()==-1 || GetTeamID()==-1){
        return EteamRelation.Neutral;
    }
    return EteamRelation.Enemy;
   }
}
