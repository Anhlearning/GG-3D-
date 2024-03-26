using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructorTree(out BTNode node)
    {   
        Selector RootSelector = new Selector();

        #region attackTarget
        Sequencer attackTargetSeq=new Sequencer();
        BTTask_MoveToTarget moveToTarget=new BTTask_MoveToTarget(this,"target",2f);
        BTTask_RotareTowardTarget rotareTowardTarget =new BTTask_RotareTowardTarget(this,"target",10f);
        BTTask_AttackTarget attackTarget=new BTTask_AttackTarget(this,"target");
        attackTargetSeq.AddChildren(moveToTarget);
        attackTargetSeq.AddChildren(rotareTowardTarget);
        attackTargetSeq.AddChildren(attackTarget);
        BlackboardDecorator attackTargetDecorator=new BlackboardDecorator(this,
                                                                                attackTargetSeq,"target",
                                                                                BlackboardDecorator.RunCondition.KeyExists,
                                                                                BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                                BlackboardDecorator.NotifiAbort.both);

        RootSelector.AddChildren(attackTargetDecorator);
        #endregion attackTarget
        
        



        #region CheckLastSeenLocation
        Sequencer CheckLastSeenLoSeq= new Sequencer();
        BTTask_MoveLoc MovetoLastSeenLoc=new BTTask_MoveLoc(this,"LastSeenLoc",3);
        BTTask_Wait WaitLastSeenLoc=new BTTask_Wait(2f);
        BTTask_RemoveBlackBoardData removeLastSeenLoc=new BTTask_RemoveBlackBoardData(this,"LastSeenLoc");
        CheckLastSeenLoSeq.AddChildren(MovetoLastSeenLoc);
        CheckLastSeenLoSeq.AddChildren(WaitLastSeenLoc);
        CheckLastSeenLoSeq.AddChildren(removeLastSeenLoc);

        BlackboardDecorator checkLastSeenLoDecorator=new BlackboardDecorator(this,CheckLastSeenLoSeq,"LastSeenLoc",
                                                                                  BlackboardDecorator.RunCondition.KeyExists,
                                                                                  BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                                  BlackboardDecorator.NotifiAbort.none);
        
        RootSelector.AddChildren(checkLastSeenLoDecorator);
        #endregion CheckLastSeenLocation
        
        #region  patrollingSeq
        Sequencer patrollingSeq=new Sequencer();

        BTTask_GetNextPatrolPoint getNextPatrolPoint=new BTTask_GetNextPatrolPoint(this,"patrolPoints");
        BTTask_MoveLoc moveTopatrolPoint=new BTTask_MoveLoc(this,"patrolPoints",3f);
        BTTask_Wait wait=new BTTask_Wait(3f);
        patrollingSeq.AddChildren(getNextPatrolPoint);
        patrollingSeq.AddChildren(wait);
        patrollingSeq.AddChildren(moveTopatrolPoint);
        
        RootSelector.AddChildren(patrollingSeq);
        #endregion patrollingSeq
        node=RootSelector;
    }
}


