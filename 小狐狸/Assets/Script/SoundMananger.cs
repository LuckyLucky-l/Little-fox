using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance;
    public AudioSource audioSource, audioSourceJump;
    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, cherryAudio;

    private void Awake()
    {
        instance = this;
    }
    public void JumpAudio()
    {
        audioSourceJump.clip = jumpAudio;
        audioSourceJump.Play();
    }
    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }
    public void CherryAudio()
    {
        audioSource.clip = cherryAudio;
        audioSource.Play();
    }

}
