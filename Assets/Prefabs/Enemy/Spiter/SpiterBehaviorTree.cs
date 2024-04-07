using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiterBehaviorTree : BehaviorTree
{
    protected override void ConstructorTree(out BTNode node)
    {
        Selector RootSelector = new Selector();

        RootSelector.AddChildren(new BTTask_GroupAttackTarget(this,5f,10f,2f));
      
        RootSelector.AddChildren(new BTTask_GroupMoveToLastSeenLoc(this,3f));
     
        RootSelector.AddChildren(new BTTask_GroupPatrolling(this,3f));

        node=RootSelector;
    }
}
