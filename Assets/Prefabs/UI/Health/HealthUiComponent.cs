using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUiComponent : MonoBehaviour
{
    [SerializeField] HealthBar healthBarToSpawn;

    [SerializeField] Transform healthbarAttachPoint;
    [SerializeField] HealthComponents healthComponents;
    
    private void Start()
    {
        IngameUi ingameUI=FindAnyObjectByType<IngameUi>();
        HealthBar newHealthBar=Instantiate(healthBarToSpawn,ingameUI.transform);
        newHealthBar.init(healthbarAttachPoint);
        healthComponents.onHealthChange+=newHealthBar.setHealthSliderValue;
        healthComponents.onHealEmpty+=newHealthBar.OnOwnerDead;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
