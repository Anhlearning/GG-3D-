using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public  abstract class BTTask_Group : BTNode
{
   BTNode Root;

   protected BehaviorTree tree;
   public BTTask_Group(BehaviorTree tree)
   {
        this.tree=tree;
   }
   protected abstract void ConstructorTree(out BTNode Root);

    protected override NodeResult Execute()
    {
        return NodeResult.Inprogress;
    }
    protected override NodeResult Update()
    {
        return Root.UpdateNode();
    }
    protected override void End()
    {   
        Root.Abort();
        base.End();
    }
    public override void SortPiority(ref int priorityConter)
    {
        base.SortPiority(ref priorityConter);
        Root.SortPiority(ref priorityConter);
    }
    public override void Initialize()
    {
        base.Initialize();
        ConstructorTree(out Root);
    }
    public override BTNode Get(){
        return Root.Get();
    }
}
