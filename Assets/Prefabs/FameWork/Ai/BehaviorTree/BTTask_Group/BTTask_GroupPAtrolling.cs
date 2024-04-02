using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_GroupPatrolling : BTTask_Group
{
    float acceptableDistance;
    public BTTask_GroupPatrolling(BehaviorTree tree,float acceptableDistance=3f) : base(tree)
    {
        this.acceptableDistance=acceptableDistance;
    }
    protected override void ConstructorTree(out BTNode Root)
    {
        Sequencer patrollingSeq=new Sequencer();
        BTTask_GetNextPatrolPoint getNextPatrolPoint=new BTTask_GetNextPatrolPoint(tree,"patrolPoints");
        BTTask_MoveLoc moveTopatrolPoint=new BTTask_MoveLoc(tree,"patrolPoints",acceptableDistance);
        BTTask_Wait wait=new BTTask_Wait(3f);
        patrollingSeq.AddChildren(getNextPatrolPoint);
        patrollingSeq.AddChildren(wait);
        patrollingSeq.AddChildren(moveTopatrolPoint);
        Root=patrollingSeq;
    }
}
