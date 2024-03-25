using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Transform _attachPoint;
    [SerializeField] Slider healthSlider;

    public void setHealthSliderValue(float healt,float val,float maxHealth){
        healthSlider.value=healt/maxHealth;
    }
    public void init(Transform attachPoint)
    {
        _attachPoint=attachPoint;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    private  void Update()
    {
        UnityEngine.Vector3 ownerScreenPoint=Camera.main.WorldToScreenPoint(_attachPoint.position);
        transform.position=ownerScreenPoint;
    }

    internal void OnOwnerDead()
    {
        Destroy(gameObject);
    }
}
