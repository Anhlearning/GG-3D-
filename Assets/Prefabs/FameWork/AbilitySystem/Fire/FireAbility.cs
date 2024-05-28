using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/FireAbility")]
public class FireAbility : Ability
{   
    [Header("FIRE")]
    [SerializeField] Scanner ScanerPrefab;
    [SerializeField] float fireRadius;
    [SerializeField] float fireDuration;
    [SerializeField] float damageDuration=2f;
    [SerializeField] float fireDamage=20f;
    [SerializeField] GameObject ScanVFX;
    [SerializeField] GameObject DamageVFX;
    public override void ActivateAbility()
    {
        if(!CommitAbility()) return ;
        Scanner fireScanner =Instantiate(ScanerPrefab,AbilityComp.transform);
        fireScanner.SetScanRange(fireRadius);
        fireScanner.SetScanDuraion(fireDuration);
        fireScanner.AddchildAttached(Instantiate(ScanVFX).transform);
        fireScanner.onScanDetectionUpdate+=DetectionUpdate;
        fireScanner.StarScan();
    }

    private void DetectionUpdate(GameObject newDetection){
        ItemInterface detectionTeamInterface=newDetection.GetComponent<ItemInterface>();
        if(detectionTeamInterface==null || detectionTeamInterface.GetRelationTowards(AbilityComp.gameObject)!=EteamRelation.Enemy){
            return;
        }
        HealthComponents EnemyhealthComponents = newDetection.GetComponent<HealthComponents>();
        if(EnemyhealthComponents ==null){
            return;
        }
        AbilityComp.StartCoroutine(ApplyDamageTo(EnemyhealthComponents));
    }
    
    IEnumerator ApplyDamageTo(HealthComponents healthComponents){
        GameObject damageVFX=Instantiate(DamageVFX,healthComponents.transform);
        float damageRate=fireDamage/damageDuration;
        float startTime=0f;
        while(damageDuration > startTime && healthComponents !=null){
            startTime+=Time.deltaTime;
            healthComponents.changeHealth(-damageRate*Time.deltaTime,healthComponents.gameObject);
            yield return new WaitForEndOfFrame();
        }
        if(damageVFX!=null){
            Destroy(damageVFX);
        }
    }
} 
