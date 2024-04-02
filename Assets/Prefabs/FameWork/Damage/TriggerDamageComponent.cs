using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerDamageComponent : DamageComponent
{
    [SerializeField] float damage;
    [SerializeField] BoxCollider trigger;
    [SerializeField] bool startEnabled=false; 

    public void SetDamageEnable(bool enable){
        trigger.enabled=enable;
    }
    private void Start() {
        SetDamageEnable(startEnabled);
    }
    private void OnTriggerEnter(Collider other) {
         if(!shouldDamage(other.gameObject)){
            return ;
        }
        HealthComponents healthComp=other.GetComponent<HealthComponents>();
        if(healthComp!=null){
            Debug.Log("ATTACK");
            healthComp.changeHealth(-damage,healthComp.gameObject);
        }
    }
}
