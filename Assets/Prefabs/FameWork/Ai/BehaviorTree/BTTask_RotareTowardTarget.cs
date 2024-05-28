using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RotareTowardTarget : BTNode
{
    BehaviorTree tree; float acceptableDegrees;string targetkey;BehaviorTreeInterface behaviorTreeInterface;GameObject target;
    public BTTask_RotareTowardTarget(BehaviorTree tree,string targetkey,float acceptableDegrees=10f){
        this.tree=tree;
        this.targetkey=targetkey;
        this.acceptableDegrees=acceptableDegrees;

        this.behaviorTreeInterface=tree.GetBehaviorTreeInterface();
    }
    protected override NodeResult Execute()
    {
        if(tree==null || tree.BlackBoard==null){
            return NodeResult.Failure;
        }
        if(behaviorTreeInterface ==null){
            return NodeResult.Failure;
        }
        if(!tree.BlackBoard.GetBlackboardData(targetkey,out target)){
            return NodeResult.Failure;
        }
        if(IsInacceptableDegree()){
            return NodeResult.Success;
        }
        tree.BlackBoard.onBlackBroadValueChange+=BlackBoardValueChanged;
        return NodeResult.Inprogress;
    }
    private void BlackBoardValueChanged(string key,object val){
        if(key==targetkey){
            target=(GameObject)val;
        }
    }
    protected override NodeResult Update()
    {   
        if(target ==null){
            return NodeResult.Failure;
        }
        if(IsInacceptableDegree()){
            return NodeResult.Success;
        }
        behaviorTreeInterface.RorateTowards(target);

        return NodeResult.Inprogress;
    }
    bool IsInacceptableDegree(){
        if(target==null){
            return false;
        }
        Vector3 targetDir=(target.transform.position-tree.transform.position).normalized;
        Vector3 dir=tree.transform.forward;

        float degrees=Vector3.Angle(targetDir,dir);

        return degrees <=acceptableDegrees;

    }
    protected override void End()
    {
         tree.BlackBoard.onBlackBroadValueChange-=BlackBoardValueChanged;
         base.End();
    }
}
