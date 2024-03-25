using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionStimuli : MonoBehaviour
{
   
    void Start()
    {
        SenseComp.RegisterStimuli(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        SenseComp.UnRegisterStimuli(this);   
    }

}
