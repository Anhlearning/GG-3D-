using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Quaternion=UnityEngine.Quaternion;
public class SightSense : SenseComp
{
    public float sightDistance=5f;
    public float sightHalfAngle=5f;
    public float eyeHeight=1f;
    protected override bool InStimuliSenable(PerceptionStimuli stimuli)
    {
        float distance =UnityEngine.Vector3.Distance(stimuli.transform.position,transform.position);
        if(distance >sightDistance){
            //không nằm trong bán kính
            return false;
        }
        UnityEngine.Vector3 forwardDir=transform.forward;
        UnityEngine.Vector3 stimuliDir=(stimuli.transform.position-transform.position).normalized;

        if(UnityEngine.Vector3.Angle(forwardDir,stimuliDir)>sightHalfAngle){
            //không nằm trong tầm nhìn hình nón
            return false;
        }

        if(Physics.Raycast(transform.position+UnityEngine.Vector3.up*eyeHeight,stimuliDir,out RaycastHit hitInfo,sightDistance)){
            if(hitInfo.collider.gameObject!=stimuli.gameObject){
               //phía trước đã bị 1 vật chắn 
                return false;
            }
        }
        return true;
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Vector3 drawCenter=transform.position+Vector3.up*eyeHeight;
        Gizmos.DrawWireSphere(drawCenter,sightDistance);
        Vector3 LeftLimitDir = Quaternion.AngleAxis(sightHalfAngle,Vector3.up)*transform.forward;
        Vector3 RightLimitDir = Quaternion.AngleAxis(-sightHalfAngle,Vector3.up)*transform.forward;
        
        Gizmos.DrawLine(drawCenter,drawCenter+LeftLimitDir*sightDistance);
        Gizmos.DrawLine(drawCenter,drawCenter+RightLimitDir*sightDistance);
        

    }
}
