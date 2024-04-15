using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability : ScriptableObject
{
    [SerializeField] Sprite AbilityIcon;
    [SerializeField] float StaminaCost=10f;
    [SerializeField] float CoolDownDuration=2f;
    AbilityComponent abilityComponent;
    bool abilityOnCoolDown=false;
    public delegate void OnCoolDownStarted();
    public event OnCoolDownStarted onCoolDownStarted;
    internal Sprite GetAbilityIcon(){
        return AbilityIcon;
    }
    public void InitAbility(AbilityComponent abilityComponent){
        this.abilityComponent=abilityComponent;
    }
    public abstract void ActivateAbility();

    public AbilityComponent AbilityComp{
        get {return abilityComponent;}
        private set {abilityComponent =value;}
    }

    //check all the condition need to active the ability
    protected bool CommitAbility(){
        if(abilityOnCoolDown) return false;

        if(abilityComponent ==null || !abilityComponent.TryConSumeStamina(StaminaCost) ){
            return false;
        }
        StartAbilityCoolDown();
        //..
        return true;
    }
    void StartAbilityCoolDown(){
        abilityOnCoolDown=true;
        abilityComponent.StartCoroutine(CoolDownCoroutine());
    }
    IEnumerator CoolDownCoroutine(){
        abilityOnCoolDown=true;
        onCoolDownStarted?.Invoke();
        yield return new WaitForSeconds(CoolDownDuration);
        abilityOnCoolDown=false;
    }
     public float getCoolDownDuration(){
        return CoolDownDuration;
    }
}
    

