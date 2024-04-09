using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
   [SerializeField] GameObject[] ObjectToSpawn;
   Animator animator;
   [SerializeField] Transform spawnTransform;
   private void Start() {
        animator=GetComponent<Animator>(); 
   }
   public bool StartSpawn(){
        if(ObjectToSpawn.Length==0){
            return false;
        }
        if(animator !=null){
            Debug.Log("NGON");
            animator.SetTrigger("Spawn");
        }
        else {
            Debug.Log($"{animator.gameObject}== null");
            SpawnImpl();
        }
        return true;
   }
   public void SpawnImpl(){
        int RandomPick=Random.Range(0,ObjectToSpawn.Length);
        GameObject newSpawn=Instantiate(ObjectToSpawn[RandomPick],spawnTransform.position,spawnTransform.rotation);
        ISpawnInterface newSpawnInterface=newSpawn.GetComponent<ISpawnInterface>();
        if(newSpawnInterface !=null){
            newSpawnInterface.SpawBy(gameObject);
        }
   }
}
