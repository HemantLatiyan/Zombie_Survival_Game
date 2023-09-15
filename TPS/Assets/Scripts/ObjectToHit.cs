using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHit : MonoBehaviour
{
    public float ObjectHealth = 100f;
    public GameObject original;
    public GameObject destroyed;
    public Vector3 lastpoint;
    public bool done = false;

    public void getpoint(Vector3 point){
        lastpoint = point;
    }

    public void ObjectHitDamage(float amount){
        ObjectHealth -= amount;
        if(ObjectHealth <= 0f){
            if(done == false){
                Die();
            }
            done = true;
        }
    }

    void Die(){
        Destroy(original);
        StartCoroutine(remove());
    }

    IEnumerator remove(){
        destroyed.SetActive(true);
        GameObject children = Instantiate(destroyed , destroyed.transform.position , destroyed.transform.rotation);

        foreach(Transform child in children.transform){
            GameObject c = child.gameObject;
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childrigidbody)){
                childrigidbody.AddExplosionForce(20f , lastpoint , 1f);
            }
        }
        
        yield return new WaitForSeconds(1.7f);
        Destroy(children);
        Destroy(destroyed);

    }
}
