using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void Resume()
    {
        FindObjectOfType<GameManager>().play();
    }

    // Update is called once per frame
    public void Restart()
    {
        //FindObjectOfType<GameManager>().play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        //FindObjectOfType<GameManager>().play();
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        //FindObjectOfType<GameManager>().play();
        Application.Quit();
    }

}
