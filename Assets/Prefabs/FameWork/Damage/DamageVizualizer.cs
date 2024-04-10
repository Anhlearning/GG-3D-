using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVizualizer : MonoBehaviour
{
    [SerializeField] Renderer mesh;
    [SerializeField] Color DamageColor;

    [SerializeField] float BlinkSpeed=0.1f;
    [SerializeField] string EmissionColorPropertyName="_Addition";
    [SerializeField] HealthComponents healthComponents;
    Color OrigionalEmissionColor;
    private void Start() {
        Material mat =mesh.material;
        mesh.material=new Material(mat);
        OrigionalEmissionColor=mesh.material.GetColor(EmissionColorPropertyName);
        healthComponents.onTakeDamage+=TookDamage;
    }
    protected virtual void TookDamage(float health,float delta,float maxhealth,GameObject Instigator){
        Color currentEmissionColor=mesh.material.GetColor(EmissionColorPropertyName);
        if(Mathf.Abs((currentEmissionColor-OrigionalEmissionColor).grayscale)<0.1f){
            mesh.material.SetColor(EmissionColorPropertyName,DamageColor);
        }
    }
    private void Update() {
        Color currentEmissionColor=mesh.material.GetColor(EmissionColorPropertyName);
        Color newEmissionColor=Color.Lerp(currentEmissionColor,OrigionalEmissionColor,Time.deltaTime*BlinkSpeed);
        mesh.material.SetColor(EmissionColorPropertyName,newEmissionColor);
    }

}
