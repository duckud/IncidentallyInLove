using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void comingSoon() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void goBack() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
    public void loadDialogueScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);
    }
}
