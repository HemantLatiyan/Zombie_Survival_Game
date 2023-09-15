using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 3f;

    [Header("Player Health Things")]
    private float playerHealth = 120f;
    public float presentHealth;
    public GameObject playerDamage;
    public HealthBar healthBar;

    [Header("Player Script Cameras")] 
    public Transform playerCamera;
    public GameObject EndGameMenuUI;

    [Header("Player Animator and Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player Instruction")]
    public GameObject Instructions;


    [Header("Player Jumping and velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        healthBar.GiveFullHealth(playerHealth);
    }

    private void Update(){
        Instructions.SetActive(true);
        changeplayerAngle();
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance , surfaceMask);
        if(onSurface && velocity.y < 0){
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
        playerMove();

        Jump();
        Sprint();
    }

    void playerMove(){
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f , vertical_axis).normalized;
        if(direction.magnitude >= 0.1f){

            animator.SetBool("Idle" , false);
            animator.SetBool("Walk" , true);
            animator.SetBool("Running" , false);
            animator.SetBool("RifleWalk" , false);
            animator.SetBool("IdleAim" , false);
            float targetAngle = Mathf.Atan2(direction.x , direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle, ref turnCalmVelocity,  turnCalmTime);
            // transform.rotation = Quaternion.Euler(0f , angle , 0f);

            Vector3 moveDirection = Quaternion.Euler(0f , angle , 0f) * direction;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else{
            animator.SetBool("Idle" , true);
            animator.SetBool("Walk" , false);
            animator.SetBool("Running" , false);
        }
    }

    void Jump(){
        if(Input.GetButtonDown("Jump") && onSurface){

            animator.SetBool("Idle" , false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else{
            animator.SetBool("Idle" , true);
            animator.ResetTrigger("Jump");
        }
    }

    public void changeplayerAngle(){
        transform.rotation = Quaternion.Euler(0f , playerCamera.eulerAngles.y , 0f);
    }

    void Sprint(){
        if(Input.GetButton("Sprint") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))){
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f , vertical_axis).normalized;
            if(direction.magnitude >= 0.1f){

                animator.SetBool("Walk" , false);
                animator.SetBool("Running" , true);

                float targetAngle = Mathf.Atan2(direction.x , direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle, ref turnCalmVelocity,  turnCalmTime);
                // transform.rotation = Quaternion.Euler(0f , angle , 0f);
                
                Vector3 moveDirection = Quaternion.Euler(0f , targetAngle , 0f) * direction;
                cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else{
                animator.SetBool("Walk" , true);
                animator.SetBool("Running" , false);
            }
        }
    }

    public void playerHitDamage(float takeDamage){
        presentHealth -= takeDamage;
        StartCoroutine(PlayerDamage());

        healthBar.SetHealth(presentHealth);
        if(presentHealth <= 0){
            PlayerDie();
        }
    }

    private void PlayerDie(){
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject , 0.5f);
        EndGameMenuUI.SetActive(true);
    }

    IEnumerator PlayerDamage(){
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerDamage.SetActive(false);

    }
}
