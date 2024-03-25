using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RemoveBlackBoardData : BTNode
{
    BehaviorTree tree;
    string keyRemove;

    public BTTask_RemoveBlackBoardData(BehaviorTree tree,string keyToRemove){
        this.tree=tree;
        this.keyRemove=keyToRemove;
    }


    protected override NodeResult Execute()
    {
        if(tree != null && tree.BlackBoard !=null){
            tree.BlackBoard.RemoveBlackBoardData(keyRemove);
            return NodeResult.Success;
        }
        return NodeResult.Failure;
    }
}
