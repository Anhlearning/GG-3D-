using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy 
{
    public override void attackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
    }
}
