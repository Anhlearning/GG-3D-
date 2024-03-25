using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] float turnSpeed=8f;
   public float RotateTowards(Vector3 AimDir)
    {   

        float currentTurnSpeed=0;    
        if (AimDir.magnitude != 0)
        {
            Quaternion preVot=transform.rotation;

            float turnLerpAlpha = Time.deltaTime * turnSpeed;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(AimDir, Vector3.up), turnLerpAlpha);
            Quaternion currentRot=transform.rotation;
            
            float Dir=Vector3.Dot(AimDir,transform.right) >0 ? 1 :-1;

            float rotationDelta=Quaternion.Angle(preVot,currentRot)*Dir;

            currentTurnSpeed=rotationDelta/Time.deltaTime;
        }
        return currentTurnSpeed;
    }
}
