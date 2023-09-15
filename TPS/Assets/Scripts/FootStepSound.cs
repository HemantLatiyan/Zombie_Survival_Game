using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("FoorSteps Sources")]
    public AudioClip[] footstepsSound;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private AudioClip GetRandomFootStep(){
        return footstepsSound[UnityEngine.Random.Range(0 , footstepsSound.Length)];
    }

    private void Steps(){
        AudioClip clip = GetRandomFootStep();
        audioSource.PlayOneShot(clip);
    }
}
