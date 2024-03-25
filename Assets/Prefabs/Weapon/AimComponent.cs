using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] float aimRange=1000f;
    [SerializeField] LayerMask aimMask;

    public GameObject GetAimTarget(out Vector3 aimDir){
        Vector3 muzzlePos= muzzle.position;
        aimDir=GetAimDir();
        //lấy giá trị getaim dir gán vào aimdir
        if(Physics.Raycast(muzzlePos,GetAimDir(),out RaycastHit hitInfo,aimRange,aimMask)){
            // hàm check va chạm 
            return hitInfo.collider.gameObject;
        }
        return null;
    }
    public Vector3 GetAimDir(){
        //chuẩn hóa vector thành 2 hướng x , z
        Vector3 muzzleDir= muzzle.forward;
        return new Vector3(muzzleDir.x,0f,muzzleDir.z).normalized;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(muzzle.position,muzzle.position + GetAimDir() * aimRange);
    }
}
