using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{

    public void RestartGame()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        Pause.TogglePause();
        SceneManager.LoadScene(activeScene.buildIndex);
    }
}
