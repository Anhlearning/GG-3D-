using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree: MonoBehaviour
{
    BTNode RootNode;

    BlackBoard blackBoard =new BlackBoard();

    BehaviorTreeInterface behaviorTreeInterface;
    
    
    public BlackBoard BlackBoard{
      get { return blackBoard; }
    }
    void Start()
    {
        behaviorTreeInterface=GetComponent<BehaviorTreeInterface>();
        ConstructorTree(out RootNode);
        SortTree();
    }
    public void SortTree(){
        int priorityConter=0;
        RootNode.SortPiority(ref priorityConter);
    }
    protected abstract void ConstructorTree(out BTNode node);

    // Update is called once per frame
    void Update()
    {   
        RootNode.UpdateNode();
    }
    public void AbortLowerThan(int priority){
        BTNode currentNode=RootNode.Get();
        if(currentNode.GetPriority()>priority){
            RootNode.Abort();
        }
    }
}
