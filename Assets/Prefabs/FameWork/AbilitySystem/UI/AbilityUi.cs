using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

using Image =UnityEngine.UI.Image;
public class AbilityUi : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image AbilityIcon;
    [SerializeField] Image CooldownWheel;

    [SerializeField] float hightlightSize=1.5f;
    [SerializeField] float hightOffset=200f;
    [SerializeField] float ScaleSpeed=20f;
    [SerializeField] RectTransform Offsetpivot;
    Vector3 GoalScale =Vector3.one;
    Vector3 GoalOffset=Vector3.zero;
    bool IsonCoolDown=false;
    float CoolDownCounter=0;

    public void SetScaleAmt(float amt){
        GoalScale=Vector3.one*(1+(hightlightSize-1)*amt);
        GoalOffset=Vector3.left*hightOffset*amt;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale=Vector3.Lerp(transform.localScale,GoalScale,Time.deltaTime*ScaleSpeed);
        Offsetpivot.localPosition=Vector3.Lerp(Offsetpivot.localPosition,GoalOffset,Time.deltaTime*ScaleSpeed);
    }
    public void Init(Ability newAbility){
        ability=newAbility;
        AbilityIcon.sprite=ability.GetAbilityIcon();
        CooldownWheel.enabled=false;
        ability.onCoolDownStarted+=StartCoolDown;
    }
    public void StartCoolDown(){
        if(IsonCoolDown) return ;

        StartCoroutine(CoolDownOnCoroutine());
    }
    public void ActivateAbility(){
        ability.ActivateAbility();
    }

    IEnumerator CoolDownOnCoroutine(){
        IsonCoolDown=false;
        CoolDownCounter=ability.getCoolDownDuration();
        float coolDownDutaion = CoolDownCounter;
        CooldownWheel.enabled=true;
        while(CoolDownCounter >0){
            CoolDownCounter-=Time.deltaTime;
            CooldownWheel.fillAmount=CoolDownCounter/coolDownDutaion;
            yield return new WaitForEndOfFrame();
        }
        IsonCoolDown=false;
        CooldownWheel.enabled=false;
    }
}
