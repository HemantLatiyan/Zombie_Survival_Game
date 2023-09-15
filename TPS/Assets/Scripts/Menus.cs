using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
   
    [Header("All Menu's")]
    public GameObject pauseMenuUI;
    public GameObject EndGameMenuUI;
    public GameObject ObjectiveMenuUI;
    public GameObject TPSCanvasImage;
    public GameObject AimCanvasImage;

    public static bool GameIsStopped = false;

    private void Update(){
        if(ObjectiveMenuUI.activeSelf){
            showObjectives();
            TPSCanvasImage.SetActive(false);
            AimCanvasImage.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && ObjectiveMenuUI.activeSelf == false){
            if(GameIsStopped){
                Resume();
                TPSCanvasImage.SetActive(true);
                AimCanvasImage.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else{
                Pause();
                TPSCanvasImage.SetActive(false);
                AimCanvasImage.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else{
            if(Input.GetKeyDown("o") && pauseMenuUI.activeSelf == false){
                if(GameIsStopped){
                    TPSCanvasImage.SetActive(true);
                    AimCanvasImage.SetActive(true);
                    removeObjectives();
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else{
                    TPSCanvasImage.SetActive(false);
                    AimCanvasImage.SetActive(false);
                    showObjectives();
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }

    public void showObjectives(){
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }

    public void removeObjectives(){
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        TPSCanvasImage.SetActive(true);
        AimCanvasImage.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        TPSCanvasImage.SetActive(false);
        AimCanvasImage.SetActive(false);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }

    public void Restart(){
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame(){
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    
}
