using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour,ItemInterface
{
    [SerializeField]Joystick MoveStick;
    [SerializeField]Joystick AimStick;
    [SerializeField] CharacterController characterController;
    Vector2 moveInput;
    Vector2 aimInput;

    [Header("InventoryComponent")]
    [SerializeField] InventoryComponent inventoryComponent;

    [Header("HealthDamage")]
    [SerializeField] HealthComponents healthComponents;
    [SerializeField] PlayerHealthBar healthBar;
    [Header("UI")]
    [SerializeField] UIManager uIManager;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] int teamID=1;
    [SerializeField]float MoveSpeed=20f;
    [SerializeField] float animTurnSpeed=20f;
    CameraController cameraController;
    Camera mainCam;
    Animator animator;

    float animatorTurnSpeed=0;

    public int GetTeamID(){
        return teamID;
    }
    
    void Start()
    {
        animator=GetComponent<Animator>();
        MoveStick.onStickValueUpdate+=MoveStickUpdate;
        AimStick.onStickValueUpdate+=AimStickUpdate;
        AimStick.onstickTaped+=StartswitchWeapon;
        mainCam=Camera.main; 
        cameraController=FindObjectOfType<CameraController>();
        healthComponents.onHealthChange+=HealthChange;
        healthComponents.BroadcastHealthValueImeidately();
        healthComponents.onHealEmpty+=StartDeathSequence;
    }
    public void StartDeathSequence(){
        animator.SetLayerWeight(2,1);
        animator.SetTrigger("Death");
        uIManager.SetGamePlayControlEnabled(false);
    }
    public void HealthChange(float healt,float delta,float maxHealth){
        healthBar.UpdateHealth(healt,delta,maxHealth);
    }
    private void Update()
    {
        PerformMove(); 
        UpdateCamera();
    }
    public void StartswitchWeapon(){
        animator.SetTrigger("switchWeapon");
    }
    public void switchWeapon(){
        inventoryComponent.nextWeapon();
    }
    public void AimStickUpdate(Vector2 inputValue){
        aimInput=inputValue;
        if(aimInput.magnitude>0){
            animator.SetBool("Attacking",true);
        }
        else {
            animator.SetBool("Attacking",false);
        }
    }
    public void AttackPoint(){
        inventoryComponent.GetActiveWeapons().Attack();
    }

    public void MoveStickUpdate(Vector2 InputVal){
        moveInput=InputVal;
    }
    private void UpdateAim(Vector3 CurrentDir)
    {
        Vector3 AimDir = CurrentDir;
        if (aimInput.magnitude != 0)
        {
            AimDir = StickInputToWorldDir(aimInput);
        }
        RotateTowards(AimDir);
    }
    public Vector3 StickInputToWorldDir(Vector2 inputVal){
        Vector3 rightDir= mainCam.transform.right;
        Vector3 upDir=Vector3.Cross(rightDir,Vector3.up);
        Vector3 worldDir=rightDir*inputVal.x+upDir*inputVal.y;
        return worldDir;
    }
    // Update is called once per frame
    private void PerformMove()
    {
        Vector3 MoveDir = StickInputToWorldDir(moveInput);
        characterController.Move(MoveDir * Time.deltaTime * MoveSpeed);
        UpdateAim(MoveDir);

        float forward = Vector3.Dot(MoveDir,transform.forward);
        float right=Vector3.Dot(MoveDir,transform.right);

        animator.SetFloat("forwardSpeed",forward);
        animator.SetFloat("rightSpeed",right);
        characterController.Move(Vector3.down * Time.deltaTime);
    }

   

    private void UpdateCamera()
    {
        //camera quay khi co toc do va aiminput == 0 và camera có tồn tại  
        if (moveInput.magnitude != 0 && aimInput.magnitude==0 && cameraController!=null)
        {
                cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 AimDir)
    {   

        float currentTurnSpeed=movementComponent.RotateTowards(AimDir);
        
        animatorTurnSpeed=Mathf.Lerp(animatorTurnSpeed,currentTurnSpeed,Time.deltaTime*animTurnSpeed);

        animator.SetFloat("turnSpeed",animatorTurnSpeed);
    }
}
