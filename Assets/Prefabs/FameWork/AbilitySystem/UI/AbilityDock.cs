using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityDock : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    // Start is called before the first frame update
    [SerializeField] AbilityComponent abilityComponent;
    [SerializeField] RectTransform Root;
    [SerializeField] VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] AbilityUi AbilityUiPrefabs;
    [SerializeField] float ScaleRange=200;
    [SerializeField] float hightlightSize=1.5f;
    [SerializeField] float ScaleSpeed=20f;
    List<AbilityUi>AbilityUis=new List<AbilityUi>();
    Vector3 GoalScale = Vector3.one;
    PointerEventData touchData;

    AbilityUi hightlightAbility;
    private void Awake() {
        abilityComponent.onNewAbilityAdded+=AddAbility;
    }
    private void AddAbility(Ability newAbility){
        AbilityUi newAbilityUI=Instantiate(AbilityUiPrefabs,Root);
        newAbilityUI.Init(newAbility);
        AbilityUis.Add(newAbilityUI);
    }
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(touchData !=null) {
             GetUIUnderPointer(touchData,out hightlightAbility);
             ArrangeScale(touchData);
        }
        transform.localScale=Vector3.Lerp(transform.localScale,GoalScale,Time.deltaTime*ScaleSpeed);
    }
    public void ArrangeScale(PointerEventData touchData){

        if(ScaleRange ==0) return ;

        float touchVerticalPos=touchData.position.y;
        foreach(AbilityUi abilityUI in AbilityUis){
           float abilityUIVerticalPos=abilityUI.transform.position.y;
           float distance =MathF.Abs(touchVerticalPos-abilityUIVerticalPos);
            if(distance > ScaleRange){
                abilityUI.SetScaleAmt(0);
                continue;
            }
            float scaleAmt=(ScaleRange-distance)/ScaleRange;
            abilityUI.SetScaleAmt(scaleAmt);
        }
    }
    public void OnPointerDown(PointerEventData eventData){
            touchData=eventData;
            GoalScale=Vector3.one*hightlightSize;
    }
    public void OnPointerUp(PointerEventData eventData){
        if(hightlightAbility){
            hightlightAbility.ActivateAbility();
        }
        touchData =null;
        ResetScale();
        GoalScale=Vector3.one;
    }   
    public void ResetScale(){
        foreach(AbilityUi abilityUI in AbilityUis){
            abilityUI.SetScaleAmt(0f);
        }
    }

    bool GetUIUnderPointer(PointerEventData eventData, out AbilityUi abilityUi){
        List<RaycastResult> findAbility=new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData,findAbility);

        abilityUi=null;
        foreach(RaycastResult resutl in findAbility){
            abilityUi=resutl.gameObject.GetComponentInParent<AbilityUi>();
            if(abilityUi !=null){
                return true;
            }
        }
        return false;
    }
}
