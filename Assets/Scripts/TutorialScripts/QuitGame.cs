using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject); //this makes the GameObject go through all the scenes. That means, this will work in every scene in the game.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Application has been quit!");
        }
    }
}
