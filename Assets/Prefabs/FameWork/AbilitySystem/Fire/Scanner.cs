using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] Transform ScannerPivot;
    public delegate void  OnScanDetectionUpdate(GameObject newDetection);

    public event OnScanDetectionUpdate onScanDetectionUpdate;
    [SerializeField]float ScanRange;
    [SerializeField] float ScanDuration;
    public void SetScanRange(float ScanRange){
        this.ScanRange=ScanRange;
    }
    public void SetScanDuraion(float x){
        this.ScanDuration=x;
    }
    public void AddchildAttached(Transform newChild){
        newChild.parent=ScannerPivot;
        newChild.localPosition=Vector3.zero;
    }
    public void StarScan(){
        ScannerPivot.localScale=Vector3.zero;
        StartCoroutine(StartScanCoroutine());
    }
    IEnumerator StartScanCoroutine(){
        float scanGrowRate=ScanRange/ScanDuration;
        float startTime=0;
        while(startTime<ScanDuration){
            startTime+=Time.deltaTime;
            ScannerPivot.localScale+=Vector3.one*scanGrowRate*Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        onScanDetectionUpdate?.Invoke(other.gameObject);
    }
}
