using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnComponent : MonoBehaviour
{
    // Các đối tượng có thể được sinh ra, được gán trong Inspector
    [SerializeField] GameObject[] ObjectToSpawn;   
    Animator animator;    
    [SerializeField] Transform spawnTransform;

    [Header("Audio Clip")]
    [SerializeField]AudioClip SpawnAudio;
    [SerializeField]float volume=1f;
    // Thành phần Animator để kiểm soát hoạt ảnh sinh
    // Điểm sinh, được gán trong Inspector
    // Phương thức khởi tạo, được gọi khi đối tượng được kích hoạt
    private void Start() 
    {
        // Lấy thành phần Animator từ đối tượng
        animator = GetComponent<Animator>(); 
    }
    // Phương thức bắt đầu sinh đối tượng
    public bool StartSpawn()
    {
        // Kiểm tra xem danh sách đối tượng có đối tượng nào không
        if (ObjectToSpawn.Length == 0)
        {
            return false; // Trả về thất bại nếu không có đối tượng nào
        }
        // Nếu có Animator, kích hoạt hoạt ảnh sinh
        if (animator != null)
        {
            Debug.Log("NGON");
            animator.SetTrigger("Spawn");
        }
        else 
        {
            // Nếu không có Animator, thực hiện sinh trực tiếp
            SpawnImpl();
        }
        Vector3 spawnAudioLoc=transform.position;
        GamePlayStatic.PlayAudioAtLoc(SpawnAudio,spawnAudioLoc,volume);
        return true; // Trả về thành công sau khi bắt đầu sinh
    }
    // Phương thức thực hiện sinh đối tượng
    public void SpawnImpl()
    {
        // Chọn ngẫu nhiên một đối tượng từ danh sách
        int RandomPick = Random.Range(0, ObjectToSpawn.Length);
        // Tạo đối tượng tại vị trí và hướng của điểm sinh
        GameObject newSpawn = Instantiate(ObjectToSpawn[RandomPick], spawnTransform.position, spawnTransform.rotation);
        // Kiểm tra và gọi phương thức SpawBy nếu đối tượng có giao diện ISpawnInterface
        ISpawnInterface newSpawnInterface = newSpawn.GetComponent<ISpawnInterface>();
        if (newSpawnInterface != null)
        {
            newSpawnInterface.SpawBy(gameObject);
        }
    }
}

