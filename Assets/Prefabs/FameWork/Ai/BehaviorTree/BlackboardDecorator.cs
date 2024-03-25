using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class BlackboardDecorator : Decorator
{
    //ngan cac dieu kien chay trong blackboard
    public enum RunCondition{
        KeyExists,
        KeyNotExists,
    }
    //enum thong bao nhung thay doi 
    public enum NotifyRule{
        RunConditionChange,
        KeyValueChange
    }
    //enum thong bao huy bo 
    public enum NotifiAbort{
        none,
        self,
        lower,
        both
    }
    string key;
    object value;
    BehaviorTree tree;
    RunCondition runCondition;
    NotifyRule notifyRule;
    NotifiAbort notifiAbort;

    /*Enum Runcondition thì có 2 điều kiện 1 là có khóa , 2 là không tồn tại khóa*/
    //NotifyRule thì có 2 đièu kiện là 1 là thay đổi điều kiện , 2 là nó thay đổi khóa 
    // Notifiabort thì nó sẽ hủy các Node con thì có 4 loại:none , chính nó , con thấp hơn , cả hai (dựa trên priority )

    public BlackboardDecorator(BehaviorTree tree,
    BTNode child,
    string key,
    RunCondition runCondition,
    NotifyRule notifyRule,
    NotifiAbort notifiAbort) : base(child)
    {
        this.runCondition=runCondition;
        this.notifiAbort=notifiAbort;
        this.notifyRule=notifyRule;
        this.key=key;
        this.tree=tree;
    }
    protected override NodeResult Execute()
    {
        //Lấy ra cái blackBoard chứa key , value 
        // sau đó nó hủy đăng ký sử kiện check Notifi ( hủy đăng ký trước đó nếu có )
        // sau đó thêm đăng ký vào 
        BlackBoard blackBoard =tree.BlackBoard;
        if(blackBoard ==null){
            return NodeResult.Failure;
        }
        blackBoard.onBlackBroadValueChange-=checkNotify;


        blackBoard.onBlackBroadValueChange+=checkNotify;
        if(checkRunCondition()){
            return NodeResult.Inprogress;
        }
        else {
            return NodeResult.Failure;
        }
    }
    public bool checkRunCondition(){
        bool exists=tree.BlackBoard.GetBlackboardData(key,out value);
        switch(runCondition){
            case RunCondition.KeyExists:
                return exists;
            case RunCondition.KeyNotExists:
                return !exists;
        }
        return false;
    }
    public void checkNotify(string key,object val){
        //nếu mà key gửi vào khác với key khởi tạo thì return 
        /* Nếu khởi tạo nitifyRule = RunconditionChange thì kiểm tra val nếu key value khởi tạo khác với
        key value được đăng ký thì sẽ gọi đến hàm Notify*/
        if(this.key!= key){
            return ;
        }
        if(notifyRule == NotifyRule.RunConditionChange){
            bool prevExists= value !=null;
            bool currentExists=val !=null;
            if(prevExists !=currentExists){
                Notify();
            }
        }
        // nếu mà notifyrule=keyvaluechange mà 2 key key nó khác nhau thì ham notify được gọi 
        else if (notifyRule == NotifyRule.KeyValueChange){
            if(value!=val){
                Notify();
            }
        }
    }
    private void Notify(){
        switch (notifiAbort){
            case NotifiAbort.none:
                break;
            case NotifiAbort.self:
                AbortSelf();
                break;
            case NotifiAbort.lower:
                AbortLower();
                break;
            case NotifiAbort.both:
                AbortBoth();
                break;
        
        }
    }
    private void AbortSelf(){
        Abort();
    }
    private void AbortLower(){
        tree.AbortLowerThan(GetPriority());
    }
    private void AbortBoth(){
        Abort();
        AbortLower();
    }

    protected override NodeResult Update()
    {
        return getChild().UpdateNode();
    }
    protected override void End()
    {   
        getChild().Abort();
        base.End();
    }
}
