using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_GroupMoveToLastSeenLoc : BTTask_Group
{

    float acceptableDistance;
    public BTTask_GroupMoveToLastSeenLoc(BehaviorTree tree,float acceptableDistance=3f):base(tree)
    {
        this.acceptableDistance=acceptableDistance;
    }
    protected override void ConstructorTree(out BTNode Root)
    {
       Sequencer CheckLastSeenLoSeq= new Sequencer();
        BTTask_MoveLoc MovetoLastSeenLoc=new BTTask_MoveLoc(tree,"LastSeenLoc",acceptableDistance);
        BTTask_Wait WaitLastSeenLoc=new BTTask_Wait(2f);
        BTTask_RemoveBlackBoardData removeLastSeenLoc=new BTTask_RemoveBlackBoardData(tree,"LastSeenLoc");
        CheckLastSeenLoSeq.AddChildren(MovetoLastSeenLoc);
        CheckLastSeenLoSeq.AddChildren(WaitLastSeenLoc);
        CheckLastSeenLoSeq.AddChildren(removeLastSeenLoc);

        BlackboardDecorator checkLastSeenLoDecorator=new BlackboardDecorator(tree,CheckLastSeenLoSeq,"LastSeenLoc",
                                                                                  BlackboardDecorator.RunCondition.KeyExists,
                                                                                  BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                                  BlackboardDecorator.NotifiAbort.none);
        
        Root=checkLastSeenLoDecorator;
    }
}
