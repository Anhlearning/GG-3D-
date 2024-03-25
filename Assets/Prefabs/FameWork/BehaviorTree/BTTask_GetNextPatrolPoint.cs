using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BTTask_GetNextPatrolPoint : BTNode
{
   PatrollingComponent patrollingComp;
    BehaviorTree tree;

    string patrollKey;
   public BTTask_GetNextPatrolPoint(BehaviorTree tree,String tmp){
        patrollingComp =tree.GetComponent<PatrollingComponent>();
        this.tree=tree;
        this.patrollKey=tmp;
   }

    protected override NodeResult Execute()
    {
        if(patrollingComp !=null && patrollingComp.GetNextPatrolPoint(out UnityEngine.Vector3 point)){
            tree.BlackBoard.SetOrAddData(patrollKey,point);
            return NodeResult.Success;
        }
        return NodeResult.Failure;
    }
}
