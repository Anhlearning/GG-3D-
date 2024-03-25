using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSense : SenseComp
{
    [SerializeField] HealthComponents healthComponents;
    [SerializeField] float HitMemory=2f;
    Dictionary<PerceptionStimuli,Coroutine>HitRecord=new Dictionary<PerceptionStimuli, Coroutine>();
   
    void Start()
    {
        healthComponents.onTakeDamage+=TookDamage;
    }
    protected override bool InStimuliSenable(PerceptionStimuli stimuli){
        return HitRecord.ContainsKey(stimuli);
    }
    public  void TookDamage(float health,float delta,float maxhealth,GameObject Instigator){
        PerceptionStimuli stimuli = Instigator.GetComponent<PerceptionStimuli>();
        if(stimuli!=null){
            Coroutine newForgettingCoroutine=StartCoroutine(ForgetStimuli(stimuli));
            if(HitRecord.TryGetValue(stimuli,out Coroutine onGoingCoroutine)){
                StopCoroutine(onGoingCoroutine);
                HitRecord[stimuli]=newForgettingCoroutine;
            }
            else{
                HitRecord.Add(stimuli,newForgettingCoroutine);
            }
        }
    }
    IEnumerator ForgetStimuli(PerceptionStimuli stimuli){
        yield return new WaitForSeconds(HitMemory);
        HitRecord.Remove(stimuli);
    }
   
}
