using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum NodeResult{
    Success,
    Failure,
    Inprogress
}
public abstract class  BTNode 
{
  bool started=false;
  public NodeResult UpdateNode(){
    //one of thing
    if(!started){
        started=true;   
        NodeResult execeResult=Execute();
        if(execeResult!=NodeResult.Inprogress){
            EndNode();
            return execeResult;
        }
    }
    //Time base
    NodeResult updateResult=Update();
    if(updateResult !=NodeResult.Inprogress){
        EndNode();
    }
    return updateResult;

  }
  private void EndNode(){
        started=false;
        End();
  }
  protected virtual void End(){

  }

  protected virtual NodeResult Execute(){
    return NodeResult.Success;
  } 

  protected virtual NodeResult Update(){
    return NodeResult.Success;
  }
  public void Abort(){
    EndNode();
  }
  int priority;
  public int GetPriority(){
    return priority;
  }
  public  virtual void SortPiority(ref int priorityConter){
    priority=priorityConter++;
    Debug.Log($"{this} has priority{priority}");
  }
  public virtual BTNode  Get(){
    return this;
  }
  public virtual void Initialize(){
    
  }

}
