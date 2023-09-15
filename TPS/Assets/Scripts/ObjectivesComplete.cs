using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objective to Complete")]
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;
    public GameObject o1;
    public GameObject o2;
    public GameObject o3;
    public GameObject o4;
    public GameObject spotlight;

    public static ObjectivesComplete occurrence;

    private void Awake(){
        occurrence = this;
    }

    public void  GetObjectivesDone1(bool obj1){
        if(obj1 == true){
            objective1.text = "1. Compelted";
            o2.SetActive(true);
            objective1.color = Color.green;
        }
        else{
            objective1.text = "1. Find a rifle.";
            objective1.color = Color.red;
        }
    }

    public void  GetObjectivesDone2(bool obj2){
        if(obj2 == true){
            objective2.text = "2. Compelted";
            o2.SetActive(false);
            o3.SetActive(true);
            objective2.color = Color.green;
            objective3.color = Color.yellow;
            objective3.text = "3. Find a Vehicle, Keys recieved";
        }
        else{
            objective2.text = "2. Find your sister and her friends.";
            objective2.color = Color.red;
        }
    }

    public void  GetObjectivesDone3(bool obj3){
        if(obj3 == true){
            objective3.text = "3. Compelted";
            spotlight.SetActive(false);
            o4.SetActive(true);
            objective3.color = Color.green;
        }
        else{
            if(objective2.color == Color.green){
                objective3.color = Color.yellow;
                objective3.text = "3. Find a Vehicle, Keys recieved";
            }
            else{
                objective3.text = "3. Find a Vehicle.";
                objective3.color = Color.red;
            }
            
        }
    }

    public void  GetObjectivesDone4(bool obj4){
        if(obj4 == true){
            objective4.text = "4. Compelted";
            o4.SetActive(false);
            objective4.color = Color.green;
        }
        else{
            objective4.text = "4. Rescue Everyone.";
            objective4.color = Color.red;
        }
    }
}
