using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : BTNode
{   
    //Design pattern Decorator 
    /*Đôi khi chúng ta cần mở rộng một phương thức trong đối tượng, và cách thông thường là chúng ta sẽ kế thừa đối tượng đó
    Việc này không phải sai, nhưng trong một vài trường hợp sẽ làm cho mã nguồn trở lên phức tạp hơn chúng ta mong muốn. 
    Đó là lý do chính cho việc ra đời của mẫu thiết kế Decorator, là một cách để mở rộng các phương thức một cách linh động.
    */
    
    BTNode child;

    protected BTNode getChild(){
        return child;
    }
    public Decorator(BTNode child){
        this.child =child;
    }
    public override void SortPiority(ref int priorityConter)
    {
        base.SortPiority(ref priorityConter);
        child.SortPiority(ref priorityConter);
    }

    public override BTNode Get()
    {
        return child.Get();
    }
    public override void Initialize()
    {
        base.Initialize();
        child.Initialize();
    }

    
}
