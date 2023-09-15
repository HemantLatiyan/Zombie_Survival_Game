using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheels colliders")]
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    [Header("Wheels Transforms")]
    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform vehicleDoor;

    [Header("Vehicle Engine")]
    public float accelerationForce = 100f;
    public float presentAcceleration = 0f;
    public float braskingForce = 200f;
    private float presentBreakFroce = 0f;

    [Header("Vehicle Streeing")]
    public float wheelsTorgue = 20f;
    private float presentTurnAngle = 0f;

    [Header("Vehicle Security")]
    public PlayerScripts player;
    private float radius = 8f;
    private bool isOpened = false;

    [Header("Disable Things")]
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;
    public GameObject PlayerCharacter;
    public GameObject objective3;
    public GameObject carCanvas;
    public GameObject RifleUI;
    public GameObject RiflePicked;
    public GameObject playerInstructions;
    public Camera Main;

    [Header("Vehicle Hit Var")]
    public Camera cam;
    public float hitRange = 2f;
    public float giveDamageOf = 200f;
    public GameObject DestroyEffect;
    public GameObject goreEffect;


    public void Update(){
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if(Input.GetKeyDown(KeyCode.F) && objective3.activeSelf){
                Main.enabled = true;
                isOpened = true;
                radius = 5000f;
                ObjectivesComplete.occurrence.GetObjectivesDone3(true);
            }
            else if(Input.GetKeyDown(KeyCode.G)){
                player.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 8f;
            }
        }
        if(isOpened == true){
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
            PlayerCharacter.SetActive(false);
            RifleUI.SetActive(false);
            playerInstructions.SetActive(false);
            carCanvas.SetActive(true);
            
            
            ResetPosition();
            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
            HitZombies();
        }
        else if(isOpened == false){
            carCanvas.SetActive(false);
            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
            PlayerCharacter.SetActive(true);
            if(RiflePicked.activeSelf == false){
                RifleUI.SetActive(true); 
            }   
            makeCarStatic();
        }
        
    }

    void makeCarStatic(){
        frontRightWheelCollider.brakeTorque =   Mathf.Infinity;
        frontLeftWheelCollider.brakeTorque =   Mathf.Infinity;
        backLeftWheelCollider.brakeTorque =   Mathf.Infinity;
        backRightWheelCollider.brakeTorque =   Mathf.Infinity;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void ResetPosition(){
        if(Input.GetKeyDown(KeyCode.Y)){
            transform.SetPositionAndRotation(new Vector3(433.239f, 20.5f, 410.3443f), Quaternion.Euler(new Vector3(0, -60.687f , 0)));
            makeCarStatic();
        }
        else{
            GetComponent<Rigidbody>().isKinematic = false;
        }   
    }

    void MoveVehicle(){
        frontRightWheelCollider.motorTorque = presentAcceleration*3f;
        frontLeftWheelCollider.motorTorque = presentAcceleration*3f;
        backLeftWheelCollider.motorTorque = presentAcceleration*3f;
        backRightWheelCollider.motorTorque = presentAcceleration*3f;

        presentAcceleration = accelerationForce * Input.GetAxis("Vertical");
    }

    void VehicleSteering(){
        presentTurnAngle = wheelsTorgue * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;
        SteeringWheels(frontLeftWheelCollider , frontLeftWheelTransform);
        SteeringWheels(frontRightWheelCollider , frontRightWheelTransform);
        SteeringWheels(backLeftWheelCollider , backLeftWheelTransform);
        SteeringWheels(backRightWheelCollider , backRightWheelTransform);
    }

    void SteeringWheels(WheelCollider WC, Transform WT){

        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;
    }

    void ApplyBreaks(){
        if(Input.GetKey(KeyCode.Space))
            presentBreakFroce = braskingForce;
        else
            presentBreakFroce = 0;
        
        frontRightWheelCollider.brakeTorque = presentBreakFroce;
        frontLeftWheelCollider.brakeTorque = presentBreakFroce;
        backLeftWheelCollider.brakeTorque = presentBreakFroce;
        backRightWheelCollider.brakeTorque = presentBreakFroce;

        
    }

    void HitZombies(){
        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position , cam.transform.forward, out hitInfo , hitRange)){
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie zombie1 = hitInfo.transform.GetComponent<Zombie>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if(objectToHit != null){
                objectToHit.getpoint(hitInfo.point);
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(DestroyEffect , hitInfo.point , Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }

            else if(zombie1 != null){
                zombie1.zombieHitDamage(giveDamageOf);
                zombie1.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect , hitInfo.point , Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }

            else if(zombie2 != null){   
                zombie2.zombieHitDamage(giveDamageOf);
                zombie2.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect , hitInfo.point , Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
    }

}
