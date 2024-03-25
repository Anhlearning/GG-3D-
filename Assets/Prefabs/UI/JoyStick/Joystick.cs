using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Joystick : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public delegate void OnStickInputValueUpdate(Vector2 inputVal);
    public delegate void OnstickTaped();

    public event OnStickInputValueUpdate onStickValueUpdate;
    public event OnstickTaped onstickTaped;
    [SerializeField] private RectTransform ThumbStickTrans;
    [SerializeField] private RectTransform BackGroundTrans;

    [SerializeField] private RectTransform CenterTrans;

    bool bWasDragging;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 TouchPos=eventData.position;
        Vector2 CenterPos=BackGroundTrans.position;

        Vector2 LocalOffset=Vector2.ClampMagnitude(TouchPos-CenterPos,BackGroundTrans.sizeDelta.x/2);
        
        Vector2 inputVal=LocalOffset/(BackGroundTrans.sizeDelta.x/2);
        
        ThumbStickTrans.position=CenterPos+LocalOffset;
        onStickValueUpdate?.Invoke(inputVal);
        bWasDragging=true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ThumbStickTrans.position=eventData.position;
        BackGroundTrans.position=eventData.position;
        bWasDragging=false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        BackGroundTrans.position=CenterTrans.position;
        ThumbStickTrans.position=BackGroundTrans.position;
        onStickValueUpdate?.Invoke(Vector2.zero);
        if(!bWasDragging){
            onstickTaped?.Invoke();
        }
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
