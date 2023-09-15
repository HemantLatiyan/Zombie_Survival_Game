using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
  [Header("Camera to Assign")]
  public GameObject AimCam;
  public GameObject AimCanvas;
  public GameObject ThirdPersonCam;
  public GameObject ThirdPersonCanvas;

  [Header("Camera animator")]
  public Animator animator; 

  public void Update(){
    if(Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){

        animator.SetBool("Idle" , false);
        animator.SetBool("IdleAim" , true);
        animator.SetBool("RifleWalk" , true);
        animator.SetBool("Walk" , true);

        ThirdPersonCam.SetActive(false);
        ThirdPersonCanvas.SetActive(false);
        AimCanvas.SetActive(true);
        AimCam.SetActive(true);
    }
    else if(Input.GetButton("Fire2") ){
        animator.SetBool("Idle" , false);
        animator.SetBool("IdleAim" , true);
        animator.SetBool("RifleWalk" , false);
        animator.SetBool("Walk" , false);

        ThirdPersonCam.SetActive(false);
        ThirdPersonCanvas.SetActive(false);
        AimCanvas.SetActive(true);
        AimCam.SetActive(true);
    }
    else{

        animator.SetBool("Idle" , true);
        animator.SetBool("IdleAim" , false);
        animator.SetBool("RifleWalk" , false);


        ThirdPersonCam.SetActive(true);
        ThirdPersonCanvas.SetActive(true);
        AimCanvas.SetActive(false);
        AimCam.SetActive(false);
    }
  }
}
