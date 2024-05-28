using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[CreateAssetMenu(menuName ="Audio/UiAudioPlaye")]
public class UiAudioPlayer : ScriptableObject {
    [SerializeField] AudioClip ClickAudioClicp;
    [SerializeField] AudioClip ComitAudioClip;
    [SerializeField] AudioClip SelectAudioClip;
    [SerializeField] AudioClip WinAudioClip;


    public void PlayClick(){
        PlayAudio(ClickAudioClicp);
    }
    public void PlayCommit(){
        PlayAudio(ComitAudioClip);
    }
    public void PlaySelect(){
        PlayAudio(SelectAudioClip);
    }
    internal void PlayWin(){
        PlayAudio(WinAudioClip);
    }
    public void PlayAudio(AudioClip audiotoPlay){
        Camera.main.GetComponent<AudioSource>().PlayOneShot(audiotoPlay);
    }

}
