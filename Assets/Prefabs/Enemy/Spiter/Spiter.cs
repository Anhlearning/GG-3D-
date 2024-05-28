using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiter : Enemy
{
    // Mẫu đạn được gán trong Inspector
    [SerializeField] Projectile projectilePrefab;

    // Điểm bắn được gán trong Inspector
    [SerializeField] Transform launchPoint;

    // Vị trí đích của đạn
    Vector3 Destination;

    // Ghi đè phương thức tấn công từ lớp Enemy
    public override void attackTarget(GameObject target)
    {
        // Kích hoạt hoạt ảnh tấn công
        Animator.SetTrigger("Attack");

        // Lưu trữ vị trí của mục tiêu
        Destination = target.transform.position;
    }

    // Phương thức bắn đạn
    public void Shoot()
    {
        Debug.Log("Spiter Shooting");

        // Tạo đạn mới tại vị trí và hướng của điểm bắn
        Projectile newProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

        // Bắn đạn về phía vị trí đích
        newProjectile.Launch(gameObject, Destination);
    }
}
