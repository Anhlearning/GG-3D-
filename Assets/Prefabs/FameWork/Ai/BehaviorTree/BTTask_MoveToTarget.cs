using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveToTarget : BTNode
{
   NavMeshAgent agent;
   string targetkey;
   GameObject target;
   float acceptableDistance;
   BehaviorTree tree;

   public BTTask_MoveToTarget(BehaviorTree tree,string key,float acceptableDistance=2f){
        this.targetkey=key;
        this.acceptableDistance=acceptableDistance;
        this.tree=tree;
   }
    protected override NodeResult Execute()
    {
        BlackBoard blackBoard=tree.BlackBoard;
        if(blackBoard==null || !blackBoard.GetBlackboardData(targetkey,out target)){
            return NodeResult.Failure;
        }
        agent=tree.GetComponent<NavMeshAgent>();
        if(agent==null){
            return NodeResult.Failure;
        }
        if(IsTargetInAcceptableDistance()){
            return NodeResult.Success;
        }
        blackBoard.onBlackBroadValueChange+=BlackboardValueChanged;

        agent.SetDestination(target.transform.position);
        agent.isStopped=false;
        return NodeResult.Inprogress;
    }
    private void BlackboardValueChanged(string key,object val){
        if(key==targetkey){
            target=(GameObject)val;
        }
    }
    protected override NodeResult Update()
    {
        if(target==null){
            agent.isStopped=true;
            return NodeResult.Failure;
        }
        agent.SetDestination(target.transform.position);
        if(IsTargetInAcceptableDistance()){
            agent.isStopped=true;
            return NodeResult.Success;
        }
        return NodeResult.Inprogress;
    }


    bool IsTargetInAcceptableDistance(){
        return Vector3.Distance(target.transform.position,tree.transform.position)<=acceptableDistance;
    }
    protected override void End()
    {
        agent.isStopped=true;
        tree.BlackBoard.onBlackBroadValueChange-=BlackboardValueChanged;
        base.End();
    }
}
