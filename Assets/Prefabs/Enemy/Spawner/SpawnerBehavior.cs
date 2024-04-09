using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : BehaviorTree
{
    protected override void ConstructorTree(out BTNode node)
    {
        BTTask_Spawn spawnTask=new BTTask_Spawn(this);
        CoolDownDecorator spawnCooldownDeco=new CoolDownDecorator(this,spawnTask,3f);
        BlackboardDecorator spawnBBDecorator=new BlackboardDecorator(this,spawnCooldownDeco,"target",BlackboardDecorator.RunCondition.KeyExists,BlackboardDecorator.NotifyRule.RunConditionChange,BlackboardDecorator.NotifiAbort.both);

        node=spawnBBDecorator;
    }
}
