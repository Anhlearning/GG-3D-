using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_AttackTarget : BTNode
{
    BehaviorTree tree;
    string targetkey;
    GameObject target;
    public BTTask_AttackTarget(BehaviorTree tree,string x){
        this.tree=tree;
        this.targetkey=x;
    }
    protected override NodeResult Execute()
    {
        if(!tree || tree.BlackBoard==null||!tree.BlackBoard.GetBlackboardData(targetkey,out target)){
            return NodeResult.Failure;
        }
        BehaviorTreeInterface behaviorInterface=tree.GetBehaviorTreeInterface();
        if(behaviorInterface==null){
            return NodeResult.Failure;
        }
        behaviorInterface.attackTarget(target);
        return NodeResult.Success;
    }
}
