using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesOrFell : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit spikes");

        PlayerHealthController.instance.currentHealth--;
        //AudioManager.instance.playSFX(0);
        UIController.instance.UpdateHealthDisplay();

        if (other.tag == "Player" && PlayerHealthController.instance.currentHealth >= 1)
        {
            TutorialRespawnManager.instance.TutorialRespawnSpikeFall();
        }
        else if (other.tag == "Player" && PlayerHealthController.instance.currentHealth < 1)
        {
            TutorialRespawnManager.instance.TutorialRespawn();
        }
    }
    
}
