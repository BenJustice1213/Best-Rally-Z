using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartRally()
    {
        SceneManager.LoadScene("Best Rally Z");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
