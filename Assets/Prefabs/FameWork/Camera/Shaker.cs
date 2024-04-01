using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] Transform shakeTransform ;
    [SerializeField] float shakeMag=1;
    [SerializeField] float ShakeDuration=0.1f;
    [SerializeField] float ShakeRecoverySpeed=10f;

    Coroutine ShakeCoroutine;
    bool isShaking ;
    Vector3 originalPos;

    Quaternion originalRot;
    void Start()
    {
        originalPos=transform.localPosition;
    }
    public void StartShake(){
        if(ShakeCoroutine ==null){
            isShaking=true;
            ShakeCoroutine=StartCoroutine(shakeStarted());
        }
    }
    IEnumerator shakeStarted(){
        yield return new WaitForSeconds(ShakeDuration);
        isShaking=false;
        ShakeCoroutine=null;
    }

    // Update is called once per frame
    private void LateUpdate() {
        processShake();
    }

    void processShake(){
        if(isShaking){
            Vector3 ShakeAmt=new Vector3(Random.value,Random.value,Random.value)*shakeMag*(Random.value>0.5?-1:1);
            shakeTransform.localPosition+=ShakeAmt;
        }
        else {
            shakeTransform.localPosition=Vector3.Lerp(shakeTransform.localPosition,originalPos,Time.deltaTime*ShakeRecoverySpeed);
        }
    }
}
