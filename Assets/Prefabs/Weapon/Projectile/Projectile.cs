using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] float flightHeight;
   [SerializeField] Rigidbody rigidBody;

   [SerializeField] DamageComponent damageComponent;

   ItemInterface instigatorTeamInterface;
   public void Launch(GameObject instigator,Vector3 Destination){
        instigatorTeamInterface=instigator.GetComponent<ItemInterface>();
        if(instigatorTeamInterface!=null){
            damageComponent.SetTeamInterFace(instigatorTeamInterface);
        }
        //tinh toan ra thoi gian bay
        float gravity=Physics.gravity.magnitude;
        float halfFlightTime=Mathf.Sqrt((2.0f*flightHeight)/gravity);
        //van toc tren khong
        float upSpeed=gravity*halfFlightTime;
        //van toc o be mat ngang 
        Vector3 DestinationVec=Destination-transform.position;
        DestinationVec.y=0;
        float horizontalDist=DestinationVec.magnitude;
        float fwSpeed=horizontalDist/(2.0f*halfFlightTime);

        Vector3 flightVel=Vector3.up*upSpeed+DestinationVec.normalized*fwSpeed;
        rigidBody.AddForce(flightVel,ForceMode.VelocityChange);
   }

   private void OnTriggerEnter(Collider other) {
        
        if(instigatorTeamInterface.GetRelationTowards(other.gameObject)!= EteamRelation.Friendly){
            Explode();
        }
   }
   private void Explode(){
        Destroy(gameObject);
   }
}
