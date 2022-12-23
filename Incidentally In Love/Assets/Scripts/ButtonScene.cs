using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//source: https://www.youtube.com/watch?v=EMo-MaKkP9s

public class ButtonScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
