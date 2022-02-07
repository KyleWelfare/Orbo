using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private string[] scenes = {
        "room1",
        "room2",
        "room3",
        "room4",
        "room5",
        "room6",
        "room7",
        "room8"
    };
    private int currentSceneIndex;
    private BoxCollider2D trigger;
    void Awake()
    {
        trigger = FindObjectOfType<BoxCollider2D>();
        currentSceneIndex = System.Array.IndexOf(scenes, SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        SceneManager.LoadScene(scenes[currentSceneIndex+1]);
    }
}
