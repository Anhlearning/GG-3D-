using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BehaviorTreeInterface 
{
 public void RorateTowards(GameObject target,bool verticalAim=false);

 public void attackTarget(GameObject target);
}
