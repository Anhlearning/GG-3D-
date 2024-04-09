using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseComp : MonoBehaviour
{
    [SerializeField] float forgettingTime;
    static List<PerceptionStimuli>registeredStimulis=new List<PerceptionStimuli>();
    private List<PerceptionStimuli>PerceivableStimulis=new List<PerceptionStimuli>();

    Dictionary<PerceptionStimuli,Coroutine>ForgettingRoutines=new Dictionary<PerceptionStimuli, Coroutine>();
    public delegate void OnPerceptionUpdate(PerceptionStimuli stimuli,bool succesfullySensed);

    public event OnPerceptionUpdate onPerceptionUpdate;
    static public void RegisterStimuli(PerceptionStimuli stimuli){
        if(registeredStimulis.Contains(stimuli)){
            return ;
        }
        registeredStimulis.Add(stimuli);
    }

    static public void UnRegisterStimuli(PerceptionStimuli stimuli){
        registeredStimulis.Remove(stimuli);
    }

    protected abstract bool InStimuliSenable(PerceptionStimuli stimuli);

    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        foreach(var stimuli in registeredStimulis){
            if(InStimuliSenable(stimuli)){
                if(!PerceivableStimulis.Contains(stimuli)){
                    // nếu mà kích thích xuất hiện trong bán kính nhưng vẫn chưa có trong danh sách hiện tại (kích thích đã được nằm trong danh sách đăng kí ) thì cho vào danh sách hiện tại  
                    /* nếu mà kích thích nằm trong danh sách forgetting routines thì nó mới thoát ra khỏi vùng bán kính nhưng chưa quên sau đó đi vào bán kính 1 lần nữa thì khi đó ta sẽ xóa nó khỏi danh sách 
                    routine , stop routines*/
                    PerceivableStimulis.Add(stimuli);
                    if(ForgettingRoutines.TryGetValue(stimuli,out Coroutine routine)){
                        StopCoroutine(routine);
                        ForgettingRoutines.Remove(stimuli);
                    }
                    // nếu không nằm trong danh sách routine có nghĩa là nó đã quên đi cái kích thích này và kích thích này vào vùng bán kính thì khi đó chúng ta update nó bằng event OnperceptionUpdate;
                    else{
                        onPerceptionUpdate?.Invoke(stimuli,true);
                    }
                }
            }
            else{
                if(PerceivableStimulis.Contains(stimuli)){
                    //khi mà kích thích không nằm trong bán kính , nhưng vẫn có trong danh sách hiện tại ==> nó vào bán kính rồi thoát ra khi đó ta phải xóa nó khỏi ds hiện tại 
                    PerceivableStimulis.Remove(stimuli);
                    //mình sẽ dùng 1 cái IEnumrator để xem trong vòng .. seconds liệu kích thích đã rời khỏi chưa , và thêm nó vào danh sách forget 
                    ForgettingRoutines.Add(stimuli,StartCoroutine(ForgetStimuli(stimuli)));
                }
            }
        }
    }
    IEnumerator ForgetStimuli(PerceptionStimuli stimuli){
        yield return new WaitForSeconds(forgettingTime);
        ForgettingRoutines.Remove(stimuli);
        onPerceptionUpdate?.Invoke(stimuli,false);
    }
    protected virtual void DrawDebug(){

    }
    internal void AssignPercievedStimuli(PerceptionStimuli targetStimuli){
        PerceivableStimulis.Add(targetStimuli);
        onPerceptionUpdate?.Invoke(targetStimuli,true);

        if(ForgettingRoutines.TryGetValue(targetStimuli,out Coroutine forgetCoroutine)){
            StopCoroutine(forgetCoroutine);
            ForgettingRoutines.Remove(targetStimuli);

        }
    }

    private void OnDrawGizmos() {
        DrawDebug();
    }
}
