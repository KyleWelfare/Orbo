using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    [SerializeField] private PlayerInputHandler playerInputHandler;

    [SerializeField] private GameObject pauseScreen;
    [SerializeField] public bool isPaused = false;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();
    }
    private void Start()
    {
        playerInputAction.Gameplay.Pause.performed += _ => PauseUnpause();
    }

    void OnEnable()
    {
        playerInputAction.Enable();
    }

    void OnDisable()
    {
        playerInputAction.Disable();
    }

    public void PauseUnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            playerInputHandler.enabled = true;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            playerInputHandler.enabled = false;

        }
    }
}
