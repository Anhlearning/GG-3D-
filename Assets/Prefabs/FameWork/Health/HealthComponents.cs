using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponents : MonoBehaviour,IRewardListener
{
    public delegate void OnHealthChange(float health,float delta,float maxhealth);
    public delegate void OnTakeDamage(float health,float delta,float maxhealth,GameObject Instigator);

    public delegate void OnHealEmpty(GameObject killer);
   [SerializeField] float health=100;
   [SerializeField] float maxhealth=100;

    public event OnHealthChange onHealthChange;
    public event OnTakeDamage onTakeDamage;

    public event OnHealEmpty onHealEmpty;
    
    public void BroadcastHealthValueImeidately(){
        onHealthChange?.Invoke(health,0,maxhealth);
    }


   public void changeHealth(float amt,GameObject Instigator){
        if(amt ==0 || health==0){
            return ;
        }
        health+=amt;
        if(amt<0){
            onTakeDamage?.Invoke(health,amt,maxhealth,Instigator);
        }
        onHealthChange?.Invoke(health,amt,maxhealth);

        if(health<=0){
            health=0;
            onHealEmpty?.Invoke(Instigator);
        }
        Debug.Log($"Take dame{amt} {gameObject.name}");
   }
    public void Reward(Reward reward){
        health=Mathf.Clamp(health+reward.healthReward,0,maxhealth);
        onHealthChange?.Invoke(health,reward.healthReward,maxhealth);
        
    }

}
