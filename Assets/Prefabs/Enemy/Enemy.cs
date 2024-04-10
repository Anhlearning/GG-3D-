using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour ,BehaviorTreeInterface,ItemInterface,ISpawnInterface
{
    [SerializeField] HealthComponents healthComponents;
    [SerializeField] Animator animator;

    [SerializeField] PerceptionComp perceptionComp;

    [SerializeField] BehaviorTree behaviorTree;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] int teamID=2;

    Vector3 previousLoc;

    public int GetTeamID(){
        return teamID;
    }
    public Animator Animator{
        get{
            return animator;
        }
        set{
            animator=value;
        }
    }
    private void Awake() {
        perceptionComp.onPerceptionTargetChanged+=targetChanged;
    }
    protected virtual void Start() {
        if(healthComponents !=null){
            healthComponents.onHealEmpty+=StartDeath;
            healthComponents.onTakeDamage+=TakenDamage;
        }
        previousLoc=transform.position;
    }
    private void Update() {
        Calculate();
    }

    private void Calculate(){
        if(movementComponent ==null) {
            return ;
        }
        Vector3 PosDelta=transform.position-previousLoc;
        float speed=PosDelta.magnitude/Time.deltaTime;

        animator.SetFloat("Speed",speed);
        previousLoc=transform.position;
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
            if(gameObject.GetComponent<CapsuleCollider>() !=null){
                gameObject.GetComponent<CapsuleCollider>().enabled=false;
            }
            animator.SetTrigger("Dead");
        }
    }
    public void OnDeathAnimationFinish(){
        Dead();
        Destroy(gameObject);
    }
    protected virtual void Dead(){

    }
    private void OnDrawGizmos() {
        if(behaviorTree && behaviorTree.BlackBoard.GetBlackboardData("target",out GameObject target)){
            Vector3 drawTargetPos=target.transform.position +Vector3.up;

            Gizmos.DrawWireSphere(drawTargetPos,0.7f);

            Gizmos.DrawLine(transform.position+Vector3.up,drawTargetPos);
        }
    }
    public void RorateTowards(GameObject target,bool verticalAim=false){
        if(movementComponent ==null){
            return ;
        }
        Vector3 Aimdir=target.transform.position - transform.position;
        Aimdir.y=verticalAim ? Aimdir.y : 0;
        Aimdir=Aimdir.normalized;
        
        movementComponent.RotateTowards(Aimdir);
    }
    public void SpawBy(GameObject spawnerGameobject){
        BehaviorTree spawnerBehaiorTree=spawnerGameobject.GetComponent<BehaviorTree>();
        if(spawnerBehaiorTree !=null && spawnerBehaiorTree.BlackBoard.GetBlackboardData<GameObject>("target",out GameObject spawnerTarger)){
                PerceptionStimuli targetStimuli=spawnerTarger.GetComponent<PerceptionStimuli>();
                if(perceptionComp && targetStimuli){
                    perceptionComp.AssignPercievedStimuli(targetStimuli);
                }
        }
    }
    public virtual void attackTarget(GameObject target){
        //override in child 
    }
}
