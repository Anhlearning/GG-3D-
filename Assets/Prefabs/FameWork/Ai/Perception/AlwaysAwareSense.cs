using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlwaysAwareSense : SenseComp
{
    [SerializeField] float awareDistance=2f;
    protected override bool InStimuliSenable(PerceptionStimuli stimuli)
    {
        return Vector3.Distance(transform.position,stimuli.transform.position)<=awareDistance;
    }
    protected override void DrawDebug()
    {
        base.DrawDebug();

        Gizmos.DrawWireSphere(transform.position+Vector3.up,awareDistance);
    }

    
}
