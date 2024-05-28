using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveLoc : BTNode
{
   NavMeshAgent agent; string lockey;Vector3 loc;   float acceptableDistance;
   BehaviorTree tree;
   public BTTask_MoveLoc(BehaviorTree tree,string key,float acceptableDistance=2f){
        this.lockey=key;
        this.acceptableDistance=acceptableDistance;
        this.tree=tree;
   }
    protected override NodeResult Execute()
    {
        BlackBoard blackBoard =tree.BlackBoard;
        if(blackBoard ==null || !blackBoard.GetBlackboardData(lockey,out loc)){
            return NodeResult.Failure;
        }
        agent=tree.GetComponent<NavMeshAgent>();
        if(agent==null){
            return NodeResult.Failure;
        }
        if(IsLocInAcceptableDistance()){
            return NodeResult.Success;
        }
        agent.SetDestination(loc);
        agent.isStopped=false;
        return NodeResult.Inprogress;
    }
    protected override NodeResult Update()
    {
        if(IsLocInAcceptableDistance()){
            agent.isStopped=true;
            return NodeResult.Success;
        }
        return NodeResult.Inprogress;
    }
    Boolean IsLocInAcceptableDistance(){
        return Vector3.Distance(loc,tree.transform.position)<=acceptableDistance;
    }
    protected override void End()
    {
        agent.isStopped=true;
        base.End();
    }
}
