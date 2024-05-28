using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string AttachSlotTag;
    [SerializeField] AnimatorOverrideController overrideController;
    [SerializeField] float AttackRateMul=1f;
    public abstract void Attack();
    [SerializeField] float volume;
    [SerializeField] AudioClip weaponAudio;
    AudioSource weaponAudioSrc;

    public void WeaponPlayAudio(){
        weaponAudioSrc.PlayOneShot(weaponAudio,volume);
    }

    private void Awake() {
        weaponAudioSrc=GetComponent<AudioSource>();
    }
    public string GetAttackSlotTag(){
        return AttachSlotTag;
    }

    public GameObject Owner{
        get;
        private set;
    }

    public void Init(GameObject owner){
        Owner=owner;
        Unequip();
    }

    public void Equip(){
        gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController=overrideController;
        Owner.GetComponent<Animator>().SetFloat("AttackRateMul",AttackRateMul);
    }
    public void Unequip(){
        gameObject.SetActive(false);
    }   
    public void DamageGameObject(GameObject obj,float amt){
        HealthComponents healthcomp=obj.GetComponent<HealthComponents>();
        if(healthcomp!=null){
            healthcomp.changeHealth(-amt,Owner);
        }
    }
     
}
