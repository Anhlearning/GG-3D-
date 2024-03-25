using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponents healthComponents;
    [SerializeField] Animator animator;

    [SerializeField] PerceptionComp perceptionComp;

    [SerializeField] BehaviorTree behaviorTree;
    
    private void Start() {
        healthComponents.onHealEmpty+=StartDeath;
        healthComponents.onTakeDamage+=TakenDamage;
        perceptionComp.onPerceptionTargetChanged+=targetChanged;
    }
    public void targetChanged(GameObject target,bool sensed){
        if(sensed){
           behaviorTree.BlackBoard.SetOrAddData("target",target);
        }
        else{
            behaviorTree.BlackBoard.SetOrAddData("LastSeenLoc",target.transform.position);
            behaviorTree.BlackBoard.RemoveBlackBoardData("target");
        }
    }
    public void TakenDamage(float health,float delta,float maxhealth,GameObject Instigator){

    }
    public void StartDeath(){
        TriggerDeathAnimation();
    }

    public void TriggerDeathAnimation(){
        if(animator!=null){
            animator.SetTrigger("Dead");
        }
    }
    public void OnDeathAnimationFinish(){
        Destroy(gameObject);
        
    }
    private void OnDrawGizmos() {
        if(behaviorTree && behaviorTree.BlackBoard.GetBlackboardData("target",out GameObject target)){
            Vector3 drawTargetPos=target.transform.position +Vector3.up;

            Gizmos.DrawWireSphere(drawTargetPos,0.7f);

            Gizmos.DrawLine(transform.position+Vector3.up,drawTargetPos);
        }
    }
}
