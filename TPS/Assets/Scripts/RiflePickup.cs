using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickup : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject PlayerRifle;
    public GameObject PickupRifle;
    public PlayerPunch playerPunch;
    public GameObject rifleUI;

    [Header("Rife Assign Things")]
    public PlayerScripts player;
    private float radius = 2.5f;
    private float nextTimeToPunch = 0f;
    public float punchCharge = 15f;
    public Animator animator;


    private void Awake(){
        PlayerRifle.SetActive(false);
        rifleUI.SetActive(false);
    }

    private void Update(){

        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToPunch){
            animator.SetBool("Punch" , true);
            animator.SetBool("Idle" , false);
            nextTimeToPunch = Time.time + 1f/punchCharge;
            
            playerPunch.Punch();
        }
        else{
            animator.SetBool("Punch" , false);
            animator.SetBool("Idle" , true);
        }


        if(Vector3.Distance(transform.position, player.transform.position) < radius){
            if(Input.GetKeyDown("f")){

               
                PlayerRifle.SetActive(true);
                PickupRifle.SetActive(false);

                ObjectivesComplete.occurrence.GetObjectivesDone1(true);
            }
        }
    }
}
