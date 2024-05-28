using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{

    [SerializeField] AimComponent aimComponent;
    [SerializeField] float damage=15f;

    [SerializeField] ParticleSystem bulletVfx;
    public override void Attack()
    {
      GameObject target=aimComponent.GetAimTarget(out Vector3 aimDir);
      if(target !=null){
        DamageGameObject(target,damage);
      }
      bulletVfx.transform.rotation=Quaternion.LookRotation(aimDir);
      bulletVfx.Emit(bulletVfx.emission.GetBurst(0).maxCount);
      WeaponPlayAudio();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
