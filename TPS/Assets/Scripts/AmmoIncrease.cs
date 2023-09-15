using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoIncrease : MonoBehaviour
{
    [Header("Ammo Increase")]
    public Rifle rifle;
    private int magToGive = 15;
    private float radius = 2f;
    private bool updated = false;


    [Header("Sounds")]
    public AudioClip AmmoBoostSound;
    public AudioSource audioSource;

    [Header("HealthBox Animator")]
    public Animator animator;

    public void Update(){

        if(Vector3.Distance(transform.position, rifle.transform.position) < radius){
            if(Input.GetKeyDown("f")){
                if(rifle.rifleUI.activeSelf){
                    if(updated == false){
                        animator.SetBool("Open" , true);
                        rifle.mag += magToGive;

                        audioSource.PlayOneShot(AmmoBoostSound);
                        AmmoCount.occurrence.UpdateMagText(rifle.mag);
                        Object.Destroy(gameObject, 1.5f);
                        updated = true;
                    }
                }
                else{
                    Debug.Log("NOT EQUIPPED");
                }
                
            }
        }
    }
}
