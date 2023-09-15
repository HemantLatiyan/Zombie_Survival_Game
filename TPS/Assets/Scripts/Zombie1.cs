using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{   
    [Header("Zombie Health an dDamage")]
    private float presentHealth;
    public float giveDamage = 5f;
    private float zombieHealth = 150f;
    public HealthBar healthBar;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zombie Guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingpointRadius = 2;

    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("Zombie Animation")]
    public Animator animator;

    [Header("Zombie mood/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInattackingRadius;

    private void Awake(){
        presentHealth = zombieHealth;
        healthBar.GiveFullHealth(zombieHealth);
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if(!playerInvisionRadius && !playerInattackingRadius) Guard();
        if(playerInvisionRadius && !playerInattackingRadius) Pursueplayer();
        if(playerInvisionRadius && playerInattackingRadius) AttackPlayer();
    }

    private void Guard(){
        if(walkPoints.Length == 0) return;
        if(Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingpointRadius){
            currentZombiePosition = Random.Range(0 , walkPoints.Length);
            if(currentZombiePosition >= walkPoints.Length){
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);

        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }

    private void Pursueplayer(){
        if(zombieAgent.SetDestination(playerBody.position)){
            animator.SetBool("Walking" , false);
            animator.SetBool("Running" , true);
            animator.SetBool("Attacking" , false);
            animator.SetBool("Died" , false);
        }
        else{
            animator.SetBool("Walking" , false);
            animator.SetBool("Running" , false);
            animator.SetBool("Attacking" , false);
            animator.SetBool("Died" , true);
        }
    }

    private void AttackPlayer(){
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if(!previouslyAttack){

            RaycastHit hitInfo;
            if(Physics.Raycast(AttackingRaycastArea.transform.position, AttackingRaycastArea.transform.forward, out hitInfo, attackingRadius)){
                Debug.Log("Attacking" + hitInfo.transform.name);

                PlayerScripts playerBody = hitInfo.transform.GetComponent<PlayerScripts>();

                if(playerBody !=null){
                    playerBody.playerHitDamage(giveDamage);
                }

                animator.SetBool("Walking" , false);
                animator.SetBool("Running" , false);
                animator.SetBool("Attacking" , true);
                animator.SetBool("Died" , false);
            }

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking) , timeBtwAttack);
        }
    }

    private void ActiveAttacking(){
        previouslyAttack = false;
    }

    public void zombieHitDamage(float takeDamage){
        visionRadius = 5000f;
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        
        if(presentHealth <= 0 ){
            animator.SetBool("Walking" , false);
            animator.SetBool("Running" , false);
            animator.SetBool("Attacking" , false);
            animator.SetBool("Died" , true);
            zombieDie();
        }
    }

    private void zombieDie(){
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInattackingRadius = false;
        playerInvisionRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
