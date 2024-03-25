using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]Joystick MoveStick;
    [SerializeField]Joystick AimStick;
    [SerializeField] CharacterController characterController;
    Vector2 moveInput;
    Vector2 aimInput;

    [Header("InventoryComponent")]
    [SerializeField] InventoryComponent inventoryComponent;

    [SerializeField]float MoveSpeed=20f;
    [SerializeField] float animTurnSpeed=20f;
    [SerializeField] float turnSpeed=20f;
    CameraController cameraController;
    Camera mainCam;
    Animator animator;

    float animatorTurnSpeed=0;
    
    void Start()
    {
        animator=GetComponent<Animator>();
        MoveStick.onStickValueUpdate+=MoveStickUpdate;
        AimStick.onStickValueUpdate+=AimStickUpdate;
        AimStick.onstickTaped+=StartswitchWeapon;
        mainCam=Camera.main; 
        cameraController=FindObjectOfType<CameraController>();
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
        // characterController.Move(Vector3.down * Time.deltaTime);
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

        float currentTurnSpeed=0;    
        if (AimDir.magnitude != 0)
        {
            Quaternion preVot=transform.rotation;

            float turnLerpAlpha = Time.deltaTime * turnSpeed;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(AimDir, Vector3.up), turnLerpAlpha);
            Quaternion currentRot=transform.rotation;
            
            float Dir=Vector3.Dot(AimDir,transform.right) >0 ? 1 :-1;

            float rotationDelta=Quaternion.Angle(preVot,currentRot)*Dir;

            currentTurnSpeed=rotationDelta/Time.deltaTime;
        }
        animatorTurnSpeed=Mathf.Lerp(animatorTurnSpeed,currentTurnSpeed,Time.deltaTime*animTurnSpeed);

        animator.SetFloat("turnSpeed",animatorTurnSpeed);
    }
}
