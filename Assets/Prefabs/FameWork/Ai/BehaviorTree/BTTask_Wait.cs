using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BTTask_Wait : BTNode
{
    float WaitTime=2f;
    float timeElapsed=0f;
    public BTTask_Wait(float waitTime)
    {
        this.WaitTime=waitTime;
    }
    protected override void End()
    {
        base.End();
    }

    protected override NodeResult Execute()
    {
       if(WaitTime<=0f){
          return  NodeResult.Success;
       }
        timeElapsed=0f;
        // Debug.Log($"Watting start Duration:{WaitTime}");
        return NodeResult.Inprogress;
    }

    protected override NodeResult Update()
    {
        timeElapsed+=Time.deltaTime;
        if(timeElapsed>=WaitTime){

            return NodeResult.Success;
        }
        return  NodeResult.Inprogress;
    }
}
