using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VFXSpeac{
    public ParticleSystem particleSystem;
    public float size;
}
public class Spawner : Enemy
{
    [SerializeField]VFXSpeac[] paricleSys;
    protected override void Dead()
    {
        foreach(VFXSpeac spec in paricleSys){
            ParticleSystem particle = Instantiate(spec.particleSystem);
            particle.transform.position=transform.position;
            particle.transform.localScale=Vector3.one*spec.size;
        }
    }
}
