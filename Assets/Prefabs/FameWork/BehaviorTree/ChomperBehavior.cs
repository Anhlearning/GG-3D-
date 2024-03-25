using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructorTree(out BTNode node)
    {   
        Selector RootSelector = new Selector();

        Sequencer attackTargetSeq=new Sequencer();

        BTTask_MoveToTarget moveToTarget=new BTTask_MoveToTarget(this,"target",2f);

        attackTargetSeq.AddChildren(moveToTarget);

        BlackboardDecorator attackTargetDecorator=new BlackboardDecorator(this,
        attackTargetSeq,"target",
        BlackboardDecorator.RunCondition.KeyExists,
        BlackboardDecorator.NotifyRule.RunConditionChange,
        BlackboardDecorator.NotifiAbort.both);

        RootSelector.AddChildren(attackTargetDecorator);

        Sequencer patrollingSeq=new Sequencer();

        BTTask_GetNextPatrolPoint getNextPatrolPoint=new BTTask_GetNextPatrolPoint(this,"patrolPoints");
        BTTask_MoveLoc moveTopatrolPoint=new BTTask_MoveLoc(this,"patrolPoints",3f);
        BTTask_Wait wait=new BTTask_Wait(3f);
        patrollingSeq.AddChildren(getNextPatrolPoint);
        patrollingSeq.AddChildren(wait);
        patrollingSeq.AddChildren(moveTopatrolPoint);
        
        RootSelector.AddChildren(patrollingSeq);

        node=RootSelector;
    }
}


