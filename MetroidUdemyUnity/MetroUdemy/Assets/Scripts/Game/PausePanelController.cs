using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelController : MonoBehaviour
{


    public void Start()
    {
        
    }


    private void Update()
    {
        
    }


    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("QUIT GAME");
    }
}
