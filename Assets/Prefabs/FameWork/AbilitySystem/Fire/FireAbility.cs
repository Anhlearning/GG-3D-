using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/FireAbility")]
public class FireAbility : Ability
{
    [SerializeField] Scanner ScanerPrefab;
    [SerializeField] float fireRadius;
    [SerializeField] float fireDuration;
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
        Debug.Log($"Detected: {newDetection.name}");
    }
    
} 
