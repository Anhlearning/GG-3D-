using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class BTTask_GroupAttackTarget : BTTask_Group
{
   
   float moveAcceptableDistance=2;
   float rotationAcceptableRadius=10f;

   float attackCoolDownDuration;
   public BTTask_GroupAttackTarget(BehaviorTree tree,float moveAcceptableDistance=2f,float rotationAcceptableRadius=10f,float attackCoolDownDuration=0f) :base(tree)
   {
    this.moveAcceptableDistance=moveAcceptableDistance;
    this.rotationAcceptableRadius=rotationAcceptableRadius;
    this.attackCoolDownDuration=attackCoolDownDuration;
   }
    protected override void ConstructorTree(out BTNode Root)
    {
        Sequencer attackTargetSeq=new Sequencer();
        BTTask_MoveToTarget moveToTarget=new BTTask_MoveToTarget(tree,"target",moveAcceptableDistance);
        BTTask_RotareTowardTarget rotareTowardTarget =new BTTask_RotareTowardTarget(tree,"target",rotationAcceptableRadius);
        BTTask_AttackTarget attackTarget=new BTTask_AttackTarget(tree,"target");
        CoolDownDecorator attackCoolDownDecorator=new CoolDownDecorator(tree,attackTarget,attackCoolDownDuration);
        attackTargetSeq.AddChildren(moveToTarget);
        attackTargetSeq.AddChildren(rotareTowardTarget);
        attackTargetSeq.AddChildren(attackCoolDownDecorator);
        BlackboardDecorator attackTargetDecorator=new BlackboardDecorator(tree,
                                                                                attackTargetSeq,"target",
                                                                                BlackboardDecorator.RunCondition.KeyExists,
                                                                                BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                                BlackboardDecorator.NotifiAbort.both);

        Root = attackTargetDecorator;
    }
}
