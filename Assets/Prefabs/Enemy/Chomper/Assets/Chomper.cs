using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy 
{
    // Thành phần gây thiệt hại được gán trong Inspector
    [SerializeField] TriggerDamageComponent damageComponent;
    // Ghi đè phương thức tấn công từ lớp Enemy
    public override void attackTarget(GameObject target)
    {
        // Kích hoạt hoạt ảnh tấn công
        Animator.SetTrigger("Attack");
    }
    // Khởi tạo lớp, được gọi khi đối tượng được kích hoạt
    protected override void Start() 
    {
        base.Start(); // Gọi phương thức khởi tạo của lớp cha

        // Thiết lập giao diện đội cho thành phần gây thiệt hại
        damageComponent.SetTeamInterFace(this);
    }
    // Ghi lại sự kiện để kiểm tra và gỡ lỗi
    public void Log()
    {
        Debug.Log("EVENT working");
    }
    // Phương thức này được gọi tại điểm tấn công trong hoạt ảnh
    public void AttackPoint()
    {
        Debug.Log("AttackPoint");

        // Kích hoạt thiệt hại
        if (damageComponent)
        {
            damageComponent.SetDamageEnable(true);
        }
    }
    // Phương thức này được gọi khi tấn công kết thúc
    public void AttackEnd()
    {
        if (damageComponent)
        {
            Debug.Log("EndAttack");

            // Vô hiệu hóa thiệt hại
            damageComponent.SetDamageEnable(false);
        }
    }
}

