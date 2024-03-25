using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
//luon thuc thi ke ca khi khong start
public class CameraArm : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float armLength;
    [SerializeField] Transform child;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        child.position=transform.position-child.forward*armLength;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(child.position,transform.position);
    }
}
