using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownDecorator : Decorator
{   
    float coolDownTime;
    float lastExecutionTime=-1;
    bool failOnCoolDown;
    public CoolDownDecorator(BehaviorTree tree,BTNode child,float coolDownTime,bool failOnCoolDown=false) : base(child)
    {
        this.coolDownTime=coolDownTime;
        this.failOnCoolDown=failOnCoolDown;
    }

    protected override NodeResult Execute()
    {
        if(coolDownTime ==0){
            return NodeResult.Inprogress;
        }
        //first
        if(lastExecutionTime == -1 ){
            lastExecutionTime=Time.timeSinceLevelLoad;
            return NodeResult.Inprogress;
        }
        //not finish
        if(Time.timeSinceLevelLoad-lastExecutionTime<coolDownTime){
            if(failOnCoolDown){
                return NodeResult.Failure;
            }
            else {
                return NodeResult.Success;
            }
        }
        //cooldown Is finish
        lastExecutionTime =Time.timeSinceLevelLoad;
        return NodeResult.Inprogress;
    }
    protected override NodeResult Update()
    {
        return getChild().UpdateNode();
        
        
    }
}
