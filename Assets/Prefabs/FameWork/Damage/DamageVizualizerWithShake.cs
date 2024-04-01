using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualizerWithShake : DamageVizualizer
{
   [SerializeField] Shaker shaker;

    protected override void TookDamage(float health, float delta, float maxhealth, GameObject Instigator)
    {
        base.TookDamage(health, delta, maxhealth, Instigator);
        if(shaker!=null){
            shaker.StartShake();
        }
    }
}