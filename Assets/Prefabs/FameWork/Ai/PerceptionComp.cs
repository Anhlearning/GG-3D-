using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComp : MonoBehaviour
{
    [SerializeField] SenseComp[] senses;

    LinkedList<PerceptionStimuli>currentlyPerceiveddStimulis=new LinkedList<PerceptionStimuli>();
    //vấn đề cần giải quyết làm sao để thêm vào các sensecomp mà thứ tự thêm vào dựa trên các kích thích lần lượt 
    // ví dụ : kích thích s1 được thêm vào đầu thì nó sẽ được giữ đến khi kết thúc , và nếu khi đang được kích thích nó gặp kt2 thì kt2 sẽ được thêm vào sau đó , tương tự với kt5 kt6..
    //-> solution: LinkedList<> chúng ta sẽ thêm node này vào node trước 

    PerceptionStimuli targetStimuli;
    public delegate void OnPerceptionTargetChanged(GameObject target,bool sensed);
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;
    void Start()
    {
        foreach(SenseComp sense in senses){
            sense.onPerceptionUpdate+=SenseUpdate;
        }
    }
    public void SenseUpdate(PerceptionStimuli stimuli,bool succesfullySensed){
        var nodeFound=currentlyPerceiveddStimulis.Find(stimuli);
        if(succesfullySensed){
            if(nodeFound!=null){
                currentlyPerceiveddStimulis.AddAfter(nodeFound,stimuli);
            }
            else{
                currentlyPerceiveddStimulis.AddLast(stimuli);
            }
        }
        else{
            currentlyPerceiveddStimulis.Remove(nodeFound);
        }

        if(currentlyPerceiveddStimulis.Count!=0){
            PerceptionStimuli highestStimuli=currentlyPerceiveddStimulis.First.Value;

            if(targetStimuli==null || targetStimuli!=highestStimuli){
                targetStimuli=highestStimuli;
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject,true);
            }
        }
        else{
            if(targetStimuli!=null){
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject,false);
                targetStimuli=null;
            }
        }
    }

}
