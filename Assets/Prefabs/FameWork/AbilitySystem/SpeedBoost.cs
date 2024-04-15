using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="Ability/SpeedBoost")]
public class SpeedBoost : Ability
{
    [SerializeField] float boostAmt=20f;
    [SerializeField] float boostDuration=2f;

    Player player;
    public override void ActivateAbility()
    {
        if(!CommitAbility()){
            return ;
        }
        player = AbilityComp.GetComponent<Player>();
        player.AddMoveSpeed(boostAmt);
        AbilityComp.StartCoroutine(ResetSpeed());
    }

    IEnumerator ResetSpeed(){
        yield return new WaitForSeconds(boostDuration);
        player.AddMoveSpeed(-boostAmt);
    }
}
