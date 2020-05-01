using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void NextLevel(int _ceneNumber)
    {
        SceneManager.LoadScene(_ceneNumber);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        
        {
         SceneManager.LoadScene(0);
        }
    }
    public void QuitGame() 
    {
        Debug.Log("Quit");
        Application.Quit();
    }


}
