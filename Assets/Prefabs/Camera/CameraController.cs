using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField]Transform followTrans;
   [SerializeField] float turnspeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate() {
        transform.position=followTrans.position;
    }
    public void AddYawInput(float amt){
        transform.Rotate(Vector3.up,amt*Time.deltaTime*turnspeed);
    }
}
