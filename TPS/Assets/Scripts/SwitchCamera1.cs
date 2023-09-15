using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera1 : MonoBehaviour
{
    public Camera Main;
    public Camera firstPerson;
    private bool switchKey;
    private bool defaultCamera;
    // Start is called before the first frame update
    void Start()
    {
        defaultCamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        switchKey = Input.GetKeyDown("t");
        if (switchKey) {
            if (defaultCamera) {
                Main.enabled = false;
                firstPerson.enabled = true;
                defaultCamera = false;
            }else {
                Main.enabled = true;
                firstPerson.enabled = false;
                defaultCamera = true;
            }
        }
    }
}
