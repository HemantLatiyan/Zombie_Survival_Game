using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : MonoBehaviour
{
    [Header("Health Increase")]
    public PlayerScripts player;
    private float healthToGive = 120f;
    private bool updated = false;
    private float radius = 3f;

    [Header("Sounds")]
    public AudioClip HealthBoostSound;
    public AudioSource audioSource;

    [Header("HealthBox Animator")]
    public Animator animator;

    public void Update(){

        if(Vector3.Distance(transform.position, player.transform.position) < radius){
            if(Input.GetKeyDown("f")){
                if(player.presentHealth < 120f){       
                    if(updated == false){
                        animator.SetBool("Open" , true);
                        player.presentHealth = healthToGive;
                        player.healthBar.SetHealth(player.presentHealth);
                        audioSource.PlayOneShot(HealthBoostSound);
                        Object.Destroy(gameObject, 1.5f);
                        updated = true;
                    }
                    

                }
                else{
                    Debug.Log("Full");
                }       
            }
        }
    }
}
