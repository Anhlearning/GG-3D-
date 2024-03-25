using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Compositor : BTNode
{
    //chay nhieu node voi nhau 
   LinkedList<BTNode>children=new LinkedList<BTNode>();
   LinkedListNode<BTNode>CurrentChild=null;

    public void AddChildren(BTNode Children){
        children.AddLast(Children);
    }

    public BTNode GetCurrentChild(){
        return CurrentChild.Value;
    }
    protected override NodeResult Execute()
    {
        if(children.Count==0){
            return NodeResult.Success;
        }
        CurrentChild=children.First;
        return NodeResult.Inprogress;
    }
    protected bool Next(){
        if(CurrentChild!=children.Last){
            CurrentChild=CurrentChild.Next;
            return true;
        }
        return false;
    }

    protected override NodeResult Update()
    {
        return base.Update();
    }
    protected override void End()
    {
        if(CurrentChild ==null){
            return;
        }
        CurrentChild.Value.Abort();
        CurrentChild=null;
    }
    public override void SortPiority(ref int priorityConter)
    {
        base.SortPiority(ref priorityConter);
        foreach(var child in children){
            child.SortPiority(ref priorityConter);
        }
    }

    public override BTNode Get()
    {
        if(CurrentChild==null){
            if(children.Count!=0){
                return children.First.Value.Get();
            }
            else {
                return this;
            }
        }
        return CurrentChild.Value.Get();
    }
}
