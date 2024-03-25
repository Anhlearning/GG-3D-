using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard 
{
    // blackboard là nơi lưu trữ các dữ liệu mà object được gán script có thể nhận biết như là kẻ thù , vector truyền vào ... 
    Dictionary<string,object>blackBoardData=new Dictionary<string, object>();

    public delegate void OnBlackboardValueChange(string key,object val);

    public event OnBlackboardValueChange onBlackBroadValueChange;
    public void SetOrAddData(String key , object val){
        if(blackBoardData.ContainsKey(key)){
            blackBoardData[key]=val;
        }
        else {
            blackBoardData.Add(key,val);
        }
        onBlackBroadValueChange?.Invoke(key,val);
    }   
    public bool GetBlackboardData<T>(string key,out T val){
       val=default(T);
       if(blackBoardData.ContainsKey(key)){
            val=(T)blackBoardData[key];
            return true;
       } 
       return false;
    }

    // public bool HasKey(string key){
    //     return blackBoardData.ContainsKey(key);
    // }

    public void RemoveBlackBoardData(string key){
        blackBoardData.Remove(key);
       onBlackBroadValueChange?.Invoke(key,null);
    }
}
