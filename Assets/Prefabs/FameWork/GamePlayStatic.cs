using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;



public  static class GamePlayStatic 
{
  class AudioSrcContext : MonoBehaviour 
  {

  }
  static private ObjectPool<AudioSource>AudioPool;

  public static void GameStarted(){
    AudioPool=new ObjectPool<AudioSource>(CreateAudioSrc,null,null,DestroyAudioSrc,false,5,10);
  }
  private static AudioSource CreateAudioSrc(){
    GameObject audioSrcGameObj= new GameObject("AudioSourceGameObj",typeof(AudioSource),typeof(AudioSrcContext));
    AudioSource audioSrc=audioSrcGameObj.GetComponent<AudioSource>();
    audioSrc.volume=1.0f;
    audioSrc.spatialBlend=1.0f;
    audioSrc.rolloffMode=AudioRolloffMode.Linear;
    return audioSrc;
  }
  private static void DestroyAudioSrc(AudioSource audioSource){
    GameObject.Destroy(audioSource.gameObject);
  }
  public static void SetGamePause(bool paused)
  {
    Time.timeScale=paused ? 0 :1;
  }
  public static void PlayAudioAtLoc(AudioClip audiotoPlay,Vector3 playLoc,float volume){
      AudioSource newSrc=AudioPool.Get();
      newSrc.volume=volume;
      newSrc.gameObject.transform.position=playLoc;
      newSrc.PlayOneShot(audiotoPlay);
      
      newSrc.GetComponent<AudioSrcContext>().StartCoroutine(releaseAudioSrc(newSrc,audiotoPlay.length));
  }
  private static IEnumerator releaseAudioSrc(AudioSource newrsc , float length){
      yield return new WaitForSeconds(length);
      AudioPool.Release(newrsc);
  }
  public static void PlayAudioAtPlayer(AudioClip abilityAudio,float volume ){
      PlayAudioAtLoc(abilityAudio,Camera.main.transform.position,volume);
  }
}
