using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class BTTask_Log : BTNode
{
    String Message;
    public BTTask_Log(String text){
        Message=text;
    }
    protected override NodeResult Execute()
    {
        // Debug.Log(Message);
        return NodeResult.Success;
    }

}
