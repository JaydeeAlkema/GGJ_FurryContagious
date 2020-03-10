using UnityEngine;
using UnityEngine.SceneManagement;

// ********************************
//	Copyright 2018 @Mirco Baalmans
//	mircobaalmans@live.nl
// ********************************

public class UI : MonoBehaviour {

    public GameObject pauseScreen;

    //switch to the game scene
	public void toGame()
    {
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    //go back to the main menu
    public void toMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    } 
    //shut down the application
    public void exit()
    {
        Application.Quit();
    }
    //if not already, open the pause menu and pause the game
    //if already paused, close and resume
    public void pauseMenu()
    {
        if (!pauseScreen.active)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
