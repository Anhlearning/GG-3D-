using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/HealthRegen")]
public class HealthRegen : Ability
{
    [SerializeField] float healthRegenAmt;
    [SerializeField] float healthRegenDuration;

    public override void ActivateAbility()
    {
        if(!CommitAbility()) return ;

        HealthComponents Healthcomp=AbilityComp.GetComponent<HealthComponents>();

        if(Healthcomp!=null){
            if(healthRegenDuration ==0f){
                Healthcomp.changeHealth(healthRegenAmt,AbilityComp.gameObject); 
            }
            AbilityComp.StartCoroutine(StartHealthRegen(healthRegenAmt,healthRegenDuration,Healthcomp));
        }
    }
    IEnumerator StartHealthRegen(float amt,float duration,HealthComponents healthComponents){
        float counter =duration;
        float RegenRate=amt/duration;
        while(counter > 0){
            counter-=Time.deltaTime;
            healthComponents.changeHealth(RegenRate*Time.deltaTime,AbilityComp.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }
}
