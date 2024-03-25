using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class PatrollingComponent : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    int currentPatrolpointIdx=-1;
    public bool GetNextPatrolPoint(out UnityEngine.Vector3 point){
        point=UnityEngine.Vector3.zero;
        if(patrolPoints.Length==0){
            return false;
        }
        currentPatrolpointIdx=(currentPatrolpointIdx+1) % patrolPoints.Length;
        point=patrolPoints[currentPatrolpointIdx].position;
        return true;
    }
}
