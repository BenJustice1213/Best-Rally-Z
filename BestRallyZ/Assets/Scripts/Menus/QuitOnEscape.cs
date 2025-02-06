using UnityEngine;

public class QuitOnEscape : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject); //Make sure the game object associated with this script doesnt get deleted when switching scenes
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
