using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{   
    public GameObject selectCharacter;
    public GameObject mainMenu;

    public void OnBackButton(){
        selectCharacter.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OnSelectCharacter1(){
        SceneManager.LoadScene("World");
    }

    public void OnSelectCharacter2(){
        SceneManager.LoadScene("World 1");
    }
}
