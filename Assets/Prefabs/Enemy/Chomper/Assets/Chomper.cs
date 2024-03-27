using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy 
{
    [SerializeField]TriggerDamageComponent damageComponent;
    public override void attackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
    }
    protected override void Start() {
        base.Start();
        damageComponent.SetTeamInterFace(this);
    }

    public void Log(){
        Debug.Log("EVENT working");
    }
    public void AttackPoint(){
        Debug.Log("AttackPoint");
        if(damageComponent){
            damageComponent.SetDamageEnable(true);
        }
    }
    public void AttackEnd(){
        if(damageComponent){
            Debug.Log("EndAttack");
            damageComponent.SetDamageEnable(false);
        }
    }
}
